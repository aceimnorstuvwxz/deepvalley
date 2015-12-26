using UnityEngine;
using System.Collections;

public class joycontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	bool isRectContainsPoint(RectTransform rectTransform, Vector2 position, out Vector2 posRadio) 
	{
		bool res = false;
		Vector3[] corners = new Vector3[4];

		rectTransform.GetWorldCorners (corners);

		float xMin = corners [0].x;
		float xMax = corners [0].x;
		float yMin = corners [0].y;
		float yMax = corners [0].y;

		for (int i = 1; i < 4; i++) {
			var c = corners[i];
			xMin = Mathf.Min(xMin, c.x);
			xMax = Mathf.Max(xMax, c.x);
			yMin = Mathf.Min(yMin, c.y);
			yMax = Mathf.Max(yMax, c.y);
		}


		if (xMin != xMax && yMax != yMin) {
			posRadio.x = (position.x - xMin) / (xMax - xMin);
			posRadio.y = (position.y - yMin) / (yMax - yMin);
		} else {
			posRadio.x = posRadio.y = 0;
		}

		Debug.Log(string.Format("xMin={0}, xMax={1}, yMin={2}, yMax={3}, rx={4}, ry={5}",
		                        xMin, xMax, yMin, yMax, posRadio.x, posRadio.y));

		return (position.x >= xMin && position.x <= xMax &&
		        position.y >= yMin && position.y <= yMax);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount == 0) {
			return;
		}
		
		var rect = GetComponent<RectTransform> ();
		Vector2 posRadio;

		foreach (var touch in Input.touches) 
		{
			if (isRectContainsPoint(rect, touch.position, out posRadio)) {
				Debug.Log("Joy in");
			}
		}

	}
}
