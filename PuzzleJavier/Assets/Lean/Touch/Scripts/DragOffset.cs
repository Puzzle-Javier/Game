using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOffset : MonoBehaviour
{

    private float deltaX, delta;
    private Rigidbody2D rb;
    public float offset;
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
        
    }


    void OnMouseDown()
    {
        selected = true;
        transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.y);
    }


}
