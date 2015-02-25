using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour {
	//This class would be used by AI as well so no references to input in here

	public int totalAmmo=120;
	public int magSize=30;
	public int magazine;
	public bool reloading;
	private float reloadTime=1f;
	private float reloadEnd;
	public LayerMask collisionMask;
	public Transform bulletSource;
	public float rpm;
	public enum GunType {Semi,Burst,Auto};
	public enum GunClass{Primary,Secondary};
	public GunClass gunClass;
	public GunType gunType;
	public Transform shellEjectionPoint;

	public Rigidbody shell;
	private LineRenderer tracer;
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

	public GameObject holder;


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
		laserRef = laserPointer.GetComponent<LaserScript> ();
		laserEquipped = false;  
		silenced = false;
		minSpread = mainMinSpread;
		maxSpread = mainMaxSpread;
		spreadAngle = mainMinSpread;
		//HUD.text = magSize + " | " + totalAmmo;
		secondsBetweenShots = 60 / rpm;
		if (GetComponent<LineRenderer> ())
						tracer = GetComponent<LineRenderer> ();
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
		toTarget = pointAt - bulletSource.position;
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

//	public void Attach(string what)
//	{
//		print ("attached");
//		GameObject attachment = GameObject.Find (what);
//		DontDestroyOnLoad (attachment);
//		if(what.Equals("LaserPointerPrimary"))
//		{
//			attachment.transform.position=laserSpot.position;
//			attachment.transform.rotation=laserSpot.rotation;
//			attachment.transform.parent=laserSpot;
//			laserPointer=attachment.GetComponent<LaserScript>();
//			laserEquipped=true;
//		}
//
//	}

	public void Holster(Transform where)
	{
		if(laserEquipped)
		laserRef.lightOn = false;
		active = false;
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
						
						//have to modify both x and z
						if(holder.name=="Player")
							holder.GetComponent<PlayerController>().MakeNoise(noiseRadius); //make noise that the AI will want to investigate
						
						Recoil ();
						//	print (bPath.ToString());
						magazine--;
						//HUD.text = magazine + " | " + totalAmmo;
						//Debug.Log (magazine+" | "+totalAmmo);
						Ray ray = new Ray (bulletSource.position, bPath);
		
						//Ray ray = new Ray (bulletSource.position, bulletSource.forward);
						RaycastHit hit;
						float shotDistance = 20;
						if (Physics.Raycast (ray, out hit, shotDistance, collisionMask)) {
								shotDistance = hit.distance;
								if (hit.collider.GetComponent<Entity> ()&&!hit.collider.isTrigger) {
										hit.collider.GetComponent<Entity> ().TakeDamage (damage);	
								}
						}
						//Debug.DrawRay (ray.origin, ray.direction * shotDistance, Color.red, 1);
						nextPossibleShootTime = Time.time + secondsBetweenShots;
						if(!silenced)//SILENCER AFFECTING SHOT SOUNDS
						audio.Play ();
						if (tracer) {
								StartCoroutine ("RenderTracer", ray.direction * shotDistance);
						}
						Rigidbody newShell = Instantiate (shell, shellEjectionPoint.position, transform.rotation) as Rigidbody;
						newShell.AddForce (shellEjectionPoint.forward * Random.Range (150, 200) + bulletSource.forward * Random.Range (-10f, 10f));
						//somewhat random shell dropping trajectories          ^

				} 

		//else if (!CanShoot()&&!reloading)
			//play a dry fire noise
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
		if (gunType == GunType.Auto) {
			Shoot ();
		}
	}//
	private bool CanShoot(){
		bool canShoot = true;
		if (Time.time < nextPossibleShootTime)
						canShoot = false;
		if (magazine == 0)
						canShoot = false;
		if (reloading)
						canShoot = false;
		return canShoot;
	}
	IEnumerator RenderTracer(Vector3 hitPoint){
		tracer.enabled = true;
		tracer.SetPosition (0, bulletSource.position);
		tracer.SetPosition (1, bulletSource.position+hitPoint);
		//yield return null;
		yield return new WaitForSeconds(.025f);//null for just a frame, but that's inconsistent
		tracer.enabled = false;
		}//
//	IEnumerator Recovery(){////
//		yield return new WaitForSeconds(.8f);
//		spreadAngle -= jump;
//		}
	IEnumerator DoReload(){//
		print ("Reload started...");//
		//HUD.text = "Reloading...";//
		yield return new WaitForSeconds (reloadTime);
		FinishReload ();
		print ("Reloaded!");
		//HUD.text = magazine + " | " + totalAmmo;
		}
}
