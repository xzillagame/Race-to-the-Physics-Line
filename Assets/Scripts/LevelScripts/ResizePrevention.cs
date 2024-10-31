using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePrevention : MonoBehaviour{
    private void OnTriggerEnter(Collider other) {
        other.gameObject?.GetComponent<PlayerPowerUpManager>().SwitchLowWallStatus();
    }
    private void OnTriggerExit(Collider other) {
        other.gameObject?.GetComponent<PlayerPowerUpManager>().SwitchLowWallStatus();
    }
}
