using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnButton : MonoBehaviour
{
    private System.Random _random = new System.Random();
    public GameObject[] prefabs;
    public GameObject[] spawnPosition;
    private Stack<GameObject> pieces;
    private GameObject parent;
    private int[] angles = { 0, 45, 90, 135, 180, 225, 270, 315, 360 };
    int count,total;


    void Start()
    {
        pieces = new Stack<GameObject>();
        Shuffle(prefabs);
        count = 0;
        total = prefabs.Length;
        parent = GameObject.Find("Pieces");
        foreach (GameObject prefab in prefabs)
        {
            pieces.Push(prefab);
        }
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = count + " / " + total;

    }

    public void SpawnPieces()
    {
        int amount = 0;
        int angle;
        SpriteRenderer sprite;
        GameObject spawn;
        MovePiece moveScript;
        while(amount < 3 && pieces.Count > 0)
        {
            spawn = pieces.Pop();
            angle = angles[Random.Range(0, angles.Length)];
            spawn =  Instantiate(spawn, spawnPosition[amount].transform.position, Quaternion.Euler(0, 0, angle));
            spawn.transform.parent = parent.transform;
            sprite = spawn.GetComponent<SpriteRenderer>();
            sprite.sortingOrder = amount;
            sprite.sortingLayerName = "Pieces";
            moveScript = spawn.GetComponent<MovePiece>();
            moveScript.justSpawn = true;
            Debug.Log(amount);
            count++;
            amount++;
        }

        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = count + " / " + total;
    }

    void Shuffle(GameObject[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(1, n);
            GameObject t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }

}
