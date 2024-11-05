using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class PowerUp_ShrinkSize : MonoBehaviour{
    private GameObject parentSpawner;
    const float baseTargetShrinkage = .5f;
    const float baseWaitTime = 2f;
    [SerializeField] float waitTime;

    private void AdjustWaitTime(){
        waitTime = baseWaitTime + Director.AdjustPotency();
    }

    /*
    private void OnTriggerEnter(Collider other) {
        if(Director.IsShrinkValid(baseTargetShrinkage, other.gameObject) == true){
            AdjustWaitTime();
            other?.GetComponent<PlayerPowerUpManager>().ShrinkSize(baseTargetShrinkage, waitTime);
            Destroy(gameObject);
        }
    }
    */

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponent<PlayerMovement>()){
            if(Director.IsShrinkValid(baseTargetShrinkage, other.gameObject) == true){
                AdjustWaitTime();
                other.gameObject?.GetComponent<PlayerPowerUpManager>().ShrinkSize(baseTargetShrinkage, waitTime);

                parentSpawner.GetComponent<PowerUpSpawner>().RemovePowerUpFromList(gameObject);
                Destroy(gameObject);
            }
        }
    }

    public void SetParentSpawner(GameObject spawner){
        parentSpawner = spawner;
    }











}
