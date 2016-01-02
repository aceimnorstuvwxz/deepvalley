using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

	private BulletController _bulletController;

	// Use this for initialization
	void Start () {

		_bulletController = GameObject.Find ("cannon").GetComponent<BulletController> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickShoot() {
		Debug.Log ("shoot");

		// TODO CD 
		
		_bulletController.ShootBatch ();


	}
}
