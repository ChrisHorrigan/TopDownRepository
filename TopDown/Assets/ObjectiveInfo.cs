using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ObjectiveInfo : MonoBehaviour {
	private Text texto;
	void Start()
	{
		texto=GetComponent<Text>();
	}
	public void UpdateText(int level)
	{
		switch(level)
		{
		case 1:
			texto.text="Level 1: Lobby [VIP]\nOur target just showed up in the lobby.  Take him out before his escort team picks him up.  Should be an easy hit.";
			break;
		case 2:
			texto.text="Level 2: Clubhouse [Clear]\nJust three of them in there right now.  Good time to send a message.";
			break;
		case 3:
			texto.text="Level 3: Corridor [VIP]\nThe target has been spotted at the end of the hallway.  Go get him.";
			break;
		case 4:
			texto.text="Level 4: Compound [Clear]\nLooks like some kind of base of operations.  Wipe them out.";
			break;
		case 5:
			texto.text="Level 5: Ambush [VIP]\nWe have the intersection blocked!  Now's your chance to hit the target.";
			break;
		case 6:
			texto.text="Level 6: Not on the List [Clear]\nPoop on this party.";
			break;
		case 7:
			texto.text="Level 7: Public Appearance [VIP]\nSecurity is tight, but this guy rarely makes public appearances.  Get him.";
			break;
		case 8:
			texto.text="Level 8: House Call [VIP]\nThe house is swarming with guards, indicating that the target is in there somewhere.  You're our best shot -- get it done.";
			break;
		}
	}//
	public void ClearText()
	{
		texto.text="";
	}

	// Use this for initialization

}
