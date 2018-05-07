using UnityEngine;
using System.Collections;

public class Onclick : MonoBehaviour {

	public GlobalVariables gv;

	public void clickButton(){
		Creator creator;
		creator = gv.activeBase.GetComponent<Creator>();
		creator.construction ();

	}
}
