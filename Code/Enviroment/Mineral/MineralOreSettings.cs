using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MineralOreSettings", menuName = "World of Cubes/MineralOreSettings", order = 2)]
public class MineralOreSettings : ScriptableObject
{
    const int MineralSizeLenght = 3;

    [SerializeField] private MineralOreInitsializator _mineralOre;
    [SerializeField] private MineralOreSetting[] _mineralOreSettings;
    [SerializeField] private MineralSize[] mineralSizes = new MineralSize[MineralSizeLenght];

    public MineralOreInitsializator MineralOre => _mineralOre;

    private void OnValidate()
    {
        if (_mineralOre == null)
            throw new ArgumentNullException(nameof(_mineralOre));

        for (int i = 0; i < mineralSizes.Length; i++)
            mineralSizes[i].Type = (MineralSizeType)i;

        for (int i = 0; i < mineralSizes.Length; i++)
        {
            if (mineralSizes[i].MinCount> mineralSizes[i].MaxCount)
                mineralSizes[i].MinCount = mineralSizes[i].MaxCount-1;
        }
    }

    public MineralSizeType GetBetweenSize(int count)
    {
        if (count >= mineralSizes[0].MinCount && count <= mineralSizes[0].MaxCount)
            return MineralSizeType.Small;

        if (count >= mineralSizes[1].MinCount && count <= mineralSizes[1].MaxCount)
            return MineralSizeType.Medium;

        if (count >= mineralSizes[2].MinCount && count <= mineralSizes[2].MaxCount)
            return MineralSizeType.Big;

        return MineralSizeType.Big;
    }

    public int GetRandomCount(MineralSizeType typeSize)
    {
        MineralSize mineral = mineralSizes.FirstOrDefault(type=>type.Type == typeSize);

        if (mineral == null)
            throw new ArgumentNullException(nameof(mineral));

        return UnityEngine.Random.Range(mineral.MinCount, mineral.MaxCount+1);
    }

    public GameObject GetModelView(MineralType mineralType, MineralSizeType sizeType)
    {
        GameObject modelView = _mineralOreSettings.FirstOrDefault(ore => ore.Type == mineralType && ore.SizeType == sizeType).Model;

        if (modelView == null)
            throw new ArgumentNullException(nameof(modelView));

        return modelView;
    }
}
