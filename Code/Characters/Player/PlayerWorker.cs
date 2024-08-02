using UnityEngine;

public abstract class PlayerWorker : MonoBehaviour
{
    [SerializeField] protected Truck Truck;
    [SerializeField] protected CharacterAnimation Animation;
    [SerializeField] protected PlayerStats Stats;

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    public abstract float GetWorkPower();
}
