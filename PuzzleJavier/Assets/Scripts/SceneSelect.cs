using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{

    public void SelectScene()
    {
        switch (this.gameObject.name)
        {
            case "Level1":
                SceneManager.LoadScene("SampleScene");
                break;
        }
    }
}
