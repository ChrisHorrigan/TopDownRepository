using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour {
	public Gun currentPrimary;
	public Gun currentSecondary;
	public Gun selectedPrimary;
	public Gun selectedSecondary;
	public Text pWeaponName;
	public Text sWeaponName;
	public bool pSelected;
	public bool sSelected;
	public AttachmentManagement primaries;
	public AttachmentManagement secondaries;
	// Use this for initialization
	void Awake() 
	{
		pSelected = false;
//		pLaser.enabled = false;
		//pSight.enabled = false;
		//pGrip.enabled = false;
		//pMag.enabled = false;
		//sLaser.enabled = false;
		//sMag.enabled = false;
		pSelected = false;
		sSelected = false;
		DontDestroyOnLoad (this.gameObject);

	}
	public void setCurrentPrimary(string weaponName)
	{
		currentPrimary = GameObject.Find (weaponName).GetComponent<Gun> ();
		pWeaponName.text = weaponName;
		if (!currentPrimary.Equals (selectedPrimary) && selectedPrimary != null)
						primaries.HideToggles ();
				else
						primaries.ShowToggles ();
	}
	public void setCurrentSecondary (string weaponName)
	{
		currentSecondary=GameObject.Find (weaponName).GetComponent<Gun> ();
		sWeaponName.text = weaponName;
	}
	public void setSelectedPrimary(string weaponName)
	{
		pSelected = true;
	

		selectedPrimary=GameObject.Find (weaponName).GetComponent<Gun> ();
		primaries.ShowToggles ();
	}
	public void setSelectedSecondary(string weaponName)
	{//
		sSelected = true;

		selectedSecondary=GameObject.Find (weaponName).GetComponent<Gun> ();
	}
	public void DisplayDefault()
	{
		if (selectedPrimary != null) {
						pWeaponName.text = selectedPrimary.name;
			primaries.ShowToggles();
				}
		if(selectedSecondary!=null)
		sWeaponName.text = selectedSecondary.name;
	}
	public void continuous()
	{
		print ("happeningggg!");
		}
	//void Start () {
	//aadad
	//}
	public void LaunchLevel()
	{
		Application.LoadLevel ("Sandbox");
	}//sdafafdasdfdfgdfgsdfg
	//

}
