using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionVisibility : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (gameObject.name + "Enter");
		gameObject.GetComponent<CanvasRenderer> ().SetAlpha (1f);
	}

	void OnTriggerExit2D(Collider2D other){
		Debug.Log (gameObject.name + "Exit");
		gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0f);
	}
}
