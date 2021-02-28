using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush_Spawn : MonoBehaviour
{
    //REFERENCES
    Manager manager;
    Emote_UI emoteUI;

    //VARIABLES
    bool canSpawn = false;
    List<Transform> childrens = new List<Transform>();
    List<GameObject> childrensPrefabs;

    void Awake() {
        BuildList();
        manager = FindObjectOfType<Manager>();
        childrensPrefabs = manager.settings.childrensPrefabs;
    }

    private void BuildList() {
        Transform mesh = transform.GetChild(0);
        int count = mesh.childCount;
        for (int i = 0; i < count; i++)
            childrens.Add(mesh.GetChild(i));
    }

    void Start() {
        HideDebugObj();
        CrateUI();
        canSpawn = Random.Range(0f,2f) < 1;
    }

    private void CrateUI()
    {
        emoteUI = Instantiate(manager.UIPrefab, manager.UIParent).GetComponent<Emote_UI>();
        emoteUI.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        emoteUI.target = transform.GetChild(1);
    }

    private void HideDebugObj() {
        foreach (Transform child in childrens)
            child.GetComponent<MeshRenderer>().enabled = false;
    }

    public bool TryToSpawn() {
        if (canSpawn) {
            canSpawn = false;
            SpawnChild();
            emoteUI.DisplayExclamMark(2f);
            return true;
        }
        emoteUI.DisplayText("I'm empty", 2f);
        return false;
    }

    private void SpawnChild() {
        int index1 = Random.Range(0, childrensPrefabs.Count);
        GameObject childPrefab = childrensPrefabs[index1];
        int index2 = Random.Range(0, childrens.Count);
        Vector3 spawnPos = childrens[index2].position;
        Instantiate(childPrefab, spawnPos, Quaternion.identity, manager.childrensParent);
    }

}
