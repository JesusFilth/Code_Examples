using UnityEngine;
using System;
using UnityEngine.UI;

public class GameModeButton : MonoBehaviour
{
    [SerializeField] private LevelTypeMode _type;
    [SerializeField] private GameObject _complete;
    [SerializeField] private GameObject _number;
    [SerializeField] private Color32 _focusedColor = Color.red;
    [SerializeField] private Color32 _noFocusedColor = Color.white;
    [SerializeField] private Button _click;
    [SerializeField] private Image _image;

    public event Action<LevelTypeMode> Changed;
    public LevelTypeMode Type => _type;

    private void OnValidate()
    {
        if (_complete == null)
            throw new ArgumentNullException(nameof(_complete));

        if (_number == null)
            throw new ArgumentNullException(nameof(_number));

        if (_click == null)
            throw new ArgumentNullException(nameof(_click));

        if (_image == null)
            throw new ArgumentNullException(nameof(_image));
    }

    private void OnEnable()
    {     
        _click.onClick.AddListener(ChangeMode);
    }

    private void OnDisable()
    {
        _click.onClick.RemoveListener(ChangeMode);
    }

    public void OnFocused(bool isFocus)
    {
        _image.color = isFocus ? _focusedColor : _noFocusedColor;
    }

    public void OnCompleted(bool isCompleted)
    {
        _complete.SetActive(isCompleted);
        _number.SetActive(!isCompleted);
    }

    public void SetInteracteble(bool isOpen)
    {
        _click.interactable = isOpen;
    }

    private void ChangeMode()
    {
        Changed.Invoke(Type);
    }
}
