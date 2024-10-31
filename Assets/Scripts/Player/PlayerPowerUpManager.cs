using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpManager : MonoBehaviour{
    private bool isShrunk;
    private bool isInLowWall = false;
    public void ShrinkSize(float targetSize, float powerUpTimer){
        //!Already checks this in the power up manager
        //if(Director.IsShrinkValid(targetSize, gameObject) == true){
            transform.localScale = new Vector3(targetSize,targetSize,targetSize);

            StartCoroutine(shrinkTimer(powerUpTimer));
        //}
        
    }



    private IEnumerator shrinkTimer(float timer){
        yield return new WaitForSeconds(timer);

        while(isInLowWall == true){
            yield return new WaitForSeconds(.1f);
        }
        transform.localScale = Vector3.one;
        isShrunk = false;
    }
    private IEnumerator gravityTimer(float timer){
        yield return new WaitForSeconds(timer);
        
        isShrunk = false;
    }



    public bool GetShrinkStatus(){
        return isShrunk;
    }
    public void SwitchLowWallStatus(){
        isInLowWall = !isInLowWall;
    }
}
