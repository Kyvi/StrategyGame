using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AfficherTuto : MonoBehaviour {

	public Image tuto;

	public void afficher(){
		tuto.gameObject.SetActive (true);
	}

	public void desAfficher(){
		tuto.gameObject.SetActive (false);
	}




}
