using UnityEngine;
using System.Collections;

public class FileScript : MonoBehaviour {
	private TeamManager boss;
	// Use this for initialization
	void Start () {
		boss = this.gameObject.GetComponentInParent<TeamManager> ();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name=="Player")
		{
			boss.FileGrab();
			Destroy(this.gameObject);
		}
	}
}
