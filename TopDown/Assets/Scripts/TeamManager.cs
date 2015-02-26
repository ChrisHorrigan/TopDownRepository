using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TeamManager : MonoBehaviour {
	public int levelID;
	public enum Objective {Clear,VIP,Hostage};
	public Objective obj;
	private Vector3 lastKnownPosition;
	public List<Enemy> minions;
	private Enemy[] basic;
	public PauseScript pause;
	void Start()
	{
		basic=gameObject.GetComponentsInChildren<Enemy>();//aaa
		foreach(Enemy e in basic){
			minions.Add (e);
		}

	}
	public void Alarm()
	{
		print ("ALARM");
		foreach(Enemy e in minions)
			e.BecomeAlarmed();
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

			GameControl.control.CompleteLevel(levelID);
			pause.Win ();
		}
	}



}
