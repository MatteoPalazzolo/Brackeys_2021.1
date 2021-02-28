using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    //VARIABLES
    string colliderTag;
    GameObject colliderObject;

    Player player;
    void Awake() {
        player = GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other) {
        colliderTag = other.gameObject.tag;
        colliderObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other) {
        colliderTag = "";
        colliderObject = null;
    }

    void Update() {
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        if (!Input.GetKeyDown(player.interact)) return;

        switch (colliderTag) {
            case "Bush":
                Bush();
                break;

            case "Child":
                Child();
                break;

            case "Interact":
                Interact();
                break;

            default:
                Debug.LogWarning("Tag not found");
                break;
        }
    }
    
    private void Bush() {
        bool spawned = colliderObject.GetComponent<Bush_Spawn>().TryToSpawn();
    }

    private void Child() {
        colliderObject.GetComponent<Child>().TouchChild();
    }

    private void Interact() {
        colliderObject.GetComponent<Emote_Object>().DisplayText();
    }

    /*
    private void Tree() {
        string text = "You aren't looking for monkeys";
        colliderObject.GetComponent<Emote_Object>().emoteUI.DisplayText(text, 2f);
    }

    private void Lamp() {
        string text = "Here!? Seriously?";
        colliderObject.GetComponent<Emote_Object>().emoteUI.DisplayText(text, 2f);
    }*/

}
