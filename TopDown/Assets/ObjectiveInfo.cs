using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ObjectiveInfo : MonoBehaviour {
	private Text texto;
	void Start()
	{
		texto=GetComponent<Text>();
	}
	public void UpdateText(string text)
	{
		if(text.Equals("VIP"))
			texto.text="VIP: Take out the VIP and get out alive.";
		else if(text.Equals ("Clear"))
			texto.text="Clear: Kill everyone.";//
	}
	public void ClearText()
	{
		texto.text="";
	}
	// Use this for initialization

}
