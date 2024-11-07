using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerSpawnLocations;


    private void Awake()
    {
        SceneManager.LoadScene("MultiplayerRaceStarter", LoadSceneMode.Additive);

        Director.OnRaceEnded += StartDelayRaceCoroutine;

    }

    private void Start()
    {
        InitalizeRace();
    }

    public void InitalizeRace()
    {
        PlayerReferenceCollector players = FindObjectOfType<PlayerReferenceCollector>();
        players.playerList[0].gameObject.transform.position = playerSpawnLocations[0].transform.position;
        players.playerList[1].gameObject.transform.position = playerSpawnLocations[1].transform.position;
    }



    private void StartDelayRaceCoroutine()
    {
        StartCoroutine(DelayReloadRaceRoutine());
    }

    private IEnumerator DelayReloadRaceRoutine()
    {
        yield return new WaitForSeconds(2f);
        ReloadRace();
    }

    private void ReloadRace()
    {
        SceneManager.UnloadSceneAsync("MultiplayerRaceStarter");
        SceneManager.LoadScene("Sharria_TesterScene", LoadSceneMode.Single);
    }


}
