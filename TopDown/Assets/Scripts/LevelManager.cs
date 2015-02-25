using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public bool[] levelsCompleted;
	public bool[] levelsAvailable;
	// Use this for initialization
	void Start () {
		levelsCompleted=GameControl.control.Load ();
		GameControl.control.Save(levelsCompleted);
		levelsAvailable=CreateAvailable(levelsCompleted);
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
	// Update is called once per frame
	void Update () {
	
	}
}
