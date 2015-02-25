using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AttachmentManagement : MonoBehaviour {
	public Toggle silencerToggle;
	public Toggle laserToggle;

	public void MakeAvailable()
	{
		silencerToggle.interactable = true;
		laserToggle.interactable = true;
	}
	public void ClearToggles()
	{
		//if (silencerToggle.isOn)
						silencerToggle.isOn = false;

		//if (laserToggle.isOn)
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
