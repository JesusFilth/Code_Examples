using Agava.WebUtility;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Button _attack;

    private bool _isJump;
    private bool _isAttack;

    private void OnEnable()
    {
        if (Device.IsMobile == false)
        {
            gameObject.SetActive(false);
            return;
        }

        _attack.onClick.AddListener(OnClickAttack);
    }

    private void OnDisable()
    {
        _attack.onClick.RemoveListener(OnClickAttack);
    }

    private void OnValidate()
    {
        if (_joystick == null)
            throw new ArgumentNullException(nameof(_joystick));

        if (_attack == null)
            throw new ArgumentNullException(nameof(_attack));
    }

    public void OnPointerDownJump()
    {
        _isJump = true;
    }

    public Vector3 GetDirection()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;

        return new Vector3(horizontal, 0f, vertical);
    }

    public bool IsJump()
    {
        if (_isJump)
        {
            _isJump = false;
            return true;
        }

       return false;
    }

    public bool IsAttack()
    {
        if (_isAttack)
        {
            _isAttack = false;
            return true;
        }

        return false;
    }

    private void OnClickAttack()
    {
        _isAttack = true;
    }
}
