using UnityEngine;
using System.Collections;

public class MicrophoneStarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioSource audioSource = GetComponent<AudioSource> ();
		audioSource.clip = Microphone.Start(null, true, 10, 44100);
		audioSource.Play ();		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
