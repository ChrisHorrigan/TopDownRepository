using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : Entity {
	public GUIText HUD;
	public GUIText HealthBar;
	public Texture crosshairs;
	private Quaternion targetRotation;
	public float walkSpeed = 5;
	public float runSpeed=8;
	public float rotationSpeed=10;
	public float crossX;
	public float crossY;//
	public float walkPenalty=1f;
	private float radius;
	public Transform backSpot;
	public Transform hipSpot;
	private Animator animator;//gffff
	public Transform eyes;
	//public Vector3 startPoint;
	public Gun secondaryGun;
	public bool inGame;
	//private bool reloading;
	private Camera cam;//for converting mouse position to real space
	public Vector3 mousePos;
	public CharacterController controller;
	public TeamManager god;
	private PauseScript menu;
	public float speed;
	public LayerMask checkMask;//asdfads
	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
		animator=GetComponent<Animator>();
	}
	public void EnterScene()
	{
		//primaryGun = GameObject.Find ("M16").GetComponent<Gun>();
		//secondaryGun = GameObject.Find ("G17").GetComponent<Gun>();fdfff

		primaryGun.holder = this.gameObject;
		secondaryGun.holder = this.gameObject;
		print ("entered scene");
//		base.Start ();
//		secondaryGun.Holster (hipSpot);
		StartCoroutine ("setCam");
		//inGame = true;
		//cam = Camera.main;
		HUD.enabled = true;
		HealthBar.enabled=true;

		}//
	public void MakeNoise(float radius)
	{
		god.DetectNoise(transform.position,radius);
	}
	public void PrimarySetup(string whatGun)
	{
		Debug.Log ("this");//s
		primaryGun= GameObject.Find (whatGun).GetComponent<Gun>();
	}
	public void SecondarySetup(string whatGun)
	{
		secondaryGun= GameObject.Find (whatGun).GetComponent<Gun>();
	}
	IEnumerator setCam()//dffh
	{
		yield return new WaitForSeconds (.25f);
		base.Start ();
		transform.position = GameObject.Find ("TeamManager").transform.position;
		menu=GameObject.Find ("GameManager").GetComponent<PauseScript>();
		god=GameObject.Find ("TeamManager").GetComponent<TeamManager>();
		secondaryGun.Holster (hipSpot);//adsffghgh
		print ("cam set over here");
		inGame = true;
		cam = GameObject.Find("GameCam").GetComponent<Camera>();
		}
	void Start () {
		print ("ran");
		controller = GetComponent<CharacterController>();
		inGame = false;
		HUD = GameObject.Find ("AmmoText").GetComponent<GUIText> ();
		HUD.enabled = false;
		HealthBar=GameObject.Find ("HealthText").GetComponent<GUIText>();
		HealthBar.enabled=false;
		DontDestroyOnLoad (HUD.gameObject);
		DontDestroyOnLoad(HealthBar.gameObject);//asdfasdfff
		startPoint.x = 10;
		startPoint.z = 10;
		startPoint.y = 2;
	
		//cam = Camera.main;sdgsfg
	}//sdf
    void OnGUI()
	{//
		if (inGame) {
						radius = gun.spreadAngle * 3 + 70;
						GUI.Label (new Rect (crossX - radius / 2, Screen.height - crossY - radius / 2, radius, radius), crosshairs);//follows the mouse, not dynamic
				}
		}
	public override void Die()
	{
		menu.DeathScreen();
	}
	void Update () {
		if (inGame) {
						base.Update ();//

			HealthBar.text=health.ToString ();
			if (controller.velocity.magnitude != 0) {//
				//print (controller.velocity.magnitude.ToString());//
				gun.minSpread = gun.mainMaxSpread / 2;
				gun.maxSpread = gun.mainMaxSpread + walkPenalty;
				
			} 
			else {
				gun.minSpread=gun.mainMinSpread;
				gun.maxSpread=gun.mainMaxSpread;
				
			}//sdfa

						Screen.showCursor = false;
						mousePos = Input.mousePosition;

						crossX = mousePos.x;
						crossY = mousePos.y;

						mousePos = cam.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
			gun.pointAt.x = mousePos.x;
			gun.pointAt.z=mousePos.z;
			gun.pointAt.y=gun.bulletSource.position.y;
						/*if (Input.GetKey (KeyCode.K))
						Die ();*/

						//if (!gun.reloading)
						if(!gun.reloading)
							HUD.color=Color.white;	
						else if (gun.reloading&&gun.feed!=Gun.Feed.Pump)//
				HUD.color=Color.yellow;//
						HUD.text = gun.magazine + " | " + gun.totalAmmo;
						//else
								//HUD.text = "Reloading...";
						if (Input.GetButtonDown ("Reload")) {
								if (gun.CanReload ())
										gun.StartReload ();
						}//

						if (Input.GetButtonDown ("Shoot")) {
				if(gun.CanShoot())
				{
					if(gun.gunClass==Gun.GunClass.Primary)
						animator.SetTrigger("ShootAR");
					else if(gun.gunClass==Gun.GunClass.Secondary)
						animator.SetTrigger("ShootPistol");
				}
								gun.Shoot ();
								
							
								
								
						} 
			else if (Input.GetButton ("Shoot")) {
				if(gun.CanShoot())
				{
					if(gun.gunClass==Gun.GunClass.Primary)
						animator.SetTrigger("ShootAR");
					else if(gun.gunClass==Gun.GunClass.Secondary)
						animator.SetTrigger("ShootPistol");
				}
								gun.ShootContinuous ();

						}



						if (Input.GetKeyDown (KeyCode.Alpha1)) {
								if (gun.gunClass != Gun.GunClass.Primary) {

										HolsterGun (gun);

										ReadyGun (primaryGun);
					animator.SetFloat("WeaponID",0);
										gun = primaryGun;
								}
						}//
						if (Input.GetKeyDown (KeyCode.Alpha2)) {
								if (gun.gunClass != Gun.GunClass.Secondary) {

										HolsterGun (gun);

										ReadyGun (secondaryGun);
					animator.SetFloat("WeaponID",1);
										gun = secondaryGun;
								}
						}
						//
						//

						ControlMouse ();
			GlitchCheck();
				}


		//ControlWASD ();
	}

	void HolsterGun(Gun toHolster)
	{

		if (toHolster.gunClass == Gun.GunClass.Primary)
						toHolster.Holster (backSpot);
		else if (toHolster.gunClass == Gun.GunClass.Secondary)
						toHolster.Holster (hipSpot);
	}

	void ReadyGun(Gun toReady)
	{
			toReady.ReadyUp (handSpot);
	}
	void GlitchCheck()
	{
		Vector3 direction = eyes.position-gun.bulletSource.position;
		direction=direction/direction.magnitude;
		Ray ray = new Ray(eyes.position,direction*-5);//*20
		//Debug.DrawRay(eyes.position,direction*-5);
		RaycastHit hit;///ffffff
		if(Physics.Raycast(ray,out hit,checkMask)){
			//print (hit.collider.ToString());
			if(hit.distance<Vector3.Distance(eyes.position,gun.bulletSource.position))
				gun.glitching=true;
			else
			gun.glitching=false;
		}
	}
	void ControlMouse(){//movement with WASD, look with mouse


		targetRotation = Quaternion.LookRotation (mousePos-new Vector3(transform.position.x,0,transform.position.z));
		//transform.eulerAngles=Vector3.up*Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotationSpeed*Time.deltaTime);
		transform.eulerAngles = Vector3.up*targetRotation.eulerAngles.y;//faster
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));//gets input
		Vector3 motion = input;

		motion*=(Mathf.Abs (input.x)==1&&Mathf.Abs (input.z)==1)?.7f:1;//there's another way, look into normalizing
		speed=motion.magnitude;
		if(speed>0)
			animator.SetBool("Moving",true);
		else
			animator.SetBool("Moving",false);
		motion*=walkSpeed; //faster if running
		motion += Vector3.up * -8; //gravity (only useful for jumping)

		controller.Move(motion*Time.deltaTime);//actually movefffff

		}


}
