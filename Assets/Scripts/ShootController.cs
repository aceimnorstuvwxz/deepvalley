using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

	private BulletController _bulletController;
	private CannonController _cannonController;

	// Use this for initialization
	void Start () {

		_bulletController = GameObject.Find ("cannon").GetComponent<BulletController> ();
		_cannonController = GameObject.Find ("cannon").GetComponent<CannonController> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickShoot() {
		Debug.Log ("shoot");

		// TODO CD 
		
		_bulletController.ShootBatch ();
		_cannonController.shootEffects ();


	}
}
