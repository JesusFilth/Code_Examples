using UnityEngine;

public struct MineralMovmentSettings
{
    public Transform StartPoint { get; set; }
    public Transform EndPoint { get; set; }
    public MineralType Type { get; set; }

    public IMineralCubeViewFinalPosition FinalAction { get; set; }
}