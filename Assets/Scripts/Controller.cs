using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller: MonoBehaviour
{
    public PlayerStates state;
    Ray ray;
    RaycastHit hit;
    public Transform[] draggedElements;
    public float elementForce;
    Vector3 prevPos3;
    Vector3 prevPos2;
    Vector3 prevPos1;
    Vector3 touchPos;
    public bool isPaused;
    public Button resumeButton;

    void Update()
    {
        //If the screen is touched
        if (Input.touchCount > 0 && Input.touchCount < 3)
        {
            //Do this for every touch input - 10 max ?
            for (int i = 0; i < Input.touchCount; i++)
            {
                //If the touch began
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    //Raycast for a spell icon under the touch, and send a call
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    Debug.DrawRay(ray.origin, ray.direction * 15f, Color.red, .5f);

                    if (Physics.Raycast(ray, out hit, 15f))
                    {
                        if (hit.collider.tag == "Slot")
                        {
                            hit.transform.GetComponent<SlotManager>().UseSpell();
                        }
                    }
                }

                //If the touch is moving
                if (Input.GetTouch(i).phase == TouchPhase.Moved)
                {
                    //If no element is selected
                    if (draggedElements[i] == null)
                    {
                        //Raycast for an element under the touch, and select it
                        ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                        Debug.DrawRay(ray.origin, ray.direction * 15f, Color.red, .5f);

                        if (Physics.Raycast(ray, out hit, 15f))
                        {
                            if (hit.collider.tag == "Fire" || 
                                hit.collider.tag == "Water" || 
                                hit.collider.tag == "Air" || 
                                hit.collider.tag == "Earth")
                            {
                                draggedElements[i] = hit.transform;
                            }
                        }
                    }

                    //If an element is selected
                    else
                    {
                        //Convert the touch's current screen pos to world pos
                        touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                        //Previous position

                        prevPos3 = prevPos2;
                        prevPos2 = prevPos1;
                        prevPos1 = touchPos;

                        //The element follows the touch
                        draggedElements[i].position = new Vector3(touchPos.x, touchPos.y, draggedElements[i].position.z);
                    }
                }

                //If the touch is lifted and an element was selected
                if (Input.GetTouch(i).phase == TouchPhase.Ended && draggedElements[i] != null)
                {
                    touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                    //print("touchPos : " + touchPos);
                    //print("prevPos : " + prevPos3);

                    Vector3 direction = touchPos - prevPos3;
                    direction.z = 0f;

                    print(direction.magnitude);

                    //Give force
                    draggedElements[i].GetComponent<Rigidbody>().AddForce(direction * elementForce);
                    
                    //Unselect the element
                    draggedElements[i] = null;
                }
            }
        }

        if (Input.touchCount > 2 && !isPaused)
        {
            Pause();
        }
    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        resumeButton.gameObject.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        resumeButton.gameObject.SetActive(false);
    }
}
