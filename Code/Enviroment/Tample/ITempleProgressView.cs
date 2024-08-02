using System;

public interface ITempleProgressView
{
    void ChangeBuildProgressBar(float percent, int blockCount);

    void ChangeCurrentMineral(MineralType type);

    void ChangeNextMineral(bool hasMineral, MineralType type);
}
