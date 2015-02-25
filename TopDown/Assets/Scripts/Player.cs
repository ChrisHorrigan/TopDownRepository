using UnityEngine;
using System.Collections;

public class Player : Entity {

	public override void Die()
	{
		Debug.Log ("Player Dead");
		Destroy (gameObject);
	}
}
