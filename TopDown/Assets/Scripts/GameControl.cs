using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameControl : MonoBehaviour {

	public static GameControl control;
	// Use this for initialization
	void Awake()
	{
		if(control==null)
		{
			control=this;
			DontDestroyOnLoad(gameObject);
		}
		else if (control!=this)
		{
			Destroy(gameObject);
		}
	}/////dfd
	public void CompleteLevel(int level){

		//This is a load combined with a save
		if(File.Exists(Application.persistentDataPath+ "/gameProgress.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file=File.Open (Application.persistentDataPath+"/gameProgress.dat",FileMode.Open);
			GameProgress progress=(GameProgress)bf.Deserialize(file);
			bool[] levels=progress.levelProgress;
			levels[level]=true;
			progress.levelProgress=levels;
			file.Close ();
		}
		else
			print("for some reason there is no file to update");

	}
	public void Save(bool[] completed)//only really useful for first time file creation, people will not always update the file by winning a level
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath+"/gameProgress.dat");
		GameProgress progress=new GameProgress();
		progress.levelProgress=completed;
		bf.Serialize(file,progress);
		file.Close ();

	}

	public bool[] Load()
	{
		if(!File.Exists(Application.persistentDataPath+ "/gameProgress.dat"))
		{

			BinaryFormatter bf=new BinaryFormatter();
			FileStream file = File.Create (Application.persistentDataPath + "/gameProgress.dat");
			GameProgress progress = new GameProgress();
			progress.levelProgress=new bool[5];

			bf.Serialize(file,progress);
			file.Close ();
			return new bool[5];
		}
		else 
		{
			print (Application.persistentDataPath);//asdfasd
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file=File.Open (Application.persistentDataPath+"/gameProgress.dat",FileMode.Open);
			GameProgress progress=(GameProgress)bf.Deserialize(file);
			bool[] levels=progress.levelProgress;

			file.Close ();
			return levels;//asdfasd
		}
	}

}
[Serializable]
class GameProgress
{

	public bool[] levelProgress;
}
