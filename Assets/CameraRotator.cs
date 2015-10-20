using UnityEngine;
using System.Collections;

public class CameraRotator : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 axis = transform.TransformDirection (Vector3.up);
		transform.RotateAround (new Vector3 (0.0f, 0.0f, 0.0f), axis, 60.0f * Time.deltaTime);
	}
}
