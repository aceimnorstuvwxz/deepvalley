using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;


public class UIController : MonoBehaviour {
	public float load_interval = 1.0f;

	public Image fenhua_open;
	public Image fenhua_pos1;
	public Text textLoadingCount;
	public Camera mainCamera;

	// Use this for initialization
	void Start () {
	
		StartCoroutine ("loadingCount");
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

	}


	IEnumerator loadingCount() {
		for (int i = 0; i <= 101; i++) {
			if (i == 101) {
				textLoadingCount.enabled = false;
				mainCamera.farClipPlane = 1000f;
				mainCamera.GetComponent<BlurOptimized>().enabled = false;

			}
			textLoadingCount.text = "Loading..."+i.ToString()+"%";
			mainCamera.farClipPlane = 3f+i*1.5f;
			mainCamera.GetComponent<BlurOptimized>().blurSize = (100-i)*0.1f;

			yield return null;
		}
	}

}
