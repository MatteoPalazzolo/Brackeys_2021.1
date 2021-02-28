using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_Movement : MonoBehaviour
{
    //REFERENCES
    Player player;
    Manager manager;
    Stamina stamina;
    Animator animator;

    //VARIABLES
    float turnSmoothVelocity;
    float speed;

    void Awake() {
        player = GetComponent<Player>();
        player.characterController = GetComponent<CharacterController>();
        manager = FindObjectOfType<Manager>();
        stamina = manager.canvas.GetComponent<Stamina>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        RunOrWalk();
        Move();
        Gravity();
    }

    private void RunOrWalk() {
        bool isKeyDown = Input.GetKey(player.run);
        bool enoughtStamina = stamina.stamina > 0;
        player.isRunning = isKeyDown && enoughtStamina;
        speed = player.isRunning ? player.runSpeed : player.walkSpeed;
        if (player.isRunning) stamina.DecreaseStamina();
    }

    private void Move() {
        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        Vector3 direction = new Vector3(x, 0, y).normalized;

        bool isMoving = direction.magnitude >= .5f;
        animator.SetBool("Run", player.isRunning && isMoving);
        animator.SetBool("Walk", !player.isRunning && isMoving);
        animator.SetBool("Idle", !isMoving);
        if (isMoving) {
            Rotate(direction);
            player.characterController.Move(new Vector3(x, 0, y));
        }
    }

    private void Gravity() {
        float gravity = -2f;
        player.characterController.Move(new Vector3(0, gravity, 0));
    }

    private void Rotate(Vector3 dir) {
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(player.playerTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, player.turnSmoothTime);
        player.playerTransform.rotation = Quaternion.Euler(0, angle, 0);
    }

}
