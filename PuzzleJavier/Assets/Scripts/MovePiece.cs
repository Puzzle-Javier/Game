using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;


using UnityEngine;

public class MovePiece : MonoBehaviour 
{

    public string pieceStatus = "idle";
    private bool moving;
    private bool selected;
    private MovePiece script1;
    private string statusChild;
    public bool joinStatus;
    public bool justSpawn;
    private GameObject image;
    private GameObject camera;
    private PanZoom cameraScript; 
    public const string LAYER_NAME = "Pieces";
    public int sortingOrder = 1;
    private SpriteRenderer sprite;
    private RectTransform imageRectTransform;
    private RawImage m_RawImage;

    public float clickDelta = 0.35f;  // Max between two click to be considered a double click

    private bool click = false;
    private float clickTime;


    // Start is called before the first frame update
    void Start()
    {
        image = GameObject.Find("RawImage");
        sprite = GetComponent<SpriteRenderer>();
        camera = GameObject.Find("Main Camera");
        cameraScript = camera.GetComponent<PanZoom>();
        moving = false;
        selected = false;
        imageRectTransform = image.GetComponent<RectTransform>();
        m_RawImage = image.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {

        if (pieceStatus == "pickedup" && moving)
        {
            moving = true;
            selected = true;
            justSpawn = false;
            sprite.sortingOrder = 4;
        }
        else if (pieceStatus == "childPicked") {
            sprite.sortingOrder = 4;
            cameraScript.pieceSelected = true;
        }
        else if(!justSpawn)
        {

            selected = false;
            if (!joinStatus)
            {
                sprite.sortingOrder = 3;
            }
            else
            {
                sprite.sortingOrder = 2;
            }
        }

        if (click && Time.time > (clickTime + clickDelta))
        {
            click = false;
        }


        if (Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Began && selected)
        {
            Rotate();
        }


        if (selected)
        {
            m_RawImage.texture = sprite.sprite.texture;
            m_RawImage.color = new Color32(255, 255, 225, 255);
            imageRectTransform.rotation = this.transform.rotation;
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
        cameraScript.pieceSelected = true;
        checkParent(this.gameObject.transform,this.gameObject.transform.parent);
        pieceStatus = "pickedup";
        statusChild = "childPicked";
        moving = true;
        traverseChildren(this.gameObject.transform,statusChild);

    }

    void OnMouseUp()
    {
        cameraScript.pieceSelected = false;
        moving = false;
        pieceStatus = "idle";
        selected = false;
        traverseChildren(this.gameObject.transform, pieceStatus);
        m_RawImage.color = new Color32(255, 255, 225, 0);

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
