using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_FlipGravity : MonoBehaviour{
    private void FlipGravity(ConstantForce relativeGravity){
        relativeGravity.force = -relativeGravity.force;
    }
    private void OnTriggerEnter(Collider other){
        //this is yn
    }
}
