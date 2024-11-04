using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour{
    [SerializeField] private GameObject[] prefabs_powerUpsToSpawn;
    [SerializeField] private List<GameObject> currentSpawnedPowerups;


    private const int baseSpawnAmount = 2;
    private int spawnLimit;


    private const float timeBeforeSpawn = 3f;
    private float timer = 0f;
    private bool isOnCooldown = false;

    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(isOnCooldown == true){
            timer += Time.deltaTime;
        }
        if(timer >= timeBeforeSpawn && isOnCooldown == true){
            isOnCooldown = false;
            timer = 0f;
        }
    }


    private void OnTriggerEnter(Collider other){
        Debug.Log("Colliding with spawner");
        //if they are a player
        if(other.gameObject.GetComponent<PlayerMovement>() != null){
            if(isOnCooldown == false){
                if(other.gameObject == Director.GetLastPlace()){
                    spawnLimit = baseSpawnAmount + Director.AdjustPotency();
                    //Call a function to spawn the orbs and
                    SpawnPowerups(spawnLimit);
                }   
            }
        }
    }


    private void SpawnPowerups(int spawnLimit){
        Debug.Log("Initiating the power up spawning...");

        int i = currentSpawnedPowerups.Count;
        GameObject temp;

        while(i <= spawnLimit){
            int r = Random.Range(0, prefabs_powerUpsToSpawn.Count());
            temp = Instantiate(prefabs_powerUpsToSpawn[r]);
            temp.transform.position += new Vector3(Random.Range(0,3),2 + Random.Range(0,3), Random.Range(0,3));
            Debug.Log("PowerUp has been spawned....");


            if(temp.GetComponent<PowerUp_ShrinkSize>() != null){
                temp.GetComponent<PowerUp_ShrinkSize>().SetParentSpawner(gameObject);
            }
            else{
                temp.GetComponent<PowerUp_GravityChange>().SetParentSpawner(gameObject);
            }


            temp.GetComponent<Rigidbody>().AddForce(new Vector3 (Random.Range(0,3) ,Director.AdjustPotency() + Random.Range(0,3), Random.Range(0,3)), ForceMode.Impulse);
            currentSpawnedPowerups.Add(temp);
        }

        isOnCooldown = true;


    }

    public void RemovePowerUpFromList(GameObject powerUp){
        currentSpawnedPowerups.Remove(powerUp);
    }











}
