using UnityEngine;
using System.Collections;


// controll the total life of a flying!
public class AFlyingController : MonoBehaviour {

	// Use this for initialization
	public float flying_scale = 3f;

	private TerrainGenerator _terrainGenerator;
	private RadarController _radarController;
	private float _speed = 0;
	private float _acce = 0.5f;
	private float _maxSpeed = 1f;
	private Vector3 _direction;
	private float _terrainWidthHalf;

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
	}
	
	// Update is called once per frame
	void Update () {
		if (_speed < _maxSpeed) {
			_speed += _acce * Time.deltaTime;
		}
		transform.position = transform.position + _direction * (_speed * Time.deltaTime);
		Vector2 pointPosition = ( new Vector2 (transform.position.x - _terrainWidthHalf, transform.position.z - _terrainWidthHalf) )* (1 / _terrainWidthHalf);
		Debug.Log (pointPosition.ToString ());
		_radarController.UpdatePoint(_id, pointPosition);
	}
}
