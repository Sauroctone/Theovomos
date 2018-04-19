using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRectSnap : MonoBehaviour {

	public RectTransform panel;
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
		panelDistance = (int)Mathf.Abs(subPanels[1].GetComponent<RectTransform>().anchoredPosition.y - subPanels[0].GetComponent<RectTransform>().anchoredPosition.y);
	}


	void Update () {
		for (int i = 0; i < subPanels.Length; i++) {
			distances [i] = Mathf.Abs (center.transform.position.y - subPanels [i].transform.position.y);
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
		float newY = Mathf.Lerp(panel.anchoredPosition.y, -position, Time.deltaTime *10f);
		Vector2 newPosition = new Vector2 (panel.anchoredPosition.x, newY);
		panel.anchoredPosition = newPosition;
	}

	public void StartDrag(){
		isDragging = true;
	}

	public void EndDrag(){
		isDragging = false;
	}
}
