using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlooLavaUI : MonoBehaviour, IGameLevelView
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.enabled = false;
    }

    public void Show()
    {
        _image.enabled = true;
    }

    public void Hide()
    {
        _image.enabled = false;
    }
}
