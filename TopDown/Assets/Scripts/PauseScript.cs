﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;/// <summary>
/// Pause script.
/// </summary>
public class PauseScript : MonoBehaviour {
	PlayerController player;
	public Canvas canvas;
	public Canvas deathCanvas;
	public Canvas winCanvas;
	public bool paused;

	void Start () {
		canvas.enabled=false;
		paused=false;
		deathCanvas.enabled=false;
		winCanvas.enabled=false;
		player=GameObject.Find ("Player").GetComponent<PlayerController>();

		player.enabled=true;
		if(player.gameObject.GetComponentInChildren<LaserScript>()!=null)
			player.gameObject.GetComponentInChildren<LaserScript>().enabled=true;
		Time.timeScale=1;
		Screen.showCursor=false;//adfasfasdasdfasdfs
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Pause")&&!paused)
		{
			paused=true;
			canvas.enabled=true;
			player.enabled=false;//asdff
			if(player.gameObject.GetComponentInChildren<LaserScript>()!=null)
			player.gameObject.GetComponentInChildren<LaserScript>().enabled=false;
			Time.timeScale=0;
			Screen.showCursor=true;
		}
		else if (Input.GetButtonDown("Pause")&&paused)
			Resume ();
	}
	public void DeathScreen()//would have been better to enable panels rather than canvasses but whatever
	{
		deathCanvas.enabled=true;
		player.enabled=false;//asdff
		if(player.gameObject.GetComponentInChildren<LaserScript>()!=null)
			player.gameObject.GetComponentInChildren<LaserScript>().enabled=false;
		Time.timeScale=0;
		Screen.showCursor=true;
	}
	public void Win()
	{
		winCanvas.enabled=true;
		player.enabled=false;//asdff
		if(player.gameObject.GetComponentInChildren<LaserScript>()!=null)
			player.gameObject.GetComponentInChildren<LaserScript>().enabled=false;
		Time.timeScale=0;
		Screen.showCursor=true;
	}
	public void Resume()
	{
		paused=false;
		canvas.enabled=false;
		player.enabled=true;
		if(player.gameObject.GetComponentInChildren<LaserScript>()!=null)
		player.gameObject.GetComponentInChildren<LaserScript>().enabled=true;
		Time.timeScale=1;
		Screen.showCursor=false;//adfasfasdasdfasdfs
	}
	public void MainMenu()
	{
		GameObject[] gunsToDestroy=GameObject.FindGameObjectsWithTag("Weapon");
		foreach (GameObject g in gunsToDestroy)
			GameObject.Destroy(g);
		GameObject.Destroy(player.gameObject);
		GameObject.Destroy(GameObject.Find ("AmmoText"));
		GameObject.Destroy(GameObject.Find ("MenuManager"));

		Application.LoadLevel(0);///asdfasdfsdfg
	}
}
