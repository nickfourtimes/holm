//using Whilefun.FPEKit;
using UnityEngine;
using System.Collections;


public class TowerEntering : MonoBehaviour {

    public Collider player;
    public Transform teleportPoint;


    // -------------------------------------------------------------------------------------------- MONOBEHAVIOUR METHODS

    IEnumerator Start() {
        while(null == player) {
            //var fpe = FindObjectOfType<FPEPlayer>();
            //if(fpe) {
            //    player = fpe.GetComponent<Collider>();
            //    break;
            //}

            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }


    void OnTriggerEnter(Collider other) {
        if(other == player) {
            player.transform.position = teleportPoint.position;
        }

        return;
    }
}
