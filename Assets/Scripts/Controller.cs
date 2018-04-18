using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller: MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public Transform[] draggedElements;
    public float elementForce;
    Vector3 prevPos;

    void Update()
    {
        //If the screen is touched
        if (Input.touchCount > 0)
        {
            //Do this for every touch input - 10 max ?
            for (int i = 0; i < Input.touchCount; i++)
            {
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
                            draggedElements[i] = hit.transform;
                        }
                    }

                    //If an element is selected
                    else
                    {
                        //Convert the touch's screen pos to world pos
                        Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                        //The element follows the touch
                        draggedElements[i].position = new Vector3(touchPos.x, touchPos.y, draggedElements[i].position.z);

                        prevPos = Input.GetTouch(i).position;
                    }
                }

                //If the touch is lifted and an element was selected
                if (Input.GetTouch(i).phase == TouchPhase.Ended && draggedElements[i] != null)
                {
                    /*
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                    Vector3 direction = touchPos - prevPos;
                    direction.z = 0f;
                    print(touchPos);

                    //Give force
                    draggedElements[i].GetComponent<Rigidbody>().AddForce(direction * elementForce);
                    */

                    //Unselect the element
                    draggedElements[i] = null;
                }
            }
        }
    }
}
