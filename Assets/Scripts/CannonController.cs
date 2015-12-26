using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour {
	public float shift_radio_threshold = 0.3f;
	public float horizontal_rotate_speed = 1.0f;
	public float vertical_rotate_speed = 1.0f;
	public float vertical_angle_max = 60.0f;
	public float vertical_angle_min = -10.0f;

	private GameObject cannonShifter;
	private GameObject cannonBore;
	private float currentVertalRotate;

	// Use this for initialization
	void Start () {
		cannonShifter = GameObject.Find ("cannon-shifter");
		cannonBore = GameObject.Find ("cannon-bore");
		Debug.Assert (cannonShifter && cannonBore);
		currentVertalRotate = 0;
	}
	
	// Update is called once per frame
	void Update () {
		shift (new Vector2 (1, 1));
	}


	// move the cannon in horizontal and the gun in vertical
	// radioXY is centered and uniformed in [-1,1]
	public void shift(Vector2 radioXY)
	{

		if (radioXY.magnitude < shift_radio_threshold) {
			return;
		}
		cannonShifter.transform.Rotate (new Vector3(0,horizontal_rotate_speed*radioXY.x,0));

		//!careful for euler stays in [0-360]
		if (currentVertalRotate > vertical_angle_max && radioXY.y > 0 ||
		    currentVertalRotate < vertical_angle_min && radioXY.y < 0) {
			// forbid to rotate out of vertical field
		} else {
			float delta = vertical_rotate_speed*radioXY.y;
			currentVertalRotate += delta;
			cannonBore.transform.Rotate(new Vector3(0,0,-delta));
		}
	}
}
