using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AffichageMenuGestion : MonoBehaviour {

	public GlobalVariables gv;
	public Image panel;

	public void changePanel(){
		
		gv.activepanel.gameObject.SetActive (false);
		gv.activepanel = panel;
		gv.activepanel.gameObject.SetActive (true);
	}
}
