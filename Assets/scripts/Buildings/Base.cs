using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Base : MonoBehaviour {

	/* Variables */
	public Image panel;
	public GlobalVariables gv;
	public int health;
	public int currentHealth;
	public int side;

	public float timerMax;
	public float timer;

	public float timerValideMax;
	public float timerValide;
	public bool isValid;

	public GameObject barreChargement;
	public Text texteChargement;
	public Text texteVie;

	public float nextActionTime = 0f;
	public float period;

	public ArrayList peuple = new ArrayList();

	public int type;




	public virtual void Start () {
		switch (side) {
		case 0:
			break;
		case 1:
			Renderer rend1 = gameObject.GetComponent<Renderer> ();
			rend1.material.SetColor ("_Color", Color.red);
			break;
		}
	
	}

	/*-	Si la base est invalide et que le timer d’invalidité est à 0 : 
		  	on remet la vie à fond, on remet le timer à fond, on remet la validité du bâtiment.
			-	Si la base est valide : on affiche la vie,
			si c’est une base alliée, on actualise la file d’attente,
			on affiche le texte de la file d’attente et la barre de chargement,
			puis on teste si la base est détruite ou non. */
	

	public virtual void Update () {

		if (!isValid && gv.time >= nextActionTime) {
			nextActionTime += period;
			if (timerValide <= 0f) {
				Renderer rend;
				if (side == 0){
				rend = gameObject.GetComponent<Renderer> ();
					rend.material.SetColor ("_Color", Color.blue);
				}else{
					rend = gameObject.GetComponent<Renderer> ();
					rend.material.SetColor ("_Color", Color.red);
				}

				currentHealth = health;
				isValid = true;
				timerValide = timerValideMax;
			} else {
				timerValide = timerValide - period;
			}
		}

		if (isValid) {

			setTextVie ();
			if (side == 0) {
				actualiseFileAttente ();
				setTextChargement ();
				setBarreChargement ((timerMax - timer) / timerMax);
			}
			testDestructionBatiment ();
		}
	}

	/*On désactive le gameObject du menu actif.
		On change la variable contenant le menu actif.
		On active le gameObject du menu actif. */

	void OnMouseDown() {

		gv.activepanel.gameObject.SetActive (false);
			gv.activeBarre.SetActive (false);
			gv.activeText.gameObject.SetActive (false);
		gv.activeTextVie.gameObject.SetActive (false);

		gv.activepanel = panel;
		gv.activeTextVie = texteVie;
		if (side == 0) {
			gv.activeBarre = barreChargement;
			gv.activeText = texteChargement;
		}
		gv.activeBase = this.gameObject;

		gv.activepanel.gameObject.SetActive (true);
		gv.activeTextVie.gameObject.SetActive (true);

		if (side == 0) {
			gv.activeBarre.SetActive (true);
			gv.activeText.gameObject.SetActive (true);
		}
	}

	public void setBarreChargement(float currentState){
		barreChargement.transform.localScale = 
			new Vector3 (currentState, barreChargement.transform.localScale.y, barreChargement.transform.localScale.z);
	}

	public void setTextVie(){
		texteVie.text = currentHealth + " / " + health;
	}

	public void setTextChargement(){
		switch (type) {
		case 0:
			texteChargement.text = "File Minions : " + peuple.Count;
			break;
		case 1:
			texteChargement.text = "File Epeistes : " + peuple.Count;
			break;
		case 2:
			texteChargement.text = "File Archers : " + peuple.Count;
			break;
		case 3:
			texteChargement.text = "File Cavaliers : " + peuple.Count;
			break;
		}
	}

	/*Le test de destruction de bâtiment :
	Si la vie est en dessous de 0. On regarde si le bâtiment est allié ou ennemi.
	S’il est allié, on regarde s’il s’agit de la base principale.
	Si c’est le cas Défaite.
	Sinon, on retire les unités de la file d’attente,
	on récupère les ressources dépensées pour ces unités.
	Si le bâtiment est ennemi, on regarde s’il s’agit de la base principale.
	Si c’est le cas  Victoire.
	Dans tous les cas, la base devient invalide.*/

	public void testDestructionBatiment(){

		if (currentHealth <= 0) {
			Renderer rend1;
			switch (side) {
			case 0:
				if (type == 0) {
					gv.panelDefaite.gameObject.SetActive (true);
					Time.timeScale = 0;
				} else {
					rend1 = gameObject.GetComponent<Renderer> ();
					rend1.material.SetColor ("_Color", Color.cyan);
					for (int i = 0; i < peuple.Count; i++) {
						gameObject.GetComponent<Creator> ().getRessourcesBack ();
						peuple.RemoveAt (i);
					}
				}
				break;
			case 1:
				if (type == 0) {
					gv.panelVictoire.gameObject.SetActive (true);
					Time.timeScale = 0;
				} else {
					rend1 = gameObject.GetComponent<Renderer> ();
					rend1.material.SetColor ("_Color", Color.magenta);
				}
				break;
			}

			isValid = false;
			timerValide = timerValideMax;
		}
	}

	/*L’actualisation de la file d’attente :
	S’il y a des gens dans la file, on regarde le timer de la file.
	S’il est arrivé à 0, on crée l’unité, on le retire de la file,
	on remet le timer à fond. Si le timer n’est pas arrivé à 0, on le décrémente.
	*/

	public void actualiseFileAttente(){
		if (peuple.Count == 0 && gv.time >= nextActionTime) {
			nextActionTime += period;
		}
		if (peuple.Count != 0 && gv.time >= nextActionTime) {
			nextActionTime += period;

			if (timer <= 0f) {
				if (type == 0) {
					gameObject.GetComponent<CreateurMinion> ().CreateMinion ();
				} else {
					gameObject.GetComponent<Creator> ().create (side);
				}
				peuple.RemoveAt (0);
				timer = timerMax;
			} else {
				timer = timer - period;
			}
		}
	}


}
