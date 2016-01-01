using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {
	public float delay_generate_terrain = 0.5f;

	private int _currentLevel;
	private string[] _randomSeedMap;

	private TerrainGenerator _terrainGenerator;

	// Use this for initialization
	void Start () {

		_randomSeedMap = new string[]{"arisecbf","b","c","d","e","f","g","h"};

		_currentLevel = PlayerPrefs.HasKey ("current_level") ? PlayerPrefs.GetInt ("current_level") : 0;
	
		string randomSeed = "9a" + _currentLevel.ToString () + (_currentLevel * _currentLevel).ToString ();
		if (_currentLevel < _randomSeedMap.Length) {
			randomSeed = _randomSeedMap[_currentLevel];
		}

		_terrainGenerator = GetComponent<TerrainGenerator> ();
		_terrainGenerator.random_seed = randomSeed;
		
		_terrainGenerator.generateTerrain ();
//		Invoke ("beginGenerateTerrain", 0);
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
