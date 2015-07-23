using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	public Gun gun;
	public Gun primaryGun;
	public float health;

	public Transform handSpot;
	public Vector3 startPoint;
	public virtual void Start()
	{
		//print ("when entity starts");//as
		//transform.position = startPoint;
		gun = primaryGun;
		gun.ReadyUp (handSpot);
		gun.holder=this.gameObject;
		}//
	public virtual void Update(){
		handSpot.rotation=transform.rotation;//dd
//		if (controller.velocity.magnitude != 0) {//fff
//						gun.minSpread = gun.mainMaxSpread / 2;
//						gun.maxSpread = gun.mainMaxSpread + 5f;
//
//				} 
//		else {
//			gun.minSpread=gun.mainMinSpread;
//			gun.maxSpread=gun.mainMaxSpread;
//		
//				}//
	}
	public virtual void TakeDamage(float damage,Vector3 direction)//virtual lets us override it in subclasses  asdfasdfasdfa
	{//
		print ("hit!");
		health -= damage;
		Debug.Log (health);
		if (health <= 0)
			Die ();
	}
	IEnumerator Death()
	{
		yield return new WaitForSeconds(.25f);
		Destroy (gameObject);
	}
	public virtual void Die()
	{
		StartCoroutine("Death");
		//Destroy (gameObject);
	}

}
