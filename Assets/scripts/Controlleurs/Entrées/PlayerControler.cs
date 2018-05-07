using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour {

	public GlobalVariables gv;


	void Start () {

	}
	
	/* Si le joueur appuie sur la touche escape 
	 * Si le jeu est fini ( victoire / défaite ), retour au menu principal 
	 * Sinon, ouverture du menu pause. Si le menu pause est déjà ouvert, retour au jeu */

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (gv.panelDefaite.gameObject.activeSelf == true ||
			    gv.panelVictoire.gameObject.activeSelf == true) {
				SceneManager.LoadScene (0);
			} else {
				if (Time.timeScale != 0) {
					pause ();
				} else {
					reprendre ();
				}
			}
		}
	}


	public void pause(){
		gv.pausePanel.gameObject.SetActive (true);
		Time.timeScale = 0;
	}

	public void reprendre(){
		
		Time.timeScale = 1;
		gv.pausePanel.gameObject.SetActive (false);

	}

	public void quitter(){
		SceneManager.LoadScene (0);
	}
}
