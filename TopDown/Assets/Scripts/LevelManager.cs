using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour {
	public bool[] levelsCompleted;
	public bool[] levelsAvailable;
	public Button[] launchButtons;
	public bool primary;
	public bool secondary;
	// Use this for initialization
	void Start () {
		primary=false;
		secondary=false;
		levelsCompleted=GameControl.control.Load ();
		GameControl.control.Save(levelsCompleted);
		levelsAvailable=CreateAvailable(levelsCompleted);

	}
	void Update()
	{
		ButtonStati ();
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
		for(int i=0;i<launchButtons.Length;i++)
		{
			if(levelsCompleted[i])
				launchButtons[i].GetComponentInChildren<Text>().color=Color.green;
			if(levelsAvailable[i]&&secondary&&primary)
				launchButtons[i].interactable=true;
			else
				launchButtons[i].interactable=false;
		}
	}
	bool[] CreateAvailable(bool[]completed)
	{
		bool[]temp=new bool[completed.Length];
		for(int b = 0;b<temp.Length;b++)
		{
			if(completed[b])
				temp[b]=true;
			else if(!temp[b])
			{
				temp[b]=true;
				break;
			}
		}
		return temp;//asdfadsfffff
	}

}
