using System;

public interface IWorkerProcess
{
    event Action<int> ChangeCount;

    event Action<float> ChangeProgress;

    void ToWork();
}