using System;
using UnityEngine;

[RequireComponent(typeof(MineralOre))]
public class MineralOreInitsializator : MonoBehaviour
{
    private MineralOre _ore;

    private void Awake()
    {
        _ore = GetComponent<MineralOre>();
    }

    public void Init(MineralType type, int sizeOre, float maxProgress, float forceResist, GameObject view)
    {
        Instantiate(view,transform);
        _ore.SetForceResistance(forceResist);
        _ore.SetMaxProgress(maxProgress);
        _ore.SetCount(sizeOre);
        _ore.SetType(type);
    }
}
