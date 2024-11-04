using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp_GravityChange : MonoBehaviour{
    private GameObject parentSpawner;
    const float baseWaitTime = 2f;
    private float waitTime;

    private void AdjustWaitTime(){
        waitTime = baseWaitTime + Director.AdjustPotency();
    }

    private void FlipGravity(GameObject targetObj){
        PlayerPowerUpManager p = targetObj?.GetComponent<PlayerPowerUpManager>();

        if (p.GetGravityStatus() == false){

            AdjustWaitTime();
            Destroy(gameObject);
            p.FlipGravity(waitTime, p?.GetComponent<PlayerMovement>());
        }
    }

    
    /*
    private void OnTriggerEnter(Collider other){
        FlipGravity(other.gameObject);
    }
    */
    private void OnCollisionEnter(Collision other){
        if(other.gameObject.GetComponent<PlayerMovement>()){
            FlipGravity(other.gameObject);
        }
    }

    public void SetParentSpawner(GameObject spawner){
        parentSpawner = spawner;
    }
}
