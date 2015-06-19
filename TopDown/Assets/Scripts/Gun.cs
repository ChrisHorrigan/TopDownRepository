using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour {
	//This class would be used by AI as well so no references to input in here

	public int totalAmmo=120;
	public int magSize=30;
	public int magazine;
	public bool reloading;
	public float reloadTime=1f;
	private float reloadEnd;
	public LayerMask collisionMask;
	public Transform bulletSource;
	public float rpm;
	public enum GunType {Semi,Burst,Auto,Shotgun};
	public enum GunClass{Primary,Secondary};
	public enum Feed{Normal,Pump};
	public Feed feed;//
	public GunClass gunClass;
	public GunType gunType;
	public Transform shellEjectionPoint;
	public Transform[] bulletSources;
	public Rigidbody shell;


	public List<LineRenderer> tracer;


	private Vector3 bPath;
	private Quaternion spread;
	public float spreadAngle;
	public float jump=4f;
	public float recover=11f;
	public float damage=1;
	public float mainMinSpread = 0f;
	public float mainMaxSpread = 8f;
	public float noiseRadius=10f;
	public float minSpread;
	public float maxSpread;
	private float secondsBetweenShots;//
	private float nextPossibleShootTime;
	public bool active=false;
	private bool pointer;//I don't think this one is doing anything.
	public Vector3 pointAt;
	public Vector3 toTarget;
	public Toggle LaserToggle;
	public bool canSilence=true;
	public GameObject holder;

	public bool glitching=false;
	public Transform silencer;
	private bool silenced;
	private bool laserEquipped;
	public Transform laserPointer;
	private LaserScript laserRef;

	//public Transform laserT;

	//sdfaf
	/// <summary>
	/// S/	/// </summary>
	void Start(){//
		glitching=false;
		laserRef = laserPointer.GetComponent<LaserScript> ();
		laserEquipped = false;  
		silenced = false;
		minSpread = mainMinSpread;
		maxSpread = mainMaxSpread;
		spreadAngle = mainMinSpread;
		//HUD.text = magSize + " | " + totalAmmo;
		secondsBetweenShots = 60 / rpm;
		tracer=new List<LineRenderer>();
		foreach (Transform t in bulletSources)
		{
			tracer.Add (t.gameObject.GetComponent<LineRenderer>());
		}
		foreach(LineRenderer L in tracer){
			L.enabled=false;
		}
		//tracer = GetComponentsInChildren<LineRenderer> ();
		magazine = magSize;

		}
	public void showToggles()
	{
		LaserToggle.enabled = true;//sdf
	}
	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
		}
	void Update()//adfdfss
	{//
		if(glitching&&active){
			laserRef.lightOn=false;
			//print ("glitching");//fdasfsdfads
		}

		else if(active&&!glitching)
			laserRef.lightOn=true;
		toTarget = pointAt - bulletSource.position;
		//NEW:
		toTarget=toTarget/toTarget.magnitude;
		spreadAngle = Mathf.MoveTowards (spreadAngle , minSpread, recover*Time.deltaTime);  //movetowards is simply not working
	
	}
	public void ToggleSilencer()
	{
		silenced = !silenced;
		if (silenced) 
		{
						silencer.gameObject.renderer.enabled = true;
						damage -= 2;
						noiseRadius=noiseRadius/4;
				} 
		else 
		{
						silencer.gameObject.renderer.enabled = false;
			noiseRadius=noiseRadius*4;
			damage+=2;
				}
	}
	public void ToggleLaser()
	{
		laserEquipped = !laserEquipped;
		if (laserEquipped) 
		{
						laserPointer.gameObject.renderer.enabled = true;
		} 
		else 
		{
			laserPointer.gameObject.renderer.enabled = false;
		}

	}
	
	public void Holster(Transform where)
	{
		if(laserEquipped)
		laserRef.lightOn = false;
		active = false;
		glitching=false;
		transform.position = where.position;
		transform.rotation = where.rotation;
		transform.parent = where;
		//
	}
	public void ReadyUp(Transform where)
	{

		if (laserEquipped) {
			laserRef.Initialize();////
			laserRef.lightOn = true;
						//laserPointer.enabled = true;
						

				}
		active = true;
		transform.position = where.position;
		transform.rotation = where.rotation;
		transform.parent = where;

	}

	public void Shoot(){
			if (CanShoot ()) {
				
				if(holder.name=="Player")
					holder.GetComponent<PlayerController>().MakeNoise(noiseRadius); //make noise that the AI will want to investigate
				magazine--;
				foreach(LineRenderer l in tracer){
					float shotDistance = 20;
					Recoil ();
					Ray ray = new Ray (bulletSource.position, bPath*20);

					RaycastHit hit;
					
					if (Physics.Raycast (ray, out hit, shotDistance, collisionMask)) {
						shotDistance=hit.distance;
						if (hit.collider.GetComponent<Entity> ()&&!hit.collider.isTrigger) {
							hit.collider.GetComponent<Entity> ().TakeDamage (damage/tracer.Count,ray.direction*-1);	
						}//
					}
				StartCoroutine(RenderTracer (ray.direction*shotDistance,l));//asdfasd
				}
				nextPossibleShootTime = Time.time + secondsBetweenShots;
				if(!silenced)//SILENCER AFFECTING SHOT SOUNDS
					audio.Play ();
				Rigidbody newShell = Instantiate (shell, shellEjectionPoint.position, transform.rotation) as Rigidbody;
				newShell.AddForce (shellEjectionPoint.forward * Random.Range (150, 200) + bulletSource.forward * Random.Range (-10f, 10f));
							
			}
	}
	public void Recoil()
	{

		//spreadAngle += jump;
		spreadAngle=Mathf.Clamp (spreadAngle+jump, minSpread, maxSpread);
		//print (spreadAngle);
		float actualSpread=Random.Range (-spreadAngle,spreadAngle);
		//int leftOrRight = Random.Range (0, 2);//change it so it shoots anywhere within the range
		//if(leftOrRight==0)
			spread=Quaternion.AngleAxis(actualSpread,Vector3.up);
		//	else
			//spread=Quaternion.AngleAxis(spreadAngle*-1,Vector3.up);
		//bPath=spread*bulletSource.forward;
		bPath = spread * toTarget;

		//StartCoroutine ("Recovery");
	}
	public bool CanReload()
	{
		if (totalAmmo != 0 && magazine != magSize&&!reloading) {
			//reloading=true;
						return true;
				}
				else
						return false;
	}
	public void StartReload()
	{
		reloading = true;
		StartCoroutine ("DoReload");
	}
	public void FinishReload()
	{

		reloading = false;
		int transfer = magSize-magazine;
		magazine += transfer;
		totalAmmo -= (transfer);
		if (totalAmmo < 0) 
		{
			magazine+=totalAmmo;
			totalAmmo=0;
		}
	}
	public void ShootContinuous(){
		if (gunType != GunType.Semi) {//
			Shoot ();
		}
	}//
	public bool CanShoot(){
		bool canShoot;
		if(!glitching)
		 canShoot = true;
		else
			 canShoot=false;
		if (Time.time < nextPossibleShootTime)
						canShoot = false;
		if (magazine == 0)
						canShoot = false;
		if (reloading&&feed==Feed.Normal)
						canShoot = false;
		if(reloading&&feed==Feed.Pump)
		{
			StopCoroutine("DoReload");
			reloading=false;
			canShoot=true;
		}
		return canShoot;
	}
	IEnumerator RenderTracer(Vector3 hitPoint,LineRenderer tracer){//performance problems...

		tracer.enabled = true;
		bulletSource.gameObject.GetComponent<Light>().enabled=true;
		tracer.SetPosition (0, bulletSource.position);
		tracer.SetPosition (1, bulletSource.position+hitPoint);
		//yield return null;
	
		yield return null;//null for just a frame, but that's inconsistent .025f
		bulletSource.gameObject.GetComponent<Light>().enabled=false;
		tracer.enabled = false;
		}//
//	IEnumerator Recovery(){////
//		yield return new WaitForSeconds(.8f);
//		spreadAngle -= jump;
//		}
	IEnumerator DoReload(){//
		//print ("Reload started...");//
		//HUD.text = "Reloading...";//
		if(feed==Feed.Normal){
		yield return new WaitForSeconds (reloadTime);
		FinishReload ();
		//print ("Reloaded!");
		}
		else if(feed==Feed.Pump)
		{
			while(magazine<magSize)
			{
				if(totalAmmo>0){
					totalAmmo--;
					print ("put one in");
					magazine++;}
				else
					break;
				yield return new WaitForSeconds(reloadTime/magSize);
			}
			reloading=false;
		}
		//HUD.text = magazine + " | " + totalAmmo;
		}
}
