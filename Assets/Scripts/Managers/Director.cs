using UnityEngine;

public class Director : MonoBehaviour{
    private static bool isGameOver = false;
    //private static PlayerInputHandler[] pInputs;
    private static MultiplayerInputManager pInputs;

    private void Start(){
        //pInputs = FindObjectsOfType<PlayerInputHandler>();
        pInputs = FindObjectOfType<MultiplayerInputManager>();
    }


    public static void EndGame(){
        isGameOver = true;
        //Add stuff to multiplayer
        //foreach(PlayerInputHandler p in pInputs){
        //    p.EndGame();
        //}
        pInputs.EndGame();
        //End the game
        Debug.Log("Game Over");
    }

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
