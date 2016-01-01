using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour {
	public float shift_radio_threshold = 0.3f;
	public float horizontal_rotate_speed = 5.0f;
	public float vertical_rotate_speed = 5.0f;
	public float vertical_angle_max = 45.0f;
	public float vertical_angle_min = -10.0f;
	public float sight_scope_var = 90f;

	private GameObject cannonShifter;
	private GameObject cannonBore;
	private float currentVertalRotate;
	private float moveRate;

	private RadarController radarController;
	private TerrainGenerator _terrainGenerator;


	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		cannonShifter = GameObject.Find ("cannon-shifter");
		cannonBore = GameObject.Find ("cannon-bore");
		mainCamera = GameObject.Find ("main-camera").GetComponent<Camera> ();
		currentVertalRotate = 0;
		moveRate = 1f;

		radarController = GameObject.Find ("Radar").GetComponent<RadarController> ();
		_terrainGenerator = GameObject.Find ("Terrain").GetComponent<TerrainGenerator> ();

		Invoke ("setPositionToValley", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
//		shift (new Vector2 (1, 1));
	}


	// move the cannon in horizontal and the gun in vertical
	// radioXY is centered and uniformed in [-1,1]
	public void shift(Vector2 radioXY)
	{

		if (radioXY.magnitude < shift_radio_threshold) {
			return;
		}
		cannonShifter.transform.Rotate (new Vector3(0,horizontal_rotate_speed*radioXY.x*moveRate,0));

		radarController.turnSightScope (cannonShifter.transform.eulerAngles.y);

		//!careful for euler stays in [0-360]
		if (currentVertalRotate > vertical_angle_max && radioXY.y > 0 ||
		    currentVertalRotate < vertical_angle_min && radioXY.y < 0) {
			// forbid to rotate out of vertical field
		} else {
			float delta = vertical_rotate_speed*radioXY.y*moveRate;
			currentVertalRotate += delta;
			cannonBore.transform.Rotate(new Vector3(0,0,-delta));
		}
	}

	public void swiftSightScope(float radio) 
	{
		moveRate = radio > 0.7f ? 0.2f :
			radio > 0.3f ? 0.6f : 1f;
		mainCamera.fieldOfView = 100 - radio * sight_scope_var;
	}

	public void setPositionToValley()
	{
		transform.position = _terrainGenerator.getValleyPosition();
		Debug.Log (transform.position.ToString ());
	}
}
