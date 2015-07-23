using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameCamera : MonoBehaviour {
	private Transform target;
	// Use this for initialization
	private Vector3 cameraTarget;
	public AudioSource[] audio;
	private TeamManager teammanager;
	public Slider SFX;
	public Slider Music;
	private Vector3 mousePosition;
	private Vector3 playerPos;
	private PlayerController player;
	void Start () {
		teammanager=GameObject.Find ("TeamManager").GetComponent<TeamManager>();
		target = GameObject.Find ("Player").transform;
		player=target.gameObject.GetComponent<PlayerController>();
		audio=GetComponents<AudioSource>();
		foreach (AudioSource a in audio)
		{
			a.ignoreListenerVolume=true;//
		}
		Music.value=PlayerPrefs.GetFloat("music");
		SFX.value=PlayerPrefs.GetFloat("SFX");
		StartCoroutine("MusicDelay");
	}
	private IEnumerator MusicDelay()
	{
		yield return new WaitForSeconds(1);
		if(!teammanager.alarmActivated)
			audio[0].Play();
	}
	// Update is called once per framefdgssfg
	void Update () {
//		foreach(AudioSource a in audio)
//		{
//			a.volume=PlayerPrefs.GetFloat("Music");
//		}


		mousePosition=player.mousePos;
		playerPos=target.position;
		if(teammanager.alarmActivated&&!audio[1].isPlaying)
		{
			audio[0].Pause ();
			audio[1].Play ();
		}
		//
		//cameraTarget = new Vector3 (target.position.x, transform.position.y, target.position.z);
		cameraTarget.x=(mousePosition.x+playerPos.x)/2;
		cameraTarget.z=(mousePosition.z+playerPos.z)/2;
		cameraTarget.y=transform.position.y;
		transform.position = Vector3.Lerp (transform.position, cameraTarget, Time.deltaTime * 3);//smoothing
	}
	public void ChangeMusicVolume(float vol)
	{
		print ("change in music vol");
		PlayerPrefs.SetFloat("music",vol);
		foreach(AudioSource a in audio)
		{
			a.volume=vol;//

		}
	}
	public void ChangeSFXVolume(float vol)
	{
		print ("change in SFX vol");
		PlayerPrefs.SetFloat("SFX",vol);
		AudioListener.volume=vol;
	}
}
//