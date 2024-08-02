using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class MineralSize
{
    public MineralSizeType Type;
    [Range(1, 100)] public int MinCount;
    [Range(2, 100)] public int MaxCount;
}