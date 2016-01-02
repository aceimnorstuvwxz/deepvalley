using UnityEngine;
using System.Collections;

public class TheBullet : MonoBehaviour {

//	private Rigidbody
	// Use this for initialization
	void Start () {
	
	}

	public void SetBeginSpeed(Vector3 speed)
	{
		gameObject.GetComponent<Rigidbody> ().velocity = speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
