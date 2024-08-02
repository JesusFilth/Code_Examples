using System;
using UnityEngine.Scripting;

[Serializable]
public class LevelModel
{
    [field: Preserve] public string Name;
    [field: Preserve] public int Id;
    [field: Preserve] public int Stars;
    [field: Preserve] public int NeedStarForOpen;
    [field: Preserve] public bool IsOpen;
    [field: Preserve] public LevelTypeMode OpenMode;
}
