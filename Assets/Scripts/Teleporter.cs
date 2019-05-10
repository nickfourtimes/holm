using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Teleporter : MonoBehaviour {

    // --------------------------------------------------------------------------------------------- DATA MEMBERS

    public GameObject playerObj;
    public List<MonoBehaviour> playerInputComponents = new List<MonoBehaviour>();

    public float teleportTime = 1.0f;
    public float maxFov = 120;

    FpsCursor targetChecker;


    // --------------------------------------------------------------------------------------------- METHODS

    void CheckToTeleport() {
        if (targetChecker.OnTarget) {
            StartCoroutine(DoTeleport());
        }

        return;
    }


    IEnumerator DoTeleport() {
        Vector3 startPos = playerObj.transform.position;
        Vector3 dest = targetChecker.Destination.position;

        // disable input
        foreach (MonoBehaviour m in playerInputComponents) {
            m.enabled = false;
        }

        float along = 0;
        float startTime = Time.time;
        float startFov = Camera.main.fieldOfView;
        while(1.0f > along) {
            along = (Time.time - startTime) / teleportTime;
            playerObj.transform.position = Vector3.Lerp(startPos, dest, along);

            // fov
            float fovalong = Mathf.Abs(along - 0.5f);
            Camera.main.fieldOfView = startFov + fovalong * (maxFov - startFov);

            yield return null;
        }

        playerObj.transform.position = dest;
        Camera.main.fieldOfView = startFov;

        yield return null;
        
        // re-enable input
        foreach (MonoBehaviour m in playerInputComponents) {
            m.enabled = true;
        }
    }


    // --------------------------------------------------------------------------------------------- UNITY METHODS

    void Start() {
        if (null == playerObj) {
            playerObj = gameObject;
        }

        targetChecker = GameObject.FindObjectOfType<FpsCursor>();

        return;
    }


    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            CheckToTeleport();
        }

        return;
    }
}
