using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_GravityChange : MonoBehaviour{
    private void FlipGravity(GameObject targetObj){
        PlayerMovement p = targetObj.GetComponent<PlayerMovement>();
        //
        if (p.GetGravityStrength() < 0f){

            p.FlipGravity();
        }
        else{
            p.NormalizeGravity();
        }
    }
    private void OnTriggerEnter(Collider other){
        FlipGravity(other.gameObject);
        Destroy(gameObject);
    }
}
