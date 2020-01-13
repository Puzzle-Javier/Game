using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

    public static Globals instance;
    public int selectedPieces = 0;
    // Start is called before the first frame update
    void Start()
    {
        // DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = this;
        }else if(instance != this){
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
