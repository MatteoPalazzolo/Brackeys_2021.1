using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Children Settings")]
public class Settings : ScriptableObject {
    [Header("General Childrens Values")]
    public List<GameObject> childrensPrefabs;
    public float idleWalkRange = 10f;
    public float escapeTriggerDistance = 10f;
    public float escapeUntriggerDistance = 20f;
    public float fleeDistance = 5f;
    [Min(2)] public float collisionDistance = 5f;
    public float checkRotationAngle = 5f;

    [Header("Game")]
    [Tooltip("In sec")]
    [Min(0)] public int gameTimer = 120;
    [Min(0)] public int childToSpawn = 10;

    [Header("Stamina")]
    [Min(0)] public int maxStamina = 100;
    [Min(0)] public int decreaseStamina = 1;
    [Min(0)] public int restoreStamina = 1;
    [Min(0)] public float preRestoreTime = 1;

}
