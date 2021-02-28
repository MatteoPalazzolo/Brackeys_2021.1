using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

enum State {Idle, Escape, Catched};

[RequireComponent(typeof(NavMeshAgent))]
public class Child : MonoBehaviour {

    public int strenght = 0;
    [SerializeField] float speed = 0;

    //REFERENCES
    NavMeshAgent navMeshAgent;
    Animator animator;
    Manager manager;
    Settings settings;

    Transform player;
    BellTarget bellTarget;

    Emote_UI emoteUI;

    //STATES
    State state = State.Idle;
    bool isCatched = false;
    bool bellReached = false;
    bool lastTrigger = true;


    void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<Manager>();
        CrateUI();
    }

    void Start() {
        settings = manager.settings;
        player = manager.player;
        bellTarget = manager.bellTarget;
        navMeshAgent.speed = speed;
    }

    void CrateUI() {
        emoteUI = Instantiate(manager.UIPrefab, manager.UIParent).GetComponent<Emote_UI>();
        emoteUI.target = transform.GetChild(1);
    }

    void Update()
    {
        SetState();
        SwitchState();
        IsMoving();
    }

    private void IsMoving()
    {
        bool isMoving = navMeshAgent.remainingDistance > .1f;
        animator.SetBool("Walk", isMoving);
        animator.SetBool("Idle", !isMoving);
    }

    private void SetState()
    {
        emoteUI.SetExclamationMark(state == State.Escape);

        if (isCatched) return;

        bool isNear = Vector3.Distance(player.position, transform.position) < settings.escapeTriggerDistance;
        if (isNear && state != State.Escape) {
            state = State.Escape;
        }

        bool isFar = Vector3.Distance(player.position, transform.position) > settings.escapeUntriggerDistance;
        if (isFar && state != State.Idle)
            state = State.Idle;
    }

    private void SwitchState() {
        if (state == State.Idle) Idle();
        if (state == State.Escape) Escape();
        else if (state == State.Catched) Catched();
    }

    private void Idle()
    {
        if (Random.Range(0, 100) < 1) {
            float x = Random.Range(-settings.idleWalkRange, settings.idleWalkRange) + transform.position.x;
            float y = Random.Range(-settings.idleWalkRange, settings.idleWalkRange) + transform.position.y;
            navMeshAgent.SetDestination(new Vector3(x,0,y));
        }
    }

    private void Escape() {
        //Vector3 dir = transform.position - player.position;
        Vector3 dir;// = transform.position - player.position;

        bool raycast = false;
        float angle = -settings.checkRotationAngle;
        bool mirror = false;
        do {
            if (!mirror) angle += settings.checkRotationAngle;
            dir = RotateVector(Vector3.Normalize(transform.position - player.position), angle, mirror);
            raycast = Physics.Raycast(transform.position, dir, settings.collisionDistance, 0);
            Debug.DrawRay(transform.position, dir.normalized * settings.collisionDistance, Color.magenta);
            mirror = !mirror;
        } while (raycast);

        //navMeshAgent.pathStatus = NavMeshPathStatus.;
        navMeshAgent.SetDestination(transform.position + (dir.normalized * settings.fleeDistance));
    }

    Vector3 RotateVector(Vector3 vector, float rotationY, bool mirror) {
        float ang = rotationY;
        if (mirror) ang *= -1;
        Vector3 newVector = new Vector3();
        newVector.x = Mathf.Cos(ang) * vector.x + Mathf.Sin(ang) * vector.y;
        newVector.z = -Mathf.Sin(ang) * vector.x + Mathf.Cos(ang) * vector.y;
        return newVector;
    }

    private void Catched() {
        if (!isCatched) {
            bellTarget.totalStrenght += strenght;
            navMeshAgent.SetDestination(bellTarget.GetSpot().position);
            isCatched = true;
        }

        bellReached = navMeshAgent.remainingDistance < .1f;
        if (bellReached) {
            RotateTowardsPlayer();
            DisplayLastMessage();
        }

    }

    private void RotateTowardsPlayer() {
        Vector3 dir = manager.player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = rotation;
    }

    private void DisplayLastMessage() {
        if (!lastTrigger) return;
        lastTrigger = false;
        emoteUI.DisplayText("I will help you...", 3f);
    }

    public void TouchChild() {
        if (!isCatched) {
            emoteUI.DisplayText("Oh, you catched me!", 3f);
            Catched();
            state = State.Catched;
        }
        else if (bellReached) {
            emoteUI.DisplayText("I will help you...", 3f);
        }

    }

}
