using UnityEngine;

public class PlayerMiner : PlayerWorker
{
    private void OnEnable()
    {
        Stats.MaxMineralChanged += UpdateMaxMineralConteiner;
    }

    private void OnDisable()
    {
        Stats.MaxMineralChanged -= UpdateMaxMineralConteiner;
    }

    public bool CanExtract() => !Truck.IsFull;

    public override float GetWorkPower()
    {
        Animation.ToCraft();

        return Stats.MiningPower;
    }

    public IMineralCubeViewFinalPosition GetTruckEndPoint() => Truck;

    public Transform GetTrackPoint() => Truck.CubeEndPoint;

    public void AddMineral(MineralType type) => Truck.AddMineral(type);

    private void UpdateMaxMineralConteiner(int value) => Truck.SetMaxMineral(value);
}
