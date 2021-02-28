using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellTarget : MonoBehaviour
{
    public int totalStrenght = 0;

    List <Transform> targets = new List<Transform>();
    EndGameMenu endGameMenu;

    void Awake()
    {
        endGameMenu = FindObjectOfType<EndGameMenu>();
        //BUILD LIST
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
            targets.Add(transform.GetChild(i));
    }

    void Start()
    {
        //HIDE IN GAME
        GetComponent<MeshRenderer>().enabled = false;
    }

    public Transform GetSpot()
    {/*
        if (targets.Count == 0)
        {
            Debug.LogWarning("No More Space Avable");
            return null;
        }*/
        int index = Random.Range(0, targets.Count);
        Transform target = targets[index];
        targets.RemoveAt(index);
        if (targets.Count == 0) {
            endGameMenu.EndGame(EndType.MaxChild);
        }
        return target;
    }

}
