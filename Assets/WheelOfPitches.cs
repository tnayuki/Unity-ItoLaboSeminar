using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class WheelOfPitches : MonoBehaviour
{
	[DllImport("AudioPluginDemo")]
	private static extern float PitchDetectorGetFreq (int index);
	
	[DllImport("AudioPluginDemo")]
	private static extern int PitchDetectorDebug (float[] data);

	[SerializeField]
	private float
		rotationSpeed = 0.25f;
	[SerializeField]
	private GameObject
		noteCubePrefab;
	private GameObject[] cubes = new GameObject[12];
	private float[] noteCubeLastEmittedTimes = new float[12];

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < cubes.Length; i++) {
			cubes [i] = GameObject.Instantiate (noteCubePrefab);
			cubes [i].GetComponent<Renderer> ().material.SetColor ("_EmissionColor", HSVColor.HSVToRGB (360.0f * i / 12.0f, 1.0f, 1.0f));
			cubes [i].transform.parent = gameObject.transform;
			cubes [i].transform.localScale = new Vector3 (0.5f, 0.2f, 1.0f);
			cubes [i].transform.localPosition = new Vector3 (Mathf.Sin (i * Mathf.PI / 6), Mathf.Cos (i * Mathf.PI / 6));
			cubes [i].transform.localRotation = Quaternion.Euler (0, 0, -i * 360 / 12);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		float freq = PitchDetectorGetFreq (0);

		if (freq > 0.0f) {
			float noteval = 57.0f + 12.0f * Mathf.Log10 (freq / 440.0f) / Mathf.Log10 (2.0f);
			float f = Mathf.Floor (noteval + 0.5f);
			int noteIndex = (int)f % 12;

			if (Time.time > noteCubeLastEmittedTimes [noteIndex] + 0.5f) {
				noteCubeLastEmittedTimes [noteIndex] = Time.time;

				GameObject noteCube = GameObject.Instantiate (noteCubePrefab);
				noteCube.AddComponent<Rigidbody> ();
				noteCube.transform.position = cubes [noteIndex].transform.position + new Vector3 (0.0f, -0.2f, 0.0f);
				noteCube.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", HSVColor.HSVToRGB (360.0f * noteIndex / 12.0f, 1.0f, 1.0f));

				StartCoroutine ("DestroyNoteCube", noteCube);
			}
		}
	}

	IEnumerator DestroyNoteCube (GameObject noteCube)
	{
		yield return new WaitForSeconds (5.0f);
		Destroy (noteCube);
	}
}
