using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Stats : MonoBehaviour {
	public Text damage;
	public Text rpm;
	public Text mag;
	public Text stability;
	public Text min;
	public Text max;
	// Use this for initialization
	void Start () {
	
	}
	public void UpdateStats(Gun gun)
	{
		damage.text=gun.damage.ToString();
		rpm.text=gun.rpm.ToString();
		mag.text=gun.magSize.ToString();
		stability.text=(gun.recover-gun.jump).ToString();
		min.text=gun.mainMinSpread.ToString();
		max.text=gun.mainMaxSpread.ToString();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
