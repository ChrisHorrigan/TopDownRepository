using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Entity {
	public bool visual;
	public NavMeshAgent agent;
	public Transform target;
	public float FOV=100f;
	public float range=10f;
	private SphereCollider sphere;
	//private bool moving; probably won't need this
	private Light light;
	public LayerMask layermasko;
	public float rotationSpeed=2;
	private bool fightingPlayer;
	private Vector3 destination;
	private bool alarmed;
	private bool sentAlarm;
	private TeamManager boss;
	public int nextNode;
	public List<Node> nodes;
	private bool lookingAround;
	private Node[] basic;
	public enum Status{patrol,investigating,combat,search};//search temporarily nothing
	public Status status;
	private Quaternion targetRotation;
	private Vector3 newForward;
	private Vector3 oldForward;
	private bool returningToPost;
	void Start()
	{
		base.Start ();
		lookingAround=false;
		returningToPost=false;
		basic=gameObject.GetComponentsInChildren<Node>();
		nextNode=0;
		foreach(Node n in basic)
		{
			nodes.Add (n);
			n.transform.parent=null;
		}

		visual=false;
		boss = this.gameObject.GetComponentInParent<TeamManager> ();
		target = GameObject.Find ("Player").GetComponent<Transform> ();
		alarmed=false;
		sentAlarm=false;
		//fightingPlayer = false;
		status = Status.patrol;
		//moving = true;
		sphere = handSpot.gameObject.GetComponent<SphereCollider> ();
		light = GetComponent<Light> ();
		light.range = range;
		light.spotAngle = FOV;
		sphere.radius = range;
		agent = GetComponent<NavMeshAgent> ();
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject == target.gameObject) {//this checks if the player is within the radius of the enemy

			Vector3 direction=target.position-transform.position;
			float angle = Vector3.Angle(direction,transform.forward);
			if(angle<=FOV/2)//this means the player is within the cone
			{

				RaycastHit hit;
				if(Physics.Raycast(transform.position,direction,out hit,range, layermasko))//raycast hit something, could be a wall
				{

					if(hit.collider.gameObject==target.gameObject){//yup, seeing the player
						StopCoroutine("LookAround");//fffff
						StopCoroutine("LookAroundIdle");

						lookingAround=false;
						//moving=false;//this makes him not aim at the player at close range ^^^  Won't be a problem when the model is right
						//fightingPlayer=true;
						visual=true;
						status=Status.combat;

						boss.LastKnownPosition=target.position;

						if(angle<=15f)
						gun.ShootContinuous();
						if(gun.magazine==0&&!gun.reloading)
							gun.StartReload();

					}
					else {
						if(status==Status.combat){
							if(!alarmed&&!sentAlarm){
								sentAlarm=true;
								StartCoroutine("RaiseAlarm");
							}
							MakeCall();
							status=Status.investigating;}}
				}

			}
			else {
				visual=false;
				if(status==Status.combat){
					if(!alarmed&&!sentAlarm){
						sentAlarm=true;
						StartCoroutine("RaiseAlarm");
					}
					MakeCall();
					status=Status.investigating;}}
		}						
	}
	public void CallToInvestigate()
	{
		if(status!=Status.combat){
			StopCoroutine("LookAround");
			StopCoroutine("LookAroundIdle");
			lookingAround=false;
			status=Status.investigating;
		}
	}
	public void MoveOn()
	{
		//asdfasdfzf

		if(nextNode+1>=nodes.Count)
			nextNode=0;
		else
			nextNode++;
	}
	IEnumerator RaiseAlarm(){
		yield return new WaitForSeconds(3f);


		boss.Alarm();
	}
	public void BecomeAlarmed()
	{
		alarmed=true;
		//move speed increases
	}
	public override void TakeDamage(float damage,Vector3 direction)
	{
		base.TakeDamage(damage,direction);
		if(status==Status.patrol)
		{
			StopCoroutine("LookAround");
			StopCoroutine("LookAroundIdle");
			lookingAround=false;
			if(!lookingAround)
				StartCoroutine("CheckBack",direction);
		}
		//transform.forward=direction; this was just to make sure the direction detection works
	}

	public IEnumerator CheckBack(Vector3 direction)
	{
		float elapsedTime=0f;
		lookingAround=true;
		newForward=direction;
		oldForward=transform.forward;
		while(elapsedTime<=1f)
		{
			transform.forward=Vector3.Slerp(oldForward,newForward,elapsedTime);
			elapsedTime+=Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		lookingAround=false;

	}
	public override void Die ()
	{
		base.Die ();
		boss.LoseMember(this);
		StopCoroutine("RaiseAlarm");
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == target.gameObject) {

			visual=false;
			if(status==Status.combat){
				if(!alarmed&&!sentAlarm){
					sentAlarm=true;
					StartCoroutine("RaiseAlarm");
				}
				MakeCall();
				status=Status.investigating;}
			//moving=true;
			//fightingPlayer=false;
			//status=Status.search;
			//asdf
		}
	}
	void MakeCall()
	{
		boss.ReceiveCall();//
		print ("Need backup!");
	}


	IEnumerator LookAround()
	{
		float elapsedTime=0f;
		lookingAround=true;
		newForward=Quaternion.AngleAxis(90,Vector3.up)*transform.forward;
		oldForward=transform.forward;
		while(elapsedTime<=1.5f)
		{
			transform.forward=Vector3.Slerp(oldForward,newForward,elapsedTime);
			elapsedTime+=Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	//	transform.forward=newForward;asddasas

	//	yield return new WaitForSeconds(1f);
		elapsedTime=0f;
		newForward=Quaternion.AngleAxis(179,Vector3.down)*transform.forward;
		oldForward=transform.forward;


		while(elapsedTime<=2f)
		{
			transform.forward=Vector3.Slerp(oldForward,newForward,elapsedTime);
			elapsedTime+=Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	//	transform.forward=newForward;

	//	yield return new WaitForSeconds(1f);

		elapsedTime=0f;
		newForward=Quaternion.AngleAxis(91,Vector3.up)*transform.forward;
		oldForward=transform.forward;
		while(elapsedTime<=1.5f)
		{
			transform.forward=Vector3.Slerp(oldForward,newForward,elapsedTime);
			elapsedTime+=Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	//	transform.forward=newForward;

	//	yield return new WaitForSeconds(1f);
		agent.updateRotation=true;
		status=Status.patrol;

		lookingAround=false;
	}
//	IEnumerator ReturnToPost()
//	{
//		returningToPost=true;
//		Vector3 oldForward=transform.forward;
//		Vector3 targetForward=nodes[nextNode].transform.forward;//this will have to get lerped of coursegggg
//		float elapsedTime=0f;
//		while(elapsedTime<=1f){
//			transform.forward=Vector3.Slerp (oldForward,targetForward,Time.deltaTime);
//			elapsedTime+=Time.deltaTime;
//			yield return new WaitForEndOfFrame();
//		}
//		returningToPost=false;
//	}
	IEnumerator LookAroundIdle()
	{
		float elapsedTime=0f;
		lookingAround=true;
		newForward=nodes[nextNode].transform.forward;
		oldForward=transform.forward;
		while(elapsedTime<=1f)
		{
			transform.forward=Vector3.Slerp(oldForward,newForward,elapsedTime);
			elapsedTime+=Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		elapsedTime=0f;
		newForward=Quaternion.AngleAxis(45,Vector3.up)*transform.forward;
		oldForward=transform.forward;
		while(elapsedTime<=1.5f)
		{
			transform.forward=Vector3.Slerp(oldForward,newForward,elapsedTime);
			elapsedTime+=Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		//	transform.forward=newForward;asddasas
		
		//	yield return new WaitForSeconds(1f);fffffj
		elapsedTime=0f;
		newForward=Quaternion.AngleAxis(270,Vector3.up)*transform.forward;
		oldForward=transform.forward;
		
		
		while(elapsedTime<=2f)
		{
			transform.forward=Vector3.Slerp(oldForward,newForward,elapsedTime);
			elapsedTime+=Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		//	transform.forward=newForward;
		
		//	yield return new WaitForSeconds(1f);
		
		elapsedTime=0f;
		newForward=Quaternion.AngleAxis(45,Vector3.up)*transform.forward;
		oldForward=transform.forward;
		while(elapsedTime<=1.5f)
		{
			transform.forward=Vector3.Slerp(oldForward,newForward,elapsedTime);
			elapsedTime+=Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		//	transform.forward=newForward;
		
		//	yield return new WaitForSeconds(1f);
		agent.updateRotation=true;
		status=Status.patrol;
		
		lookingAround=false;
	}

	void Update()
	{

		if(status==Status.patrol)
		{
			agent.SetDestination(nodes[nextNode].gameObject.transform.position);
			if(nodes.Count>1){
			
			if(Vector3.Distance(transform.position,nodes[nextNode].transform.position)<1f){

				MoveOn();
			}
			}
			else if(Vector3.Distance(transform.position,nodes[nextNode].transform.position)<1f)
				if(!lookingAround)
				{

				StartCoroutine("LookAroundIdle");
				}
		}
		else if (status==Status.combat){
			targetRotation = Quaternion.LookRotation (boss.LastKnownPosition-new Vector3(transform.position.x,0,transform.position.z));
			transform.eulerAngles=Vector3.up*Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotationSpeed*Time.deltaTime);
		}
		else if (status==Status.investigating){

			if(Vector3.Distance (transform.position,boss.LastKnownPosition)<1f&&!visual) // temporarily means return to psdfdfatrol, but he should actually look around first.
			{
				//status=Status.patrol;//time to look around like "wtf"
				agent.updateRotation=false;
				if(!lookingAround)
					StartCoroutine("LookAround");
			}
			else{
				agent.updateRotation=true;
				agent.SetDestination(boss.LastKnownPosition); }
		}

		if (status!=Status.combat && gun.magazine <= gun.magazine / 2&&!gun.reloading)

						gun.StartReload ();

		gun.toTarget = gun.bulletSource.forward;
	}


}
