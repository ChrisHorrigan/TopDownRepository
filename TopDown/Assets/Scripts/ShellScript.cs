using UnityEngine;
using System.Collections;

public class ShellScript : MonoBehaviour {
	private float lifeTime = 2.5f;
	private Material mat;
	private Color originalColor;
	private float fadePercent;
	private float deathTime;
	private bool fading;
	// Use this for initialization
	void Start () {
		mat = renderer.material;
		originalColor = mat.color;
		deathTime = Time.time + lifeTime;
		StartCoroutine ("Fade");
	}
	
	IEnumerator Fade(){
		while (true) 
		{
			yield return new WaitForSeconds(.2f);
			if(fading)
			{
				fadePercent+=Time.deltaTime;
				mat.color=Color.Lerp(originalColor,Color.clear,fadePercent);
				if(fadePercent>=1)
					Destroy(gameObject);
			}
			else if(Time.time>deathTime)
			{
				fading=true;
			}
			//
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.tag == "Ground") {

						rigidbody.Sleep ();
				}
		}
}
