using UnityEngine;

public class KeyboardInput : IPlayerInput
{
    private const string Vertical = "Vertical";
    private const string Horizontal = "Horizontal";
    private const KeyCode _jump = KeyCode.Space;
    private const KeyCode _attack = KeyCode.Mouse1;

    public Vector3 GetDirection()
    {
        float horizontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);

        return new Vector3(horizontal, 0f, vertical);
    }

    public bool IsAttack()
    {
        return Input.GetKey(_attack);
    }

    public bool IsJump()
    {
        return Input.GetKey(_jump);
    }
}
