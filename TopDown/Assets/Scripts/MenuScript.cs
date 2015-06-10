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
	public Stats primary;
	public Stats secondary;
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
		//DontDestroyOnLoad (this.gameObject);

	}

	public void setCurrentPrimary(string weaponName)//CURRENT is when the cursor hovers, SELECTED is the one that is clicked
	{
		currentPrimary = GameObject.Find (weaponName).GetComponent<Gun> ();
		pWeaponName.text = weaponName;
		primary.UpdateStats(currentPrimary);
		if (!currentPrimary.Equals (selectedPrimary) && selectedPrimary != null)
						primaries.HideToggles ();
				else
						primaries.ShowToggles ();
	}
	public void setCurrentSecondary (string weaponName)
	{
		currentSecondary=GameObject.Find (weaponName).GetComponent<Gun> ();
		sWeaponName.text = weaponName;
		secondary.UpdateStats(currentSecondary);
		if (!currentSecondary.Equals (selectedSecondary) && selectedSecondary != null)
			secondaries.HideToggles ();
		else
			secondaries.ShowToggles ();
	}
	public void setSelectedPrimary(string weaponName)
	{
		pSelected = true;
	

		selectedPrimary=GameObject.Find (weaponName).GetComponent<Gun> ();
		primaries.ShowToggles ();
		primaries.setGun(selectedPrimary);
	}
	public void setSelectedSecondary(string weaponName)
	{//
		sSelected = true;

		selectedSecondary=GameObject.Find (weaponName).GetComponent<Gun> ();
		secondaries.ShowToggles();
		secondaries.setGun(selectedSecondary);
	}
	public void DisplayDefault()
	{
		if (selectedPrimary != null) {
						pWeaponName.text = selectedPrimary.name;
			primary.UpdateStats(selectedPrimary);
			primaries.ShowToggles();
				}
		if(selectedSecondary!=null){
			sWeaponName.text = selectedSecondary.name;
			secondary.UpdateStats(selectedSecondary);
			secondaries.ShowToggles();
		}
	}
	public void continuous()
	{
		print ("happeningggg!");
		}
	//void Start () {
	//aadad
	//}
	public void LaunchLevel(int toLaunch)
	{
		Application.LoadLevel (toLaunch);
	}//sdafafdasdfdfgdfgsdfg
	//

}
