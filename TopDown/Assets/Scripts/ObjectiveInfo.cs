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
			texto.text="Level 4: Compound [Clear]\nLooks like some kind of base of operations.  In our territory.  Wipe them out.";
			break;
		case 5:
			texto.text="Level 5: Ambush [VIP]\nWe have the intersection blocked!  Now's your chance to hit the target.";
			break;
		case 6:
			texto.text="Level 6: Not on the List [Clear]\nPoop on this party.";
			break;
		case 7:
			texto.text="Level 7: Public Appearance [VIP]\nThis is a public place with tight security, but this guy knows way too much.  Get him.";
			break;
		case 8:
			texto.text="Level 8: Assault [Clear]\nThat last hit drew some heat.  SWAT is raiding our HQ!  Get them out of here.";
			break;
		case 9:
			texto.text="Level 9: Station [File]\nWe need to know who gave our location to the police.  There should be a file at the station that will tell us.";
			break;
		case 10:
			texto.text="Level 10: House Call [VIP]\nThe target is in witness protection and heavily guarded.  He's the only link to us that the authorities have and he has to go.  You know how it is -- no loose ends.";
			break;
		case 11:
			texto.text="Level 11: Absolution [Clear]\n<i>They made a good point about eliminating loose ends.  This organization is a serious liability to me.  I think it's time to disappear and kill all the witnesses.</i>";
			break;
		}//
	}//fff
	public void ClearText()
	{
		texto.text="";
	}

	// Use this for initialization

}
