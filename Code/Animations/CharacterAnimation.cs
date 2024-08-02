using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ToJump()
    {
        _animator.SetTrigger(AnimationsData.Character.Jump);
    }

    public void ToWalk(bool isWalk)
    {
        _animator.SetBool(AnimationsData.Character.IsWalk, isWalk);
    }

    public void ToCraft()
    {
        _animator.SetTrigger(AnimationsData.Character.Craft);
    }

    public void ToAttack()
    {
        _animator.SetTrigger(AnimationsData.Character.Attack);
    }

    public void ToDie()
    {
        _animator.SetTrigger(AnimationsData.Character.Die);
    }

    public void ToIdel()
    {
        _animator.SetTrigger(AnimationsData.Character.Idel);
    }
}
