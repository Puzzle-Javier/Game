using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{

    public string pieceStatus = "idle";
    public bool moving = false;
    private MovePiece script1;
    private string status;
    private bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pieceStatus == "pickedup")
        {
            moving = true;
            selected = true;
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }
        else
        {
            selected = false;
        }

    }

    //Detect 2D collision 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == gameObject.name)
        {
            transform.position = other.gameObject.transform.position;
            pieceStatus = "locked";
            moving = false;
        }
    }

    //Action taken when a mouse button is pressed
    void OnMouseDown()
    {
        if (pieceStatus == "idle")
        {
            checkParent(this.gameObject.transform,this.gameObject.transform.parent);
            pieceStatus = "pickedup";
            status = "childPicked";
            traverseChildren(this.gameObject.transform,status);

        }
        else
        {
            pieceStatus = "idle";
            traverseChildren(this.gameObject.transform, pieceStatus);
        }
    }


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && selected)
        {
            Rotate();
        }
    }


    void Rotate()
    {
        int degrees = 45;
        Vector3 to = new Vector3(0, 0, degrees);
        //transform.eulerAngles = Vector3.Lerp(this.transform.rotation.eulerAngles, to);
        transform.Rotate(0, 0, degrees);
    }

    //Method to chek parent and change transform
    void checkParent(Transform root, Transform parent)
    {
        if(parent.name == "Pieces")
        {
            return;
        }

        root.transform.SetParent(parent.transform.parent.transform);
        parent.transform.SetParent(root.transform);
        checkParent(root,parent.transform.parent.transform);
    }

    void traverseChildren(Transform root, string status)
    {
        foreach (Transform child in root)
        {
            // Do something with child, then recurse.

            script1 = child.gameObject.GetComponent<MovePiece>();
            if(script1 != null)
            {
                script1.pieceStatus = status;
                traverseChildren(child, status);
            }

        }
    }

}
