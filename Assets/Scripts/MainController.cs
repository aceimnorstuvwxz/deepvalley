using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
	public float delay_generate_terrain = 0.5f;

	private int _currentLevel;
	private string[] _randomSeedMap;
	private int[] _targetScores;

	private TerrainGenerator _terrainGenerator;
	private GameObject _goFlyings;
	private Text _textLeftTime;
	private Text _textCurrentScore;
	private Text _textTargetScore;
	private int _currentScore;
	private int _targetScore;
	private float _leftTime;

	// Use this for initialization
	void Start () {

		_randomSeedMap = new string[]{"7a", "b", "c", "d", "e", "f", "g", "h"};
		_targetScores = new int[]    {3,     5,   8,   13,  21,  34,  55,  89};


		_currentLevel = PlayerPrefs.HasKey ("current_level") ? PlayerPrefs.GetInt ("current_level") : 0;
	
		string randomSeed = _randomSeedMap[_currentLevel];


		_leftTime = 2 * 60;
		_targetScore = _targetScores [_currentLevel];
		_currentScore = 0;

		_textLeftTime = GameObject.Find ("left_time").GetComponent<Text> ();
		_textTargetScore = GameObject.Find ("target-score").GetComponent<Text> ();
		_textCurrentScore = GameObject.Find ("current-score").GetComponent<Text> ();

		_textTargetScore.text =  (_targetScore <10 ? "0":"" )+_targetScore.ToString ();
		_textCurrentScore.text = (_currentScore < 10 ? "0" : "") + _currentScore.ToString ();


		_terrainGenerator = GetComponent<TerrainGenerator> ();
		_terrainGenerator.random_seed = randomSeed;
		
		_terrainGenerator.generateTerrain ();

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
			yield return new WaitForSeconds(_flyingRandomGen.Next(3,5)*1f);
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
	}

	// kill a flying!
	public void AddScore()
	{
		_currentScore++;
		_textCurrentScore.text = (_currentScore < 10 ? "0" : "") + _currentScore.ToString ();

		if (_currentScore == _targetScore) {
			Debug.Log("Win");
		}
	}

	
	// Update is called once per frame
	void Update () {
		_leftTime -= Time.deltaTime;
		int minut = (int)(_leftTime / 60);
		int second = (int)((_leftTime - minut * 60));
		_textLeftTime.text = minut.ToString () + ":" + (second < 10 ? "0" : "") + second.ToString ();

		if (_leftTime <= 0) {
			Debug.Log("GO");
		}
	}
}
