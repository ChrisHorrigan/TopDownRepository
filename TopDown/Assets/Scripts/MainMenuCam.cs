using UnityEngine;
using System.Collections;

public class MainMenuCam : MonoBehaviour {
	private AudioSource audio;
	private GameControl control;
	// Use this for initialization
	void Start () {
		control=GameObject.Find ("GameController").GetComponent<GameControl>();
		audio=GetComponent<AudioSource>();
		if(control.firstRun)
			audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
