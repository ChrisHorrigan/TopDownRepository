using UnityEngine;
using System.Collections;

public class VIP : Entity {
	private TeamManager boss;
	// Use this for initialization
	public override void Start () {
		boss = this.gameObject.GetComponentInParent<TeamManager> ();
	}
	public override void Die()
	{
		base.Die ();
		boss.VIPDeath();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
