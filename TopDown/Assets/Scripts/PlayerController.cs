using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : Entity {
	public GUIText HUD;
	public Texture crosshairs;
	private Quaternion targetRotation;
	public float walkSpeed = 5;
	public float runSpeed=8;
	public float rotationSpeed=10;
	public float crossX;
	public float crossY;
	public float walkPenalty=5f;
	private float radius;
	public Transform backSpot;
	public Transform hipSpot;
	private Animator animator;
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
	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
		animator=GetComponent<Animator>();
	}
	public void EnterScene()
	{
		//primaryGun = GameObject.Find ("M16").GetComponent<Gun>();
		//secondaryGun = GameObject.Find ("G17").GetComponent<Gun>();fd

		primaryGun.holder = this.gameObject;
		secondaryGun.holder = this.gameObject;
		print ("entered scene");
//		base.Start ();
//		secondaryGun.Holster (hipSpot);
		StartCoroutine ("setCam");
		//inGame = true;
		//cam = Camera.main;
		HUD.enabled = true;

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
	IEnumerator setCam()
	{
		yield return new WaitForSeconds (.25f);
		base.Start ();
		transform.position = GameObject.Find ("PlayerSpawn").transform.position;
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
		DontDestroyOnLoad (HUD.gameObject);
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
			if (controller.velocity.magnitude != 0) {//
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
						gun.pointAt = mousePos;
						/*if (Input.GetKey (KeyCode.K))
						Die ();*/
						if (!gun.reloading)
								HUD.text = gun.magazine + " | " + gun.totalAmmo;
						else
								HUD.text = "Reloading...";
						if (Input.GetButtonDown ("Reload")) {
								if (gun.CanReload ())
										gun.StartReload ();
						}//

						if (Input.GetButtonDown ("Shoot")) {
								gun.Shoot ();
						} else if (Input.GetButton ("Shoot")) {
								gun.ShootContinuous ();
						}



						if (Input.GetKeyDown (KeyCode.Alpha1)) {
								if (gun.gunClass != Gun.GunClass.Primary) {
										HolsterGun (gun);
										ReadyGun (primaryGun);
										gun = primaryGun;
								}
						}
						if (Input.GetKeyDown (KeyCode.Alpha2)) {
								if (gun.gunClass != Gun.GunClass.Secondary) {
										HolsterGun (gun);
										ReadyGun (secondaryGun);
										gun = secondaryGun;
								}
						}
						//
						//
						ControlMouse ();
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

	void ControlMouse(){//movement with WASD, look with mouse


		targetRotation = Quaternion.LookRotation (mousePos-new Vector3(transform.position.x,0,transform.position.z));
		//transform.eulerAngles=Vector3.up*Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotationSpeed*Time.deltaTime);
		transform.eulerAngles = Vector3.up*targetRotation.eulerAngles.y;//infinitely faster
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));//gets input
		Vector3 motion = input;

		motion*=(Mathf.Abs (input.x)==1&&Mathf.Abs (input.z)==1)?.7f:1;//there's another way, look into normalizing
		speed=motion.magnitude;
		if(speed>0)
			animator.SetBool("Moving",true);
		else
			animator.SetBool("Moving",false);
	motion*=(Input.GetButton("Run"))?runSpeed:walkSpeed; //faster if running
		motion += Vector3.up * -8; //gravity (only useful for jumping)

		controller.Move(motion*Time.deltaTime);//actually move

		}


}
