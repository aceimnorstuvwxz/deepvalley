using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScopeSwitcher : MonoBehaviour {

	private UIController uiController;

	// Use this for initialization
	void Start () {
		uiController = GameObject.Find ("UI-Canvas").GetComponent<UIController> ();
		Debug.Assert (uiController);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnValueChanged(float value) {
		// control the scope sight here
		// so the move of slider will dynamiclly 
		// influence the sight


		// control the cross image
		uiController.swiftSightScope (value);


	}

	public void OnSlidingDone() {
		// make sliding in 3 degree
		var slider = GetComponent<Slider> ();
		float value = slider.value;
		if (value < 0.3f) {
			slider.value = 0f;
		} else if (value < 0.7f) {
			slider.value = 0.5f;
		} else {
			slider.value = 1f;
		}
	}
}
