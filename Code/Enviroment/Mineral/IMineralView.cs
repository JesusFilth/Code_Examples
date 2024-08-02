using System;
using System.Collections.Generic;

public interface IMineralView
{
    event Action<IReadOnlyDictionary<MineralType, int>> BagChanged;
}
