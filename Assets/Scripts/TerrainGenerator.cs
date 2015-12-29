using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// as the terrain self
public class TerrainGenerator : MonoBehaviour {

	public int width_para_n = 5; //2^N + 1 per edge
	public float reduce_rate = 0.7f; //random value scope reduce radio
	public float init_height = 0f;
	public float init_random_scope = 10f;
	public string random_seed = "arisecbf";

	private float [,] _heightMap;
	private float _minHeight;
	private float _maxHeight;
	private bool[,,] _voxels;
	private System.Random _randomGen;

	private Dictionary<VoxelNode, int> _voxelPointIndexTable;


	class Hmp //height map point
	{
		public Hmp(int x, int y){
			this.x = x;
			this.y = y;
		}

		public int x;
		public int y;
	}

	class VoxelNode
	{
		public int x;
		public int y;
		public int h;

		public VoxelNode(int x, int y, int h) {
			this.x = x;
			this.y = y;
			this.h = h;
		}
		public class EqualityComparer : IEqualityComparer<VoxelNode> {
			
			public bool Equals(VoxelNode a, VoxelNode b) {
				return a.x == b.x && a.y == b.y && a.h == b.h;
			}
			
			public int GetHashCode(VoxelNode a) {
				int c = a.x ^ a.y ^ a.h;
				return c.GetHashCode();
			}
		}
	}



	void generateHeightMap()
	{
		_randomGen = new System.Random(random_seed.GetHashCode());

		int width = 1;
		for (int i = 0; i < width_para_n; i++) {
			width = width*2;
		}
		width += 1;

		_heightMap = new float[width,width];
		_heightMap [0,0] = init_height;
		_heightMap [0,width - 1] = init_height;
		_heightMap [width - 1,0] = init_height;
		_heightMap [width - 1,width - 1] = init_height;

		_minHeight = _maxHeight = 0;


		int currentStep = width - 1;
		float currentRandomScope = init_random_scope;
		for (int i = 0; i < width_para_n; i++) {
			for (int x = 0; x < width-1; x += currentStep) {
				for ( int y = 0; y < width-1; y += currentStep) {
					diamond(new Hmp(x,y), new Hmp(x, y+currentStep), new Hmp(x+currentStep, y), new Hmp(x+currentStep, y+currentStep),currentRandomScope);
				}
			}
			int halfStep = currentStep/2;
			for (int y = 0; y < width; y += halfStep) {
				for (int x = y%(currentStep) == 0 ? halfStep : 0; x < width; x+= currentStep) {
//					Debug.Log(x.ToString() + " " +y.ToString() + " " + halfStep.ToString());
					square(new Hmp(x-halfStep,y), new Hmp(x+halfStep, y), new Hmp(x, y-halfStep), new Hmp(x, y+halfStep),currentRandomScope);
				}
			}
			currentStep = currentStep/2;
			currentRandomScope = currentRandomScope*reduce_rate;
		}


	}

	float getHmpv(Hmp p) {
		return _heightMap [p.x, p.y];
	}
	void setHmpv(Hmp p, float v) {
		_heightMap [p.x, p.y] = v;
		_minHeight = Mathf.Min (_minHeight, v);
		_maxHeight = Mathf.Max (_maxHeight, v);
	}
	bool isLegelHmp(Hmp p) {
		int width = _heightMap.GetLength (0);
		return p.x >= 0 && p.x < width && p.y >= 0 && p.y < width;
	}

	void diamond(Hmp lb, Hmp lt,Hmp rb, Hmp rt, float randomScope)
	{
		int centerX = (lb.x + rt.x) / 2;
		int centerY = (lb.y + rt.y) / 2;
		Hmp center = new Hmp (centerX, centerY);
		float centerValue = 0.25f * (getHmpv (lb) + getHmpv (lt) + getHmpv (rb) + getHmpv (rt)) + (float)((_randomGen.NextDouble()-0.5)*2)*randomScope;
		setHmpv (center, centerValue);

		/*
		float nextRandomScope = randomScope * reduce_rate;
		// left square
		square (new Hmp (lb.x + lb.x - center.x, center.y), center, lb, lt, nextRandomScope);

		// right square
		square (center, new Hmp (rb.x + rb.x - center.x, center.y), rb, rt, nextRandomScope);

		// bottom square
		square (lb, rb, new Hmp (center.x, lb.y + lb.y - center.y), center, nextRandomScope);

		// top square
		square (lt, rt, center, new Hmp (center.x, lt.y + lt.y - center.y), nextRandomScope);
		*/
	}

	void square(Hmp left, Hmp right, Hmp bottom, Hmp top,  float randomScope)
	{
		int cnt = 0;
		float amount = 0;
		if (isLegelHmp (left)) {
			amount += getHmpv(left);
			cnt ++;
		} 
		if (isLegelHmp (right)) {
			amount += getHmpv(right);
			cnt ++;
		} 
		if (isLegelHmp (bottom)) {
			amount += getHmpv(bottom);
			cnt ++;
		} 
		if (isLegelHmp (top)) {
			amount += getHmpv(top);
			cnt ++;
		} 

		float centerValue = amount / cnt + (float)((_randomGen.NextDouble () - 0.5)*2) * randomScope;
		Hmp center = new Hmp (top.x, left.y);
		setHmpv (center, centerValue);

	}

	void generateVoxelMesh() 
	{
		// using Height map && minHeight/maxHeight, using 1 as step!!


		//new voxels
		float totalHeight = (_maxHeight - _minHeight);
		int voxelHeight = (int)totalHeight + 1;
		int width = _heightMap.GetLength (0);
		_voxels = new bool[width,width,voxelHeight]; // x,y,height

		//heightmap to voxels
		for (int y = 0; y < width; y++) {
			for (int x = 0; x < width; x++) {
				float v = getHmpv(new Hmp(x,y));
				int height = (int)(v-_minHeight);
				for (int i = 0; i < voxelHeight; i++) {
					_voxels[x,y,i] = (i <= height);
				}
			}
		}


		//marching cubes
		_voxelPointIndexTable = new Dictionary<VoxelNode, int> (new VoxelNode.EqualityComparer ());

	}

	public void refreshHeightMap(){
		generateHeightMap ();
		generateVoxelMesh ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos() {
		if (_heightMap != null) {
			int width = _heightMap.GetLength (0);
			int height = width;
			for (int x = 0; x < width; x ++) {
				for (int y = 0; y < height; y ++) {
					float v = getHmpv(new Hmp(x,y));
//					Debug.Log("v"+v.ToString());
					Gizmos.color = new Color((v+5)/10f,1-(v+5)/10f,0,1);
					Vector3 pos = new Vector3(-width/2 + x + .5f, v, -height/2 + y+.5f);
					Gizmos.DrawCube(pos,Vector3.one/2f);
				}
			}
		}

	}
}
