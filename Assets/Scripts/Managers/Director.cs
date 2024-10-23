using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour{
    [NonSerialized] public static float GRAVITY = -40;

    
    public static float AdjustPotency(){
        //Will get a random number for testing purposes
        int potency = UnityEngine.Random.Range(0, 101);
        //0-30 is low distance between players
        if (potency >= 0 && potency <= 30){
            Debug.Log("Low distance");
            return 0;
        }
        //31-60 is medium distance between players
        else if(potency >= 31 && potency <= 60){
            Debug.Log("Medium distance");
            return 1;
        }
        //61+ is high distance for players
        else{
            Debug.Log("High distance");
            return 2;
        }
    }

    public static 
    bool IsShrinkValid(float shrinkAmount, GameObject targetObj){
        //All scales should match, so I just chose to use x
        if ((targetObj.transform.localScale.x - shrinkAmount) <= 0){
            return false;
        }
        else{
            return true;
        }
    }
}
