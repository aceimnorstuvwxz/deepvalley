using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject bullet_prefab;


	private GameObject _boreHead;
	private GameObject _goBullets;
	private GameObject _goCamera;

	// Use this for initialization
	void Start () {
		_boreHead = GameObject.Find ("bore-head");
		_goBullets = GameObject.Find ("Bullets");
		_goCamera = GameObject.Find ("main-camera");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LaunchBullet( )
	{
		//position

		Vector3 pos = _boreHead.transform.position;

		GameObject bullet = Instantiate (bullet_prefab) as GameObject;
		bullet.transform.position = pos;
//		bullet.transform.rotation =  //TODO diection and speed and collision rigidbody??
		bullet.transform.SetParent (_goBullets.transform);


	}

	public void ShootBatch() {

		// TODO get config of cannon
		LaunchBullet ();

	}
}
