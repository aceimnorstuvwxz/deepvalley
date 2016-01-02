using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

	public GameObject explosion_prefab;


	// Use this for initialization
	void Start () {
	
	}

	public void boom()
	{
		Debug.Log ("boom");
		int N = 10;
		GameObject[] nodes = new GameObject[N];
		for (int i = 0; i < N; i++) {
			var node = Instantiate(explosion_prefab) as GameObject;
			node.transform.SetParent(transform);
			node.transform.localPosition = Vector3.zero;
			nodes[i] = node;

			node.GetComponent<Rigidbody>().velocity = (new Vector3(Random.value-0.5f, Random.value-0.5f, Random.value-0.5f)).normalized * 1f;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
