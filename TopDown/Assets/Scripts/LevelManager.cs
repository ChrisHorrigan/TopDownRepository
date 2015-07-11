using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour {
	public bool[] levelsCompleted;
	public bool[] levelsAvailable;
	public Button[] levelButtons;
	public bool primary;
	public bool secondary;
	public GameObject menuPanel;
	public GameObject loadoutPanel;
	public GameObject instructionPanel;
	private GameControl control;
	public Button launchButton;
	public Slider Music;
	public Slider SFX;
	private bool levelSelected;
	// Use this for initialization
	void Start () {
		levelSelected=false;
		//PlayerPrefs.DeleteAll();
		if(!PlayerPrefs.HasKey("music"))
		{
			PlayerPrefs.SetFloat ("music",Music.value);
		}
		else
			Music.value=PlayerPrefs.GetFloat("music");

		if(!PlayerPrefs.HasKey("SFX"))
		{
			PlayerPrefs.SetFloat ("SFX",SFX.value);
		}
		else
			SFX.value=PlayerPrefs.GetFloat("SFX");
		control=GameObject.Find ("GameController").GetComponent<GameControl>();
		if(control.firstRun){
		menuPanel.SetActive(true);
		loadoutPanel.SetActive(false);
		}
		else
		{
			menuPanel.SetActive(false);
			loadoutPanel.SetActive(true);
		}
		primary=false;
		secondary=false;
		//levelsCompleted=GameControl.control.Load ();
		//GameControl.control.Save(levelsCompleted);
		//levelsAvailable=CreateAvailable(levelsCompleted);

	}
	public void ChangeMusicVolume(float vol)
	{
		PlayerPrefs.SetFloat("music",vol);
	}
	public void ChangeSFXVolume(float vol)
	{
		PlayerPrefs.SetFloat("SFX",vol);
	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
			ChangePanel ("main");
		ButtonStati ();

	}
	public void LevelChosen()
	{
		levelSelected=true;
	}
	public void ChangePanel(string which)
	{
		if(which.Equals("loadout"))
		{
			menuPanel.SetActive(false);
			instructionPanel.SetActive (false);
			loadoutPanel.SetActive(true);
		}
		else if(which.Equals("instructions"))
		{
			menuPanel.SetActive(false);
			instructionPanel.SetActive (true);
			loadoutPanel.SetActive(false);
		}
		else if(which.Equals("main"))
		{
			menuPanel.SetActive(true);
			instructionPanel.SetActive (false);
			loadoutPanel.SetActive(false);
		}
	}
	public void primarySelected()
	{
		primary = true;
	}
	public void secondarySelected()
	{
		secondary = true;//
	}
	void ButtonStati()
	{
		if(loadoutPanel.activeSelf){
//		for(int i=0;i<launchButtons.Length;i++)
//		{
//			if(levelsCompleted[i])
//				launchButtons[i].GetComponentInChildren<Text>().color=Color.green;
//			if(levelsAvailable[i]&&secondary&&primary)
//				launchButtons[i].interactable=true;
//			else
//				launchButtons[i].interactable=false;
//			}
		if(primary&&secondary&&levelSelected)
				launchButton.interactable=true;//fffff
			for(int i=0;i<levelButtons.Length;i++)
			{
				if(PlayerPrefs.GetFloat(i.ToString ())==1)//the level is complete
				{	
					levelButtons[i].GetComponentInChildren<Text>().color=Color.green;

						levelButtons[i].interactable=true;
				}
				else if((PlayerPrefs.GetFloat ((i-1).ToString())==1||i==0))
					levelButtons[i].interactable=true;
				else
					levelButtons[i].interactable=false;
			}
		
		}
	}
//	bool[] CreateAvailable(bool[]completed)
//	{
//		bool[]temp=new bool[completed.Length];
//		for(int b = 0;b<temp.Length;b++)
//		{
//			if(completed[b])
//				temp[b]=true;
//			else if(!temp[b])
//			{
//				temp[b]=true;
//				break;
//			}
//		}
//		return temp;//asdfadsfffff
//	}

}
