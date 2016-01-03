using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RadarController : MonoBehaviour {

	public float scan_speed = 180; //angle per second
	public Image point_prefab;

	private GameObject scanWave;
	
	private Image scopeBig;
	private Image scopeMiddle;
	private Image scopeSmall;

	private Dictionary<int, Image> _points;

	// Use this for initialization
	void Start () {
		scanWave = GameObject.Find ("scan_wave");
		
		scopeBig = GameObject.Find ("scope_big").GetComponent<Image> ();
		scopeMiddle = GameObject.Find ("scope_middle").GetComponent<Image> ();
		scopeSmall = GameObject.Find ("scope_small").GetComponent<Image> ();

		
		scopeBig.color = new Color (1, 1, 1, 1);
		scopeMiddle.color = new Color (1, 1, 1, 0);
		scopeSmall.color = new Color (1, 1, 1, 0);

		_points = new Dictionary<int, Image> ();
	
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
		Image pointImgae = Instantiate (point_prefab) as Image;
		pointImgae.transform.SetParent (transform);
		_points.Add (id, pointImgae);
	}

	public void UpdatePoint(int id, Vector2 position)
	{
		if (!_points.ContainsKey (id))
			return;

		RectTransform rect = gameObject.GetComponent<RectTransform> ();
		float radarScale = rect.sizeDelta.x/2;

		_points [id].GetComponent<RectTransform>().localPosition = new Vector3 (position.x * radarScale, position.y * radarScale, 0f);
	}

	public void DeletePoint(int id)
	{
		if (!_points.ContainsKey (id))
			return;

		var img = _points [id];
		_points.Remove(id);
		Destroy (img);
	}
}
