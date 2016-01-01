using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RadarController : MonoBehaviour {

	public float scan_speed = 180; //angle per second

	private GameObject scanWave;
	
	private Image scopeBig;
	private Image scopeMiddle;
	private Image scopeSmall;

	// Use this for initialization
	void Start () {
		scanWave = GameObject.Find ("scan_wave");
		
		scopeBig = GameObject.Find ("scope_big").GetComponent<Image> ();
		scopeMiddle = GameObject.Find ("scope_middle").GetComponent<Image> ();
		scopeSmall = GameObject.Find ("scope_small").GetComponent<Image> ();

		
		scopeBig.color = new Color (1, 1, 1, 1);
		scopeMiddle.color = new Color (1, 1, 1, 0);
		scopeSmall.color = new Color (1, 1, 1, 0);
	
	}
	
	// Update is called once per frame
	void Update () {
		scanWave.transform.Rotate (new Vector3 (0, 0, -Time.deltaTime * scan_speed));
	}

	// control scope
	public void swiftSightScope(float radio)
	{
		float alphaBig = radio < 0.5 ? (1 - radio / 0.5f) : 0;
		float alphaMiddle = 1f - Mathf.Abs (radio - 0.5f) / 0.5f;
		float alphaSmall = radio > 0.5 ? (1 - (1 - radio) / 0.5f) : 0;

		scopeBig.color = new Color (1, 1, 1, alphaBig);
		scopeMiddle.color = new Color (1, 1, 1, alphaMiddle);
		scopeSmall.color = new Color (1, 1, 1, alphaSmall);

	}

	public void turnSightScope(float angle)
	{
		var euler = new Vector3 (0, 0, -angle);
		scopeBig.transform.eulerAngles = euler;
		scopeMiddle.transform.eulerAngles = euler;
		scopeSmall.transform.eulerAngles = euler;
	}

	public void AddPoint(int id)
	{

	}

	public void UpdatePoint(int id, Vector3 newPosition)
	{

	}

	public void DeletePoint(int id)
	{

	}
}
