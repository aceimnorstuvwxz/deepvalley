using UnityEngine;
using System.Collections;

public class ViewButton : MonoBehaviour {
	public float radio_speed = 1f;
	private UIController uiController;
	private CannonController cannonController;
	private RadarController radarController;
	private int _currentMode = 0; //0 -1 -2
	private float _targetRadio = 0f;
	private float _currentRadio = 0f;

	private GameObject _goIn;
	private GameObject _goOut;
	// Use this for initialization
	void Start () {
		
		uiController = GameObject.Find ("UI-Canvas").GetComponent<UIController> ();
		cannonController = GameObject.Find ("cannon").GetComponent<CannonController> ();
		radarController = GameObject.Find ("Radar").GetComponent<RadarController> ();

		_goIn = GameObject.Find ("btn_view");
		_goOut = GameObject.Find ("btn_view2");
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch t in Input.touches){

			if (t.phase == TouchPhase.Ended) {
				if (RectTransformUtility.RectangleContainsScreenPoint(_goIn.GetComponent<RectTransform>(), t.position)) {
					Debug.Log("C in");
					SwitchModeIn();
				} 
				if (RectTransformUtility.RectangleContainsScreenPoint(_goOut.GetComponent<RectTransform>(), t.position)) {
					Debug.Log("C in");
					SwitchModeOut();
				} 
			}
		}
		RefreshRadio ();
	}

	void SwitchModeIn()
	{
		_currentMode = _currentMode == 0 ? 1 :
			_currentMode == 1 ? 2 : 2;
		_targetRadio = _currentMode * 0.5f;
	}

	void SwitchModeOut()
	{
		_currentMode = _currentMode == 0 ? 0 :
			_currentMode == 1 ? 0 : 1;
		_targetRadio = _currentMode * 0.5f;
	}

	void RefreshRadio()
	{
		if (_currentRadio != _targetRadio) {
			if (_currentRadio > _targetRadio) {
				_currentRadio = Mathf.Max (_targetRadio, _currentRadio - Time.deltaTime*radio_speed);
			} else {
				_currentRadio = Mathf.Min (_targetRadio, _currentRadio + Time.deltaTime*radio_speed);
			}

			float value = _currentRadio;

			// control the cross image
			uiController.swiftSightScope (value);
			
			// control camera scaling
			cannonController.swiftSightScope (value);
			
			// control radar's scope view
			radarController.swiftSightScope (value);
		}
	}
}
