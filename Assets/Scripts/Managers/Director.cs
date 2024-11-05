using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour{
    //private static bool isGameOver = false;
    private static PlayerInputHandler[] pInputs;

    private void Start(){
        pInputs = FindObjectsOfType<PlayerInputHandler>();
    }

    static public GameObject GetLastPlace(){
        //!Hardcoding the fact that there will only be two players
        if(pInputs[0].gameObject.transform.position.z < pInputs[1].gameObject.transform.position.z){
            return pInputs[0].gameObject;
        }
        else{
            return pInputs[1].gameObject;
        }
    }


    public static void EndGame(){
        ////isGameOver = true;
        //End the game
        FindObjectOfType<MultiplayerInputManager>().EndGame();
        Debug.Log("Game Over");
    }

    public static int AdjustPotency(){
        //Will get a random number for testing purposes
        ////int potency = UnityEngine.Random.Range(0, 101);
        //0-30 is low distance between players
        if ((pInputs[0].gameObject.transform.position.z - pInputs[1].gameObject.transform.position.z) <= 35){
            ////Debug.Log("Low distance");
            return 0;
        }
        //31-60 is medium distance between players
        else if((pInputs[0].gameObject.transform.position.z - pInputs[1].gameObject.transform.position.z) <= 70){
            ////Debug.Log("Medium distance");
            return 1;
        }
        //61+ is high distance for players
        else{
            ////Debug.Log("High distance");
            return 2;
        }
    }
    public static bool IsShrinkValid(float shrinkAmount, GameObject targetObj){
        //All scales should match, so I just chose to use x
        if ((targetObj.transform.localScale.x - shrinkAmount) <= 0 || targetObj?.GetComponent<PlayerPowerUpManager>().GetShrinkStatus() == true){
            return false;
        }
        else{
            return true;
        }
    }
}
