using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AttachmentManagement : MonoBehaviour {
	public Toggle silencerToggle;
	public Toggle laserToggle;
	public enum Type {primary,secondary};
	public Type type;
	private Gun currentGun;
	private MenuScript UpRef;
	void Start()
	{
		UpRef=GetComponentInParent<MenuScript>();
	}
	public void setGun(Gun gun)
	{
		currentGun=gun;
	}
	public void MakeAvailable()
	{
		if(currentGun.canSilence)
			silencerToggle.interactable = true;
		else
			silencerToggle.interactable=false;
		laserToggle.interactable = true;
	}
	public void ToggleSilencer()//it shouldn't call this if the value is changing by picking a different weapon
	{

		currentGun.ToggleSilencer();
		if(type==Type.primary)
			UpRef.primary.UpdateStats(currentGun);
		else if(type==Type.secondary)
			UpRef.secondary.UpdateStats(currentGun);
	}
	public void ToggleLaser()
	{

		currentGun.ToggleLaser();
	}
	public void TestFunction(bool lol)
	{
		print ("dynamic bool por favor");
	}
	public void ClearToggles()
	{
	//fffff
			silencerToggle.isOn = false;
		


			laserToggle.isOn = false;
		
		//silencerToggle.graphic.enabled = false;
		//laserToggle.graphic.enabled = false;
		//print ("toggle values cleared");
	}
	public void HideToggles()
	{
		silencerToggle.graphic.enabled = false;
		laserToggle.graphic.enabled = false;
		//print ("toggle values HIDDEN");
	}
	public void ShowToggles()
	{
		//print ("toggle values SHOWN");
		if(silencerToggle.isOn)
		silencerToggle.graphic.enabled = true;
		if(laserToggle.isOn)
		laserToggle.graphic.enabled = true;
	}


}
