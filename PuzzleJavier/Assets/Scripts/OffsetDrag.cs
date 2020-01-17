using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetDrag : MonoBehaviour
{

    private float deltaX, deltaY;
    private Rigidbody2D rb;
    public float offset = 0.5f;
    private bool selected;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.touchCount > 0  && selected == true)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);


            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    break;
                case TouchPhase.Moved:
                    rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    break;
                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    break;
            }

        }


    }


    void OnMouseDown()
    {
        selected = true;
        transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);

    }




}
