using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour{
    [SerializeField] private GameObject[] prefabs_powerUpsToSpawn;
    [SerializeField] private List<GameObject> currentSpawnedPowerups;

    [SerializeField] private Material availableMaterial;
    [SerializeField] private Material cooldownMaterial;
    

    private const int baseSpawnAmount = 2;
    private int spawnLimit;

    private const float timeBeforeSpawn = 3f;
    private float timer = 0f;
    private bool isOnCooldown = false;

    // Update is called once per frame
    void Update(){
        if(isOnCooldown == true){
            timer += Time.deltaTime;
        }
        if(timer >= timeBeforeSpawn && isOnCooldown == true){
            isOnCooldown = false;
            timer = 0f;
            gameObject.GetComponent<Renderer>().material = availableMaterial;
        }
    }


    private void OnTriggerEnter(Collider other){
        ////Debug.Log("Colliding with spawner");
        //if they are dj player
        if(other.gameObject.GetComponent<PlayerMovement>() != null){
            if(isOnCooldown == false){

                if(other.gameObject == Director.GetLastPlace()){
                    spawnLimit = baseSpawnAmount + Director.AdjustPotency()+1;
                }
                else{
                    spawnLimit = baseSpawnAmount;
                }


                Debug.Log("Set spawn limit to: " + spawnLimit);
                //Call dj function to spawn the orbs and
                SpawnPowerups(spawnLimit, other.gameObject);

                gameObject.GetComponent<Renderer>().material = cooldownMaterial;
            }
        }
    }


    private void SpawnPowerups(int spawnLimit, GameObject player){
        ////Debug.Log("Initiating the power up spawning...");

        int i = currentSpawnedPowerups.Count;
        Debug.Log("i = " + i);
        GameObject temp;
        Vector3 pos;

        while(i <= spawnLimit){
            int r = Random.Range(0, prefabs_powerUpsToSpawn.Count());
            pos = new Vector3(Random.Range(0,3),2 + Random.Range(0,3), Random.Range(0,3));

            temp = Instantiate(prefabs_powerUpsToSpawn[r], (transform.position + pos), transform.rotation);
            ////Debug.Log("PowerUp has been spawned....");


            //if(temp.GetComponent<PowerUp_ShrinkSize>() != null){
            //    temp.GetComponent<PowerUp_ShrinkSize>().SetParentSpawner(gameObject);
            //}
            //else if(temp.GetComponent<PowerUp_GravityChange>() != null){
            //    temp.GetComponent<PowerUp_GravityChange>().SetParentSpawner(gameObject);
            //}
            //else if(temp.GetComponent<PowerUp_JumpIncrease>() != null){
            //    temp.GetComponent<PowerUp_JumpIncrease>().SetParentSpawner(gameObject);
            //}
            //else if(temp.GetComponent<PowerUp_SpeedIncrease>() != null){
            //    temp.GetComponent<PowerUp_SpeedIncrease>().SetParentSpawner(gameObject);
            //}

            if(temp.TryGetComponent<IPowerupSetter>(out var powerupSetter)) 
            {
                powerupSetter.SetParentSpawner(gameObject);

                if(player == Director.GetLastPlace())
                {
                    powerupSetter.AffectPowerupPotency();
                }
            }


            temp.GetComponent<Rigidbody>().AddForce(new Vector3 (Random.Range(0,3) ,Director.AdjustPotency() + Random.Range(0,3), Random.Range(0,3)), ForceMode.Impulse);
            currentSpawnedPowerups.Add(temp);
            i++; //Was causing an infinite loop....
        }
        isOnCooldown = true;
    }

    public void RemovePowerUpFromList(GameObject powerUp){
        currentSpawnedPowerups.Remove(powerUp);
    }











}
