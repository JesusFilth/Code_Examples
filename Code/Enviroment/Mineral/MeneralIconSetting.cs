using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MeneralIconSetting", menuName = "World of Cubes/MeneralIconSetting", order = 2)]
public class MeneralIconSetting : ScriptableObject
{
    [SerializeField] private MineralIcon[] _mineralIcons;

    public MineralIcon[] MineralIcons => _mineralIcons;

    private void OnValidate()
    {
        if (_mineralIcons == null)
            throw new ArgumentNullException(nameof(_mineralIcons));
    }

    public Sprite GetIcon(MineralType type)
    {
        MineralIcon mineralIcon = _mineralIcons.FirstOrDefault(icon => icon.Type == type);

        if (mineralIcon == null)
            throw new ArgumentNullException(nameof(mineralIcon));

        return mineralIcon.Icon;
    }
}
