using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    public GameObject pieces;
    public GameObject image;
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickMap()
    { 

        if (image.activeSelf)
        {
            image.SetActive(false);
            pieces.SetActive(true);
            button.SetActive(true);
        }
        else
        {
            image.SetActive(true);
            pieces.SetActive(false);
            button.SetActive(false);
        }
    }
}
