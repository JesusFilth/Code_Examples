using System;
using UnityEngine.Scripting;

[Serializable]
public class UserStatsModel
{
    [field: Preserve] public float Health;
    [field: Preserve] public float Damage;
    [field: Preserve] public float BuildSpeed;
    [field: Preserve] public float CraftSpeed;
    [field: Preserve] public float MaxMineralConteiner;
}
