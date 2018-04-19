using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionVisibility : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Enter");
		gameObject.GetComponent<CanvasRenderer> ().SetAlpha (1f);
	}

	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Exit");
		gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0f);
	}
}
