using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	public Image fenhua_open;
	public Image fenhua_pos1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void swiftSightScope(float radio) {

		// fenhua 
		float alphaOfMiddle = 1f - Mathf.Abs (radio - 0.5f) / 0.5f;
		fenhua_open.color = new Color (1, 1, 1, alphaOfMiddle);

		float alphaOfSecond = radio > 0.5f ? (1f - Mathf.Abs (radio - 1f) / 0.5f) : 0f;
		fenhua_pos1.color = new Color (1, 1, 1, alphaOfSecond);

		// camera position
	}

}
