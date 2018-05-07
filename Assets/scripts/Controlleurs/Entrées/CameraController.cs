using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	void Start () {

	}

	/* La caméra se délpalce selon la position de la souris */

	void Update () {
		float mousePosX = Input.mousePosition.x; 
		float mousePosY = Input.mousePosition.y; 

		int scrollDistance = 5; 
		float scrollSpeed = 100;

		if (mousePosX < scrollDistance && transform.position.x > -395) 
		{ 
			transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime); 
		} 

		if (mousePosX >= Screen.width - scrollDistance && transform.position.x < 395) 
		{ 
			transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime); 
		}

		if (mousePosY < scrollDistance && transform.position.z > -440) 
		{ 
			transform.Translate(Vector3.up * -scrollSpeed * Time.deltaTime); 
		} 

		if (mousePosY >= Screen.height - scrollDistance && transform.position.z < 440) 
		{ 
			transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime); 
		}
	}
}
