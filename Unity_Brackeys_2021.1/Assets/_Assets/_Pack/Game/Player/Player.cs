using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    //PARAMETES
    [Header("Movement")]
    public Transform playerTransform;
    public float walkSpeed = 5f;
    public float runSpeed = 7f;
    public float turnSmoothTime = .1f;

    [Header("KeyCodes")]
    public KeyCode run = KeyCode.LeftShift;
    public KeyCode interact = KeyCode.Space;

    //REFERENCES
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Manager manager;
    [HideInInspector] public Settings settings;
    public bool isRunning;

    void Awake() {
        characterController = GetComponent<CharacterController>();
        manager = FindObjectOfType<Manager>();
        settings = manager.settings;
    }

}
