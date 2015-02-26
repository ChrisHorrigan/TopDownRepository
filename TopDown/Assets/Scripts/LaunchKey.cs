using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LaunchKey : MonoBehaviour {
	public bool ready;
	public bool primary;
	public bool secondary;

	// Use this for initialization
	void Start () {
		primary = false;
		secondary = false;
		ready = false;//
	}
	public void primarySelected()
	{
		primary = true;
		}
	public void secondarySelected()
	{
		secondary = true;//
		}

	// Update is called once per frameaasdfaafaf
	void Update () {
		if (!(primary&&secondary))
						gameObject.GetComponent<Button> ().interactable = false;
		else
			gameObject.GetComponent<Button> ().interactable = true;

	}//
}
