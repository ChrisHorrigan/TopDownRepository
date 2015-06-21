using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TeamManager : MonoBehaviour {
	public int levelID;
	public enum Objective {Clear,VIP};
	public Objective obj;
	private Vector3 lastKnownPosition;
	public List<Enemy> minions;
	private Enemy[] basic;
	public PauseScript pause;
	private bool VIPdown;
	private SphereCollider endPoint;
	private Color lightColor;
	public List<Light> lights;
	private Light[] lightsa;
	private bool alarmActivated;
	private float t;
	private float p;
	private bool red;
	private ReinforcementManager reinforcers;
	public List<Transform> spawnPoints;
	private GameObject additionalEnemy;
	void Start()
	{
		additionalEnemy=(GameObject)Resources.Load ("Enemy");
		reinforcers = GetComponentInChildren<ReinforcementManager>();
		spawnPoints=reinforcers.GetLocations();
		red=false;
		t=0;
		p=20;
		lightColor=Color.white;
		alarmActivated=false;
		endPoint=GetComponent<SphereCollider>();
		lightsa=GetComponentsInChildren<Light>();
		foreach(Light l in lightsa){
			if(!l.Equals(this.gameObject.GetComponent<Light>()))
				lights.Add(l);
		}//
		VIPdown=false;
		basic=gameObject.GetComponentsInChildren<Enemy>();//aaa
		foreach(Enemy e in basic){
			minions.Add (e);
		}

	}
	void Update()
	{

		foreach(Light f in lights){
			f.color=lightColor;
			if(alarmActivated)
			{

				lightColor=Color.Lerp(Color.white,Color.red,t);
				if(red)
					t-=Time.deltaTime/p;
				else
					t+=Time.deltaTime/p;//
				if(t>=1){

					red=true;
				}
				else if (t<=0)
					red=false;


//				if(t<1)
//					t+=Time.deltaTime/p;
//				else 
//					t=0;
			}

		}
	}
	public void Alarm()
	{
		//print ("ALARM");
		alarmActivated=true;
		foreach(Enemy e in minions)
			e.BecomeAlarmed();
		//if it's a VIP mission, reinforcements should come.  in any case, the lights should start to pulse red
		if(obj==TeamManager.Objective.VIP)
		{
			foreach(Transform sp in spawnPoints)
			{
				GameObject newGuyo=(GameObject)Instantiate(additionalEnemy,sp.position,sp.rotation);
				Enemy newGuy=newGuyo.GetComponent<Enemy>();
				newGuy.transform.SetParent(this.transform);
				minions.Add(newGuy);
				newGuy.BecomeAlarmed();//fffff
				newGuy.CallToInvestigate();
			}
		}
	}
	public Vector3 LastKnownPosition{
		get{
			return lastKnownPosition;
		}
		set{
			lastKnownPosition=value;

		}
	}
	public void DetectNoise(Vector3 center,float radius)
	{
		lastKnownPosition=center;
		foreach(Enemy e in minions)
		{
			if(e.status!=Enemy.Status.investigating&&e.status!=Enemy.Status.combat&&Vector3.Distance(e.transform.position,center)<=radius)
				e.CallToInvestigate();
		}
	}
	public void ReceiveCall()
	{
		foreach(Enemy e in minions)
		{
			if(e.status!=Enemy.Status.investigating&&e.status!=Enemy.Status.combat)
				e.CallToInvestigate();
		}
	}
	public void LoseMember(Enemy e)
	{

		minions.Remove(e);
		if(minions.Count<=0&&obj==Objective.Clear)
		{

			GameControl.control.CompleteLevel(levelID);//ss
			pause.Win ();
		}
	}
	public void VIPDeath()
	{
		VIPdown=true;
		//GameControl.control.CompleteLevel(levelID); it's not over yet!
		//pause.Win ();
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player")&&VIPdown)
		{
			GameControl.control.CompleteLevel(levelID); 
			pause.Win ();
		}
	}


}
