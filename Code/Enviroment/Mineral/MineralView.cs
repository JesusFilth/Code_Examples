using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MineralView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _count;

    public MineralType MineralType { get; private set; }

    public void Init(Sprite icon, MineralType type)
    {
        _icon.sprite = icon;
        MineralType = type;
    }

    public void SetCount(int count)
    {
        _count.text = count.ToString();
    }
}
