using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MainController : MonoBehaviour {
	public float delay_generate_terrain = 0.5f;
	public float init_left_time = 60f*2;
	public float time_add_per_score = 15f;
	private int _currentLevel;


	private TerrainGenerator _terrainGenerator;
	private GameObject _goFlyings;
	private Text _textLeftTime;
	private Text _textCurrentScore;
	private Text _textTargetScore;
	private int _currentScore;
	private int _targetScore;
	private float _leftTime;
	private bool _running = false;
	private Color _leftTextOldColor;

	// Use this for initialization
	void Start () {

		int[] targetScores = new int[]           {5,        13,       34,       89};
		float[] reduceRates = new float[]        {0.65f,    0.6f,    0.65f,   0.65f};

		float[] initHeightLB = new float[]       {0,        0,        0,       30};
		float[] initHeightLT = new float[]       {30,       50,       30,      0};
		float[] initHeightRT = new float[]       {30,       50,       30,      0};
		float[] initHeightRB = new float[]       {0,        0,        0,       20};

		float[] initRandomScope = new float[]    {15.44f,    20f,     20f,     20f};
		string[] randomSeedMap = new string[]    {"7a",     "be",      "c",     "d3"};
		float[] centerDeepScales = new float[]   {2f,       0f,       2f,      1.5f};
		bool[] smoothNormals = new bool[]         {false,    false,    true,   false};

		_currentLevel = PlayerPrefs.HasKey ("current_level") ? PlayerPrefs.GetInt ("current_level") : 0;

		//debug
		_currentLevel = 0;
	
		_leftTime = init_left_time;
		_targetScore = targetScores [_currentLevel];
		_currentScore = 0;

		_textLeftTime = GameObject.Find ("left_time").GetComponent<Text> ();
		_textTargetScore = GameObject.Find ("target-score").GetComponent<Text> ();
		_textCurrentScore = GameObject.Find ("current-score").GetComponent<Text> ();

		_textTargetScore.text =  (_targetScore <10 ? "0":"" )+_targetScore.ToString ();
		_textCurrentScore.text = (_currentScore < 10 ? "0" : "") + _currentScore.ToString ();


		_terrainGenerator = GetComponent<TerrainGenerator> ();
		
		_terrainGenerator.reduce_rate = Random.Range(0.55f,0.7f);//reduceRates [_currentLevel];
		_terrainGenerator.init_height_left_bottom = Random.Range (-5, 15);// initHeightLB [_currentLevel];
		_terrainGenerator.init_height_left_top = Random.Range (5, 35);//initHeightLT [_currentLevel];
		_terrainGenerator.init_height_right_top = Random.Range (5, 35);//initHeightRT [_currentLevel];
		_terrainGenerator.init_height_right_bottom = Random.Range (-5, 15);//initHeightRB [_currentLevel];
		_terrainGenerator.init_random_scope = Random.Range (15f, 25f);// initRandomScope [_currentLevel];
		_terrainGenerator.random_seed = Random.Range (0f,10000f).ToString();//randomSeedMap[_currentLevel];
		_terrainGenerator.center_deep_scale = Random.Range (0,2f);// centerDeepScales [_currentLevel];
		_terrainGenerator.smooth_normal = false;//smoothNormals [_currentLevel];

		_terrainGenerator.generateTerrain ();

		_goFlyings = GameObject.Find ("Flyings");
		_leftTextOldColor = _textLeftTime.color;
		RefreshTime ();

		{
			GameObject uiCanvas = GameObject.Find ("UI-Canvas");
			CanvasScaler scaler = uiCanvas.GetComponent<CanvasScaler> ();
			uiCanvas.SetActive (true);
			scaler.scaleFactor = 0.001f;
		}
		{
			GameObject uiCanvas = GameObject.Find ("Radar-Canvas");
			CanvasScaler scaler = uiCanvas.GetComponent<CanvasScaler> ();
			uiCanvas.SetActive (true);
			scaler.scaleFactor = 0.001f;
		}


		StartCoroutine ("CameraDance");
	}

	System.Random _flyingRandomGen;
//	List<GameObject> _flyings;
	AFlyingNames _flyingNamesGen = new AFlyingNames();
	int _idIndex = 0;
	void StartAddFlying()
	{
		string	seed = Time.time.ToString();
		_running = true;
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
		GameObject obj = Instantiate( Resources.Load("AFlyings/"+flyName) )as GameObject;
		obj.transform.parent = _goFlyings.transform;
		AFlyingController flyCon = obj.AddComponent<AFlyingController>() as AFlyingController;
		flyCon.setId (id);
	}

	// kill a flying!
	public void AddScore()
	{
		_currentScore++;
		_textCurrentScore.text = (_currentScore < 10 ? "0" : "") + _currentScore.ToString ();
		_leftTime = Mathf.Min (init_left_time, _leftTime + time_add_per_score);

		if (_currentScore == _targetScore) {
			Debug.Log("Win");
		}
	}

	void RefreshTime() 
	{
		if (_leftTime > 0) {
			_leftTime = Mathf.Max(_leftTime - Time.deltaTime, 0f);

			int minut = (int)(_leftTime / 60);
			int second = (int)((_leftTime - minut * 60));
			_textLeftTime.text = minut.ToString () + ":" + (second < 10 ? "0" : "") + second.ToString ();

			Color c = _leftTime < 10 ? Color.red : _leftTextOldColor;
			_textLeftTime.color = c;
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (_running) {
			RefreshTime();
			if (_leftTime <= 0) {
				Debug.Log ("GO");
			}
		}
	}

	private Quaternion oldRotation;
	public void Play()
	{
		Debug.Log ("play");

		StartCoroutine ("WelcoOut");
		StopCoroutine ("CameraDance");
		mainCamera.transform.rotation = oldRotation;
		
		Invoke ("StartAddFlying", 3);
	}

	IEnumerator WelcoOut()
	{

		GameObject welcoCanvas = GameObject.Find ("Welco-Canvas");
		CanvasScaler scaler =  welcoCanvas.GetComponent<CanvasScaler> ();
		for (;;) {
			scaler.scaleFactor = scaler.scaleFactor-0.2f;
			if (scaler.scaleFactor < 0.05f) {
				break;
			}
			yield return null;
		}
		Destroy (welcoCanvas);
		Invoke ("StartAddFlying", 3);

		Begin ();
	}

	public void Begin()
	{
		StartCoroutine ("UICanvasIn");
		StartCoroutine ("RadarCanvasIn");
		StartCoroutine ("loadingCount2");
	}
	
	IEnumerator UICanvasIn()
	{
		GameObject uiCanvas = GameObject.Find ("UI-Canvas");
		CanvasScaler scaler = uiCanvas.GetComponent<CanvasScaler> ();
		uiCanvas.SetActive (true);
		scaler.scaleFactor = 0.001f;
		for (;;) {
			scaler.scaleFactor = scaler.scaleFactor+0.2f;
			if (scaler.scaleFactor > 1f) {
				scaler.scaleFactor = 1f;
				break;
			}
			yield return null;
		}
	}

	IEnumerator RadarCanvasIn()
	{
		GameObject uiCanvas = GameObject.Find ("Radar-Canvas");
		CanvasScaler scaler = uiCanvas.GetComponent<CanvasScaler> ();
		uiCanvas.SetActive (true);
		scaler.scaleFactor = 0.001f;
		for (;;) {
			scaler.scaleFactor = scaler.scaleFactor+0.2f;
			if (scaler.scaleFactor > 1f) {
				scaler.scaleFactor = 1f;
				break;
			}
			yield return null;
		}
	}

	public Camera mainCamera;

	IEnumerator loadingCount2() {
		int N = 50;
		for (int i = 0; i <= N ; i++) {
//			mainCamera.farClipPlane = 3f+i*1.5f;
			mainCamera.GetComponent<BlurOptimized>().blurSize = (N-i)*1f/N;
			
			yield return null;
		}
		mainCamera.farClipPlane = 1000f;
				mainCamera.GetComponent<BlurOptimized>().enabled = false;
	}

	IEnumerator CameraDance()
	{
		Vector3 r = new Vector3 (UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
		oldRotation = mainCamera.transform.rotation;
		for (;;) {
			mainCamera.transform.Rotate(r);
			yield return new WaitForFixedUpdate();
		}
	}
}
