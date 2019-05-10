using UnityEngine;
using System.Collections;

public class FpsCursor : MonoBehaviour {

    // -------------------------------------------------------------------------------------------- DATA MEMBERS

    public Texture normalTex;
    public Texture targetTex;
    public float tszMult = 0.25f;

    public LayerMask towerLayerMask;
    int layer;

    Rect guiRect;
    Vector3 screenPt = new Vector3(0.5f, 0.5f, 0.0f);

    Transform targetDestination;
    public Transform Destination
    {
        get { return targetDestination; }
    }

    bool onTarget = false;
    public bool OnTarget
    {
        get { return onTarget; }
    }


    // -------------------------------------------------------------------------------------------- UNITY METHODS

    void Start() {
        float sw = Screen.width;
        float sh = Screen.height;
        float tw = normalTex.width * tszMult;
        float th = normalTex.height * tszMult;

        guiRect = new Rect(0.5f * (sw - tw), 0.5f * (sh - th), tw, th);

        layer = 1 << LayerMask.NameToLayer("tower");

        return;
    }


    void Update() {
        Ray ray = Camera.main.ViewportPointToRay(screenPt);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layer)) {
            onTarget = true;
            //targetDestination = null;
            foreach(Transform t in hit.transform) {
                if(t.name.Contains("teleportPoint")) {
                    targetDestination = t;
                    break;
                }
            }

        } else {
            onTarget = false;
        }

        return;
    }


    void OnGUI() {
        if (onTarget) {
            GUI.DrawTexture(guiRect, targetTex);
        } else {
            GUI.DrawTexture(guiRect, normalTex);
        }

        return;
    }
}
