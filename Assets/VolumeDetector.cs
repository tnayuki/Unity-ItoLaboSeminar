using UnityEngine;
using System.Collections;

public class VolumeDetector : MonoBehaviour {
	public float volume;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnAudioFilterRead(float[] data, int channels)
	{
		float total = 0.0f;
		for (int i = 0; i < data.Length; i++) {
			total += Mathf.Abs (data [i]);
		}
		total /= data.Length;
		
		volume = total;
	}
}
