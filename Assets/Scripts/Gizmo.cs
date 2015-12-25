using UnityEngine;
using System.Collections;
using System;

//Gizmos are used to give visual debugging or setup aids in the scene view.

public class Gizmo : MonoBehaviour {

	public float gizmoSize = .75f;
	public Color gizmoColor = Color.yellow;

	void Start()
	{
		Debug.Log ("hello world!");
	}


	void OnDrawGizmos()
	{
		Gizmos.color = gizmoColor;
		Gizmos.DrawWireSphere (transform.position, gizmoSize);
	}
}
