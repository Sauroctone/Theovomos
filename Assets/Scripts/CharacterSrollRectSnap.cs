using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSrollRectSnap : MonoBehaviour {
	public RectTransform Playerpanel;
	public RectTransform[] subPanels;
	public RectTransform center;

	private float[] distances; //distance de tous les panneaux avec le centre
	private bool isDragging = false; 
	private int panelDistance; //Distance entre les panneaux
	private int minPanelNumber; //plus petite distance entre les panneaux

	void Start () {
		int panelLength = subPanels.Length;
		distances = new float[panelLength];

		//get distance between panels
		panelDistance = (int)Mathf.Abs(subPanels[1].GetComponent<RectTransform>().anchoredPosition.x - subPanels[0].GetComponent<RectTransform>().anchoredPosition.x);
	}

	void Update () {
		for (int i = 0; i < subPanels.Length; i++) {
			distances [i] = Mathf.Abs (center.transform.position.x - subPanels [i].transform.position.x);
		}

		float minDistance = Mathf.Min(distances); //get the min distance in the array

		for (int a = 0; a < subPanels.Length; a++) {
			if (minDistance == distances [a]) {
				minPanelNumber = a;
			}
		}

		if (!isDragging) {
			LerpToPanel(minPanelNumber * -panelDistance);
		}
	}

	void LerpToPanel(int position){
		float newX = Mathf.Lerp(Playerpanel.anchoredPosition.x, position, Time.deltaTime *10f);
		Vector2 newPosition = new Vector2 (newX,Playerpanel.anchoredPosition.y);
		Playerpanel.anchoredPosition = newPosition;
	}

	public void StartDrag(){
		isDragging = true;
	}

	public void EndDrag(){
		isDragging = false;
	}
}
