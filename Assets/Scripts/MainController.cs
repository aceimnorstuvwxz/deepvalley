using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainController : MonoBehaviour {
	public float delay_generate_terrain = 0.5f;

	private int _currentLevel;
	private string[] _randomSeedMap;

	private TerrainGenerator _terrainGenerator;
	private GameObject _goFlyings;

	// Use this for initialization
	void Start () {

		_randomSeedMap = new string[]{"7a","b","c","d","e","f","g","h"};

		_currentLevel = PlayerPrefs.HasKey ("current_level") ? PlayerPrefs.GetInt ("current_level") : 0;
	
		string randomSeed = "9a" + _currentLevel.ToString () + (_currentLevel * _currentLevel).ToString ();
		if (_currentLevel < _randomSeedMap.Length) {
			randomSeed = _randomSeedMap[_currentLevel];
		}

		_terrainGenerator = GetComponent<TerrainGenerator> ();
		_terrainGenerator.random_seed = randomSeed;
		
		_terrainGenerator.generateTerrain ();
//		Invoke ("beginGenerateTerrain", 0);

		_goFlyings = GameObject.Find ("Flyings");

		Invoke ("StartAddFlying", 3);
	}

	System.Random _flyingRandomGen;
//	List<GameObject> _flyings;
	AFlyingNames _flyingNamesGen = new AFlyingNames();
	int _idIndex = 0;
	void StartAddFlying()
	{
		string	seed = Time.time.ToString();
//		_flyings = new List<GameObject> ();
		_idIndex = 0;
		
		_flyingRandomGen = new System.Random(seed.GetHashCode());
		StartCoroutine ("AddingFlyings");
	}

	IEnumerator AddingFlyings() {
		for (;;) {
			AddOneFlying();
			yield return new WaitForSeconds(_flyingRandomGen.Next(1,5)*1f);
		}
	}



	void AddOneFlying() {
		string flyName = _flyingNamesGen.getRandomFlyingName ();
		int id = _idIndex++;
		Debug.Log ("Add flying:"+flyName+"  id:"+id.ToString());
		GameObject obj = Instantiate( Resources.Load(flyName) )as GameObject;
		obj.transform.parent = _goFlyings.transform;
		AFlyingController flyCon = obj.AddComponent<AFlyingController>() as AFlyingController;
		flyCon.setId (id);
//		_flyings.Add (obj);
	}

	/*
	void beginGenerateTerrain()
	{
		Debug.Log ("beginGenerateTerrain");
		_terrainGenerator.generateTerrain ();
	}*/
	
	// Update is called once per frame
	void Update () {
	
	}
}
