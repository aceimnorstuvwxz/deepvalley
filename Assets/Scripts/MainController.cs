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
	List<GameObject> _flyings;
	void StartAddFlying()
	{
		string	seed = Time.time.ToString();
		_flyings = new List<GameObject> ();
		
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
		Debug.Log ("add flying...");
		GameObject obj = Instantiate( Resources.Load("BabyDuck") )as GameObject;
		obj.transform.position  = new Vector3(72,28,120);
		obj.transform.localScale = new Vector3 (100, 100, 100);
		obj.transform.parent = _goFlyings.transform;
		AFlyingController flyCon = obj.AddComponent<AFlyingController>() as AFlyingController;
		_flyings.Add (obj);
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
