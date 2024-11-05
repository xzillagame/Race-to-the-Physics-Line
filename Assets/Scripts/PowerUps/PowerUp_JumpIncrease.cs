using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_JumpIncrease : MonoBehaviour{
    private GameObject parentSpawner;
    private const int baseIncrease = 1;
    private int statIncrease;

    

    private void AdjustStatIncrease(){
        statIncrease = baseIncrease + Director.AdjustPotency();
    }


    private void OnCollisionEnter(Collision other) {
        PlayerMovement p = other.gameObject.GetComponent<PlayerMovement>();
        if(p != null){
            AdjustStatIncrease();
            p.JumpStrength += statIncrease;

            parentSpawner.GetComponent<PowerUpSpawner>().RemovePowerUpFromList(gameObject);
            Destroy(gameObject);
        }
    }

    public void SetParentSpawner(GameObject spawner){
        parentSpawner = spawner;
    }

}
