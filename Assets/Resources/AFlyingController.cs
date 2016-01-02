using UnityEngine;
using System.Collections;


// controll the total life of a flying!
public class AFlyingController : MonoBehaviour {


	// Use this for initialization
	public float flying_scale = 3f;
	public float colider_radius = 0.8f;

	private TerrainGenerator _terrainGenerator;
	private RadarController _radarController;
	private float _speed = 0;
	private float _acce = 1f;
	private float _maxSpeed = 2f;
	private Vector3 _direction;
	private float _terrainWidthHalf;

	private GameObject _explosion;


	private int _id;

	public void setId(int id) {
		_id = id;
	}

	void Start () {

		_terrainGenerator = GameObject.Find ("Terrain").GetComponent<TerrainGenerator> ();
		_radarController = GameObject.Find ("Radar").GetComponent<RadarController> ();
		
		transform.position  = _terrainGenerator.nextFlyingPosition();
		_direction = _terrainGenerator.nextFlyingDirection ();

		transform.localScale = new Vector3 (flying_scale,flying_scale,flying_scale);
		_terrainWidthHalf = 0.5f * _terrainGenerator.getTerrainWidth ();

		// add point to radar
		_radarController.AddPoint (_id);

		Rigidbody rigid = gameObject.AddComponent<Rigidbody> ();
		rigid.isKinematic = true;
		rigid.useGravity = false;

		SphereCollider colider = gameObject.AddComponent<SphereCollider> ();
		colider.radius = colider_radius;
		colider.isTrigger = true;

		gameObject.tag = "Flying";



		_explosion = Instantiate (Resources.Load("Explosion"))as GameObject;
		_explosion.transform.SetParent (transform);
		_explosion.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (_speed < _maxSpeed) {
			_speed += _acce * Time.deltaTime;
		}
		transform.position = transform.position + _direction * (_speed * Time.deltaTime);
		Vector2 pointPosition = ( new Vector2 (transform.position.x - _terrainWidthHalf, transform.position.z - _terrainWidthHalf) )* (1 / _terrainWidthHalf);
		_radarController.UpdatePoint(_id, pointPosition);
	}


	void OnTriggerEnter(Collider other) {
		Debug.Log ("trigger enter");
		if (other.gameObject.tag == "Bullet") {
//			_explosion.GetComponent<ExplosionController>().boom();
//			Destroy (other.gameObject);
//			Destroy (gameObject);
		}
	}
}
