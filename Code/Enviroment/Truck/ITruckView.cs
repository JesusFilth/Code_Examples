using System;

public interface ITruckView
{
    event Action<int,int> ValueChanged;
}
