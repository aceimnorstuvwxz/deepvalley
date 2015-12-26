using UnityEngine;
using System.Collections;

public class joycontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		foreach (var touch in Input.touches) 
		{
			Debug.Log("x:"+touch.position.x.ToString() + " y:"+touch.position.y.ToString() );
		}
	
	}
}
