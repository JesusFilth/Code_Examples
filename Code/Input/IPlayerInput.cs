using System;
using UnityEngine;

public interface IPlayerInput
{
    Vector3 GetDirection();

    bool IsJump();

    bool IsAttack();
}
