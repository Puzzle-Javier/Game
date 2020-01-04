using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{

    public string pieceStatus = "idle";
    private bool moving;
    private bool selected;
    private MovePiece script1;
    private string statusChild;
    public bool joinStatus;
    public const string LAYER_NAME = "Pieces";
    public int sortingOrder = 1;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        moving = false;
        selected = false;

        if (sprite)
        {
            sprite.sortingOrder = sortingOrder;
            sprite.sortingLayerName = LAYER_NAME;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pieceStatus == "pickedup" && moving)
        {
            moving = true;
            selected = true;
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
            sprite.sortingOrder = 2;
        }
        else if (pieceStatus == "childPicked") {
            sprite.sortingOrder = 2;
        }
        else
        {

            selected = false;
            if (!joinStatus)
            {
                sprite.sortingOrder = sortingOrder;
            }
            else
            {
                sprite.sortingOrder = 0;
            }
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

        checkParent(this.gameObject.transform,this.gameObject.transform.parent);
        pieceStatus = "pickedup";
        statusChild = "childPicked";
        moving = true;
        traverseChildren(this.gameObject.transform,statusChild);

    }


    void OnMouseUp()
    {
        moving = false;
        pieceStatus = "idle";
        traverseChildren(this.gameObject.transform, pieceStatus);
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
