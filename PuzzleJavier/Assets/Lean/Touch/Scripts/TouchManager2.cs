using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TouchManager2 : MonoBehaviour
{
 
    public GameObject[] touchedObj = new GameObject[2];
    private Vector3 offset;
 
    void Update()
    {
        Touch[] myTouches = Input.touches;
               // GET WHICH FINGER TOUCHES THE SCREEN //
 
        for (int i = 0; i < Input.touchCount; i++)
        {
         
            Vector3 mainPos = Camera.main.ScreenToWorldPoint(myTouches[i].position);
            Ray ray = Camera.main.ScreenPointToRay(myTouches[i].position);
            RaycastHit hit;
            mainPos.z = -1f;
         
 
            if (Physics.Raycast(ray, out hit, 20f))
            {
             
                // BEGAN PHASE //
                if (hit.collider.gameObject.name == "player" && hit.collider != null && myTouches[i].phase == TouchPhase.Began){
                    print("New touch detected at position " + myTouches[i].position  );    
                    touchedObj[i] = hit.transform.gameObject ;                                          
                }
            }
 
               // DRAGGED PHASE //
            int ID = myTouches[i].fingerId;
            if (touchedObj[ID] != null)
            {
                touchedObj[ID].transform.position = mainPos;
                print("Obj Dragged!!");
            }
 
               // ENDED PHASE //
            if (myTouches[i].phase == TouchPhase.Ended && touchedObj[ID] != null)
            {
               // touchedObj[ID].GetComponent<Rigidbody>().AddForce(Vector3.right * 50f);
                touchedObj[ID] = null;
                print("Obj Released!!");
            }
        }
    }
 
}