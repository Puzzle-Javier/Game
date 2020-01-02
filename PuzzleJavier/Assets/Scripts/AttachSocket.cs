using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

public class AttachSocket : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform child;
    private Transform parent;
    private Transform child_1;
    private Transform parent_1;
    private MovePiece script1;
    private MovePiece script2;
    private Transform parent_angle1;
    private Transform parent_angle2;
    private float angle1;
    private float angle2;

    // public float dif;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string string1 = null;
        string string2 = null;

        script1 = collider.transform.parent.gameObject.GetComponent<MovePiece>();
        script2 = this.transform.parent.gameObject.GetComponent<MovePiece>();

        if (collider is CircleCollider2D && collider.gameObject.name == gameObject.name)
        {

            string1 = collider.transform.parent.name;
            string2 = gameObject.transform.parent.name;
            //float angle1 = collider.transform.parent.transform.localEulerAngles.z;
            //float angle2 = this.transform.parent.transform.localEulerAngles.z;

            parent_angle1 = searchParent(collider.transform.parent);
            parent_angle2 = searchParent(this.transform.parent);

            angle1 = parent_angle1.localEulerAngles.z;
            angle2 = parent_angle2.localEulerAngles.z;

            //print(this.transform.parent.name + " angle " + angle2);
            //print(collider.transform.parent.name + " angle " + angle1);

            float angle = Mathf.Round(angle1);
          
            if (Mathf.Abs(angle1 - angle2) < 5)
            {
                if (script2.pieceStatus == "pickedup")
                {
                    child = collider.transform.parent;
                    parent = this.gameObject.transform.parent;
                    alignPieces(parent, child, string1, string2,angle);
                    transformParent(parent, child);
                    deactivateGameObject(collider.transform, this.transform, script2, script1);
                }
                else if (script1.pieceStatus == "pickedup")
                {
                    child = this.transform.parent;
                    parent = collider.transform.parent;
                    alignPieces(parent, child, string1, string2,angle);
                    transformParent(parent, child);
                    deactivateGameObject(collider.transform, this.transform, script1,script2);
                }
            }
        }
    }

    void deactivateGameObject(Transform object1, Transform object2, MovePiece script1,MovePiece script2)
    {
        object1.gameObject.SetActive(false);
        object2.gameObject.SetActive(false);
        script1.pieceStatus = "idle";
        script1.joinStatus = true;
        script2.joinStatus = true;
    }
    


    void transformParent(Transform parent,Transform child)
    {
        if(child.transform.parent.name == "Pieces")
        {
            child.transform.SetParent(parent.transform);
            //child.transform.localPosition = Vector3.zero;
            //child.transform.localRotation = Quaternion.identity;
            return;
        }
        transformParent(parent, child.transform.parent);
    }


    Transform searchParent(Transform root)
    {
        if(root.transform.parent.name == "Pieces")
        {
            return root.transform;
        }
        searchParent(root.transform.parent);
        return root.transform.parent;
    }

    void alignPieces(Transform parent, Transform child, string name1, string name2, float angle)
    {
        char[] string1 = name1.ToCharArray();
        char[] string2 = name2.ToCharArray();
        float dif = 3.42f;
        float row_x, row_y,column_x,column_y;
        float angleRad = angle * Mathf.Deg2Rad;

        row_x = dif * Mathf.Cos(angleRad);
        row_y = dif * Mathf.Sin(angleRad);
        column_y = dif * Mathf.Cos(angleRad);
        column_x = dif * Mathf.Sin(angleRad);

        if(name1[0] == name2[0])
        {
            if(angle < 90)
            {
                movePieceXA(parent,child,row_x,row_y);
            }
            else if (angle > 90 && angle < 270)
            {
                movePieceXB(parent,child,row_x,row_y);
            }
            else if(angle > 270 && angle < 360)
            {
                movePieceXA(parent,child,row_x,row_y);
            }
            else if(angle == 90)
            {
                movePieceXC(parent,child,row_x,row_y);
            }
            else if(angle == 270)
            {
                movePieceXD(parent,child,row_x,row_y);
            }
        }
        else 
        {
            if(angle > 0 && angle < 180 && angle != 90)
            {
                movePieceYA(parent, child, row_x, row_y);
            }
            else if(angle > 180 && angle < 360 && angle != 270)
            {
                movePieceYB(parent,child,row_x,row_y);
            }
            else if(angle == 270)
            {
                movePieceYC(parent,child,column_x);
            }
            else if(angle == 90)
            {
                movePieceYD(parent, child, column_x);
            }
            else if (angle == 180)
            {
                movePieceYE(parent,child,column_y);
            }
            else
            {
                movePieceYF(parent,child,column_y);
            }
        }
    }


    void movePieceXA(Transform parent, Transform child,float x,float y){

        if (parent.transform.position.x < child.transform.position.x){
            parent.transform.position = new Vector3(child.transform.position.x - x, child.transform.position.y - y, child.transform.position.z);
        }
        else{
            parent.transform.position = new Vector3(child.transform.position.x + x, child.transform.position.y + y, child.transform.position.z);
        }
    }

    void movePieceXB(Transform parent, Transform child, float x, float y){

        if (parent.transform.position.x < child.transform.position.x){
            parent.transform.position = new Vector3(child.transform.position.x + x, child.transform.position.y + y, child.transform.position.z);
        }
        else{
            parent.transform.position = new Vector3(child.transform.position.x - x, child.transform.position.y - y, child.transform.position.z);
        }  
    }

    void movePieceXC(Transform parent, Transform child, float x, float y){

        if (parent.transform.position.y < child.transform.position.y){
            parent.transform.position = new Vector3(child.transform.position.x, child.transform.position.y - y, child.transform.position.z);
        }
        else{
            parent.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + y, child.transform.position.z);
        }
    }

    void movePieceXD(Transform parent, Transform child, float x, float y){

        if (parent.transform.position.y < child.transform.position.y){
            parent.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + y, child.transform.position.z);
        }
        else{
            parent.transform.position = new Vector3(child.transform.position.x, child.transform.position.y - y, child.transform.position.z);
        }
    }


    void movePieceYA(Transform parent, Transform child, float x, float y){
        if (parent.transform.position.y < child.transform.position.y){
            parent.transform.position = new Vector3(child.transform.position.x + x, child.transform.position.y - y, child.transform.position.z);
        }
        else{
            parent.transform.position = new Vector3(child.transform.position.x - x, child.transform.position.y + y, child.transform.position.z);
        }
    }

    void movePieceYB(Transform parent, Transform child, float x, float y){
        if (parent.transform.position.y < child.transform.position.y){
            parent.transform.position = new Vector3(child.transform.position.x - x, child.transform.position.y + y, child.transform.position.z);
        }
        else{
            parent.transform.position = new Vector3(child.transform.position.x + x, child.transform.position.y - y, child.transform.position.z);
        }
    }

    void movePieceYC(Transform parent, Transform child, float x){
    
        if (parent.transform.position.x < child.transform.position.x){
            parent.transform.position = new Vector3(child.transform.position.x + x, child.transform.position.y, child.transform.position.z);
        }
        else{
            parent.transform.position = new Vector3(child.transform.position.x - x, child.transform.position.y, child.transform.position.z);
        }
    }

    void movePieceYD(Transform parent, Transform child, float x){
    
        if (parent.transform.position.x < child.transform.position.x){
            parent.transform.position = new Vector3(child.transform.position.x - x, child.transform.position.y, child.transform.position.z);
        }
        else{
            parent.transform.position = new Vector3(child.transform.position.x + x, child.transform.position.y, child.transform.position.z);
        }
    }

    void movePieceYE(Transform parent,Transform child,float y){
    
        if (parent.transform.position.y < child.transform.position.y){
            parent.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + y, child.transform.position.z);
        }
        else{
            parent.transform.position = new Vector3(child.transform.position.x, child.transform.position.y - y, child.transform.position.z);
        }
    }

    void movePieceYF(Transform parent,Transform child, float y){
    
        if (parent.transform.position.y < child.transform.position.y){
            parent.transform.position = new Vector3(child.transform.position.x, child.transform.position.y - y, child.transform.position.z);
        }
        else{
            parent.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + y, child.transform.position.z);
        }
    }
}
