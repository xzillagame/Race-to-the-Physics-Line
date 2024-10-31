using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpManager : MonoBehaviour{
    private bool isShrunk;
    private bool resizeValidity = true;

    private bool isInverseGravity = false;






    public void ShrinkSize(float targetSize, float powerUpTimer){
        isShrunk = true;
        transform.localScale = new Vector3(targetSize,targetSize,targetSize);

        StartCoroutine(shrinkTimer(powerUpTimer));
        
    }
    public void FlipGravity(float powerUpTimer, PlayerMovement pMove){
        //PlayerMovement pMove = gameObject?.GetComponent<PlayerMovement>();
        if(pMove != null){
            isInverseGravity = true;
            pMove.FlipGravity();
            StartCoroutine(gravityTimer(powerUpTimer, pMove));
        }
    }





    private IEnumerator shrinkTimer(float timer){
        yield return new WaitForSeconds(timer);

        while(resizeValidity == false){
            yield return new WaitForSeconds(.1f);
        }
        transform.localScale = Vector3.one;
        isShrunk = false;
    }
    private IEnumerator gravityTimer(float timer, PlayerMovement pMove){
        yield return new WaitForSeconds(timer);
        pMove.NormalizeGravity();
        
        isInverseGravity = false;
    }



    public bool GetShrinkStatus(){
        return isShrunk;
    }
    public void SwitchLowWallStatus(){
        resizeValidity = !resizeValidity;
    }
    public bool GetGravityStatus(){
        return isInverseGravity;
    }
    
}
