using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class PowerUp_ShrinkSize : MonoBehaviour{
    const float baseTargetShrinkage = 1.0f;
    [SerializeField] float targetShrinkageSize; //Serialize for debugging purposes

    private void AdjustTargetSize(){
        targetShrinkageSize = baseTargetShrinkage + Director.AdjustPotency();
    }
    private void ChangeTargetObjectSize(GameObject targetObj){
        AdjustTargetSize();

        if(Director.IsShrinkValid(targetShrinkageSize, targetObj) == true){
            targetObj.transform.localScale -= new Vector3(targetShrinkageSize,targetShrinkageSize,targetShrinkageSize);
        }
        else{
            targetObj.transform.localScale = Vector3.one;
        }
    }

    private void OnTriggerEnter(Collider other) {
        //This is currently assuming that the other object is the player at this moment
        ChangeTargetObjectSize(other.gameObject);
        Destroy(gameObject);
    }
}
