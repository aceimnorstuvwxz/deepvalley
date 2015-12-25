using UnityEngine;
using System.Collections;

public class uicontriller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickTest() {
		Debug.Log ("click test");
	}


	// UI buttons control callbacks
	public void OnArrowLeftDown() {
		Debug.Log ("left down");

	}

	public void OnArrowLeftUp() {
		Debug.Log ("left up");
		
	}

	public void OnArrowRightDown() {
		Debug.Log ("left down");
		
	}
	
	public void OnArrowRightUp() {
		Debug.Log ("left up");
		
	}

	public void OnArrowTopDown() {
		Debug.Log ("left down");
		
	}
	
	public void OnArrowTopUp() {
		Debug.Log ("left up");
		
	}

	public void OnArrowBottomDown() {
		Debug.Log ("left down");
		
	}
	
	public void OnArrowBottomUp() {
		Debug.Log ("left up");
		
	}

	public void OnClickShoot() {
		Debug.Log ("shoot");
	}
}
