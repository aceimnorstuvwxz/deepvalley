using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject bullet_prefab;
	public float bullet_speed = 10f;

	private GameObject _boreHead;
	private GameObject _boreHead2;

	private GameObject _goBullets;
	private GameObject _goCamera;


	// Use this for initialization
	void Start () {
		_boreHead = GameObject.Find ("bore-head");
		_boreHead2 = GameObject.Find ("bore-head2");

		_goBullets = GameObject.Find ("Bullets");
		_goCamera = GameObject.Find ("main-camera");



	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LaunchBullet( )
	{
		//position

		{
			Vector3 pos = _boreHead.transform.position;

			GameObject bullet = Instantiate (bullet_prefab) as GameObject;
			bullet.transform.position = pos;
			bullet.transform.rotation = _goCamera.transform.rotation;
			bullet.transform.SetParent (_goBullets.transform);

			bullet.GetComponent<TheBullet> ().SetBeginSpeed (bullet_speed * _goCamera.transform.forward);
		}
		{
			Vector3 pos = _boreHead2.transform.position;
			
			GameObject bullet = Instantiate (bullet_prefab) as GameObject;
			bullet.transform.position = pos;
			bullet.transform.rotation = _goCamera.transform.rotation;
			bullet.transform.SetParent (_goBullets.transform);
			
			bullet.GetComponent<TheBullet> ().SetBeginSpeed (bullet_speed * _goCamera.transform.forward);
		}
	}

	public void ShootBatch() {

		// TODO get config of cannon
		LaunchBullet ();

	}

}
