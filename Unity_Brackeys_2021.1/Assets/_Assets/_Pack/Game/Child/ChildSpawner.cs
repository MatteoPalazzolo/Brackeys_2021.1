using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawner : MonoBehaviour
{
    Manager manager;
    Settings settings;

    List<Transform> spawners = new List<Transform>();
    List<GameObject> childrensPrefabs;
    int childToSpawn;
    

    private void Awake() {
        manager = FindObjectOfType<Manager>();
        settings = manager.settings;
        childrensPrefabs = settings.childrensPrefabs;
        childToSpawn = settings.childToSpawn;
        //BUILD LIST
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
            spawners.Add(transform.GetChild(i));
        foreach (Transform child in spawners) child.gameObject.SetActive(false);
    }

    private void Start() {
        while (childToSpawn > 0) {
            int index1 = Random.Range(0, childrensPrefabs.Count);
            GameObject childPrefab = childrensPrefabs[index1];
            int index2 = Random.Range(0, spawners.Count);
            Instantiate(childPrefab, spawners[index2].position, Quaternion.identity, manager.childrensParent);
            childToSpawn--;
        }

    }

}
