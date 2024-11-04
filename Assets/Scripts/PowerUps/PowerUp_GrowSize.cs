/*

using UnityEngine;

public class PowerUp_GrowSize : MonoBehaviour{
    const float baseTargetGrowth = 1.0f;
    [SerializeField] float targetGrowthSize; //Serialize for debugging purposes

    private void AdjustTargetSize(){
        targetGrowthSize = baseTargetGrowth + Director.AdjustPotency();
    }
    private void ChangeTargetObjectSize(GameObject targetObj){
        AdjustTargetSize();
        targetObj.transform.localScale += new Vector3(targetGrowthSize,targetGrowthSize,targetGrowthSize);
    }

    private void OnTriggerEnter(Collider other) {
        //This is currently assuming that the other object is the player at this moment
        ChangeTargetObjectSize(other.gameObject);
        Destroy(gameObject);
    }
}
*/