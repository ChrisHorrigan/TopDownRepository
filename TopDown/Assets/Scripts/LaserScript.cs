using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {
	public LineRenderer laser;
	public LayerMask collisionMasko;
	// Use this for initialization
	public Vector3 mousePos;
	private Camera cam;
	public bool lightOn;
	private bool inGame=false;
//	void Awake()
//	{
//		//DontDestroyOnLoad (this.gameObject);//sadfa
//	}
	void Start () {
		inGame = false;
		lightOn = false;
		laser = GetComponent<LineRenderer> ();
		laser.enabled = false;
	}
	public void Initialize()
	{
		inGame = true;
		print ("Laserpointer.initialize");
		if(cam==null)
		cam = GameObject.Find ("GameCam").GetComponent<Camera>();
		lightOn = true;
		//cam = Camera.main;
	}
	// Update is called once per frame
	void Update () {
		if (inGame) {				
						if (lightOn) {
								laser.enabled = true;
								laser.SetPosition (0, transform.position);
								RaycastHit end;
								mousePos = Input.mousePosition;
								Vector3 mousePosreal = cam.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
								Vector3 toMouse = mousePosreal - transform.position;
								//Ray straight = new Ray (transform.position, transform.forward);
								//Ray straight = new Ray (transform.position, mousePosreal);
								Ray straight = new Ray (transform.position, toMouse / Vector3.Magnitude (toMouse));
								if (Physics.Raycast (straight, out end, Vector3.Magnitude (toMouse), collisionMasko))
		//laser.SetPosition (1, transform.forward*end.distance); better
										laser.SetPosition (1, transform.position + toMouse / Vector3.Magnitude (toMouse) * end.distance);
		//laser.SetPosition (1, transform.position + transform.forward * 5);
		else
										laser.SetPosition (1, mousePosreal);
								//dfssdafasdfasdfasdasdfasdfasdf
						} else
								laser.enabled = false;
				}
	}
}
//