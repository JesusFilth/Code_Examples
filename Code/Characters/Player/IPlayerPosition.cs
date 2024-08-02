using UnityEngine;

public interface IPlayerPosition
{
    void SetPosition(Transform point);

    Transform GetPosition();
}