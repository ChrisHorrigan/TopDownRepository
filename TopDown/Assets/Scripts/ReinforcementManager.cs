using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReinforcementManager : MonoBehaviour {
	public List<Transform> spawns;
	private Transform[] basic;
	// Use this for initialization
	void Start () {
		basic=GetComponentsInChildren<Transform>();
		foreach (Transform t in basic)
		{
			if(!t.Equals(this.transform))
				spawns.Add (t);
		}
	}
	public List<Transform> GetLocations()
	{
		return spawns;
	}
	// Update is called once per frame

}
