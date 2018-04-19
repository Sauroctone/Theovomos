using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

	public RectTransform scrollpanel;
	public RectTransform[] characterPanels;
	public RectTransform[] character1Skins;
	public RectTransform[] character2Skins;
	public RectTransform[] character3Skins;
	public RectTransform[] character4Skins;
	public RectTransform center;

	private float[] Xdistances;
	private float[] char1Ydistances;
	private float[] char2Ydistances;
	private float[] char3Ydistances;
	private float[] char4Ydistances;
	private bool isDragging;
	private int panelDistance;
	private int minXPanelNumber;
	private int minYPanelNumber;
	// Use this for initialization
	void Start () {
		int panellength = characterPanels.Length;
		Xdistances = new float[panellength];

		int skins1Length = character1Skins.Length;
		char1Ydistances = new float[skins1Length];

		int skins2Length = character2Skins.Length;
		char2Ydistances = new float[skins1Length];

		int skins3Length = character3Skins.Length;
		char3Ydistances = new float[skins1Length];

		int skins4Length = character4Skins.Length;
		char4Ydistances = new float[skins1Length];

		panelDistance = (int)Mathf.Abs (characterPanels [1].GetComponent<RectTransform> ().anchoredPosition.x - characterPanels [0].GetComponent<RectTransform> ().anchoredPosition.x);
	}
	
	// Update is called once per frame
	void Update () {
		//get closer panel in Absysse
		for(int i = 0; i<characterPanels.Length; i++){
			Xdistances [i] = Mathf.Abs (center.transform.position.x - characterPanels [i].transform.position.x);
		}
		float minXdistance = Mathf.Min (Xdistances);

		for (int a = 0; a < characterPanels.Length; a++) {
			if (minXdistance == Xdistances [a]) {
				minXPanelNumber = a;
			}
		}

		//get Closer panel in ordonnées (en fonction de panel en absysse)
		if (minXPanelNumber == 0) {
			for (int u = 0; u < character1Skins.Length; u++) {
				char1Ydistances [u] = Mathf.Abs (center.transform.position.y - character1Skins [u].transform.position.y);
			}
			float minYdistance = Mathf.Min (char1Ydistances);

			for (int b = 0; b < character1Skins.Length; b++) {
				if (minYdistance == char1Ydistances [b]) {
					minYPanelNumber = b;
				}
			}
		}

		if (minXPanelNumber == 1) {
			for(int u = 0; u<character2Skins.Length; u++){
				char2Ydistances [u] = Mathf.Abs (center.transform.position.y - character2Skins[u].transform.position.y);
			}
			float minYdistance = Mathf.Min (char2Ydistances);

			for (int b = 0; b < character2Skins.Length; b++) {
				if (minYdistance == char2Ydistances [b]) {
					minYPanelNumber = b;
				}
			}
		}
		if (minXPanelNumber == 2) {
			for(int u = 0; u<character3Skins.Length; u++){
				char3Ydistances [u] = Mathf.Abs (center.transform.position.y - character3Skins[u].transform.position.y);
			}
			float minYdistance = Mathf.Min (char3Ydistances);

			for (int b = 0; b < character3Skins.Length; b++) {
				if (minYdistance == char3Ydistances [b]) {
					minYPanelNumber = b;
				}
			}
		}
		if (minXPanelNumber == 3) {
			for(int u = 0; u<character4Skins.Length; u++){
				char4Ydistances [u] = Mathf.Abs (center.transform.position.y - character4Skins[u].transform.position.y);
			}
			float minYdistance = Mathf.Min (char4Ydistances);

			for (int b = 0; b < character4Skins.Length; b++) {
				if (minYdistance == char4Ydistances [b]) {
					minYPanelNumber = b;
				}
			}
		}

		if (!isDragging) {
			LerpToPanel(minXPanelNumber * -panelDistance, minYPanelNumber * -panelDistance);
		}
	}
	void LerpToPanel(int positionx, int positiony){
		float newX = Mathf.Lerp(scrollpanel.anchoredPosition.x, positionx, Time.deltaTime *10f);
		float newY = Mathf.Lerp(scrollpanel.anchoredPosition.y, -positiony, Time.deltaTime *10f);

		Vector2 newPosition = new Vector2 (newX, newY);
		scrollpanel.anchoredPosition = newPosition;
	}

	public void StartDrag(){
		isDragging = true;
	}

	public void EndDrag(){
		isDragging = false;
	}
}
