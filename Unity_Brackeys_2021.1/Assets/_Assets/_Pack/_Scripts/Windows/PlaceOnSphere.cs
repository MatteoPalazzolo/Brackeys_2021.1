using UnityEngine;
using UnityEditor;

public class PlaceOnSphere : EditorWindow {
    [MenuItem("Window/Level Design/Place On Sphere")]
    public static void ShowWindow() {
        GetWindow<PlaceOnSphere>();
    }

    //public AudioBehaviour audio {get; set;}
    Transform planet;
    int mask;

    void OnEnable() {
        mask = planet.gameObject.layer;
    }

    void OnGUI() {
        DrawAutoRotate();
        GUILayout.Space(5f);
        DrawRandomizeRotation();
    }

    private void DrawAutoRotate() {
        GUILayout.Label("Auto Place", EditorStyles.boldLabel);
        planet = (Transform)EditorGUILayout.ObjectField(new GUIContent("Target Planet"), planet, typeof(Transform), true);
        if (GUILayout.Button("Place")) {
            AutoRotate();
            RaycstPosition();
            //Debug.DrawRay(Vector3.zero, Vector3.forward, Color.red);
        }
    }

    void AutoRotate() {
        foreach (GameObject obj in Selection.gameObjects) {
            if (obj == planet) continue;
            float rotationZ = obj.transform.rotation.eulerAngles.z;
            Vector3 dir = obj.transform.position - planet.position;
            Quaternion rotation = Quaternion.LookRotation(dir.normalized);
            rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotationZ);
            obj.transform.rotation = rotation;
        }
    }

    void RaycstPosition() {
        foreach (GameObject obj in Selection.gameObjects) {
            if (obj == planet) continue;
            RaycastHit hit;
            Vector3 dir = (obj.transform.position - planet.position).normalized;

            float planetRadius = 25f;
            bool castUp = Physics.Raycast(obj.transform.position, dir, out hit, planetRadius/*, mask*/);
            Debug.DrawRay(obj.transform.position, obj.transform.forward, Color.red);
            if (castUp) obj.transform.position = hit.point;

            float distance = (obj.transform.position - planet.transform.position).magnitude;
            bool castDown = Physics.Raycast(obj.transform.position, dir * -1, out hit, distance/*, mask*/);
            Debug.DrawRay(obj.transform.position, obj.transform.forward * -1, Color.red);
            if (castDown) obj.transform.position = Vector3.zero;
        }
    }

    void DrawRandomizeRotation() {
        GUILayout.Label("Randomize Rotation", EditorStyles.boldLabel);
        if (GUILayout.Button("Randomize")) RandomizeRotation();
    }

    void RandomizeRotation() {
        foreach (GameObject obj in Selection.gameObjects) {
            float randomZ = Random.Range(0, 360);
            Vector3 rotation = obj.transform.localRotation.eulerAngles;
            rotation.z = randomZ;
            obj.transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}
