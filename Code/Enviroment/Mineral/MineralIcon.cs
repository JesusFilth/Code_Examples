using UnityEngine;

[CreateAssetMenu(fileName = "MineralIcon", menuName = "World of Cubes/MineralIcon", order = 2)]
public class MineralIcon : ScriptableObject
{
    [SerializeField] private MineralType _type;
    [SerializeField] private Sprite _icon;

    public MineralType Type => _type;
    public Sprite Icon => _icon;
}
