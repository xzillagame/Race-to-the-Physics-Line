using UnityEngine;

[CreateAssetMenu(fileName = "Player Default Starting Stats", menuName = "SciptableObjects/Player Default Starting Stats")]
public class PlayerStatCostants : ScriptableObject
{
    [field: SerializeField] public float StartingMovementSpeed { get; private set; } = 5f;
    [field: SerializeField] public float StartingJumpStrength { get; private set; } = 15f;
    [field: SerializeField] public float StartingGravityStrength { get; private set; } = -25f;

}
