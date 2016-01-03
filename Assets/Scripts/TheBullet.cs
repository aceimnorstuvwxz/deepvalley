using UnityEngine;
using System.Collections;

public class TheBullet : MonoBehaviour {
	private GameObject _explosion;
	public AudioClip sound_boom;

	private TerrainGenerator _terrainGenerator;
	private float _width;

//	private Rigidbody
	// Use this for initialization
	void Start () {
		
		_explosion = Instantiate (Resources.Load("Explosion"))as GameObject;
		//		_explosion.transform.SetParent (transform);

		_terrainGenerator = GameObject.Find ("Terrain").GetComponent<TerrainGenerator> ();
		_width = _terrainGenerator.getTerrainWidth ();
	}

	public void SetBeginSpeed(Vector3 speed)
	{
		gameObject.GetComponent<Rigidbody> ().velocity = speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < 0 || transform.position.z < 0 || transform.position.x > _width || transform.position.z > _width) {
			Debug.Log("bullet out");
			Destroy(gameObject);
		}
	}

	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("trigger enter");
		if (other.gameObject.tag == "Flying") {
			boom ();
		}
	}

	public void boom()
	{
		
		_explosion.transform.position = transform.position;

		_explosion.GetComponent<ExplosionController>().boom();


		GetComponent<AudioSource> ().PlayOneShot (sound_boom);

	}
}
