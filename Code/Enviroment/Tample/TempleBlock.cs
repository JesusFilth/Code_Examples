using Reflex.Attributes;
using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TempleBlock : MonoBehaviour, IMineralCubeViewFinalPosition
{
    [SerializeField] private MineralType _type;
    [SerializeField] private Material _empty;

    public MineralType Type => _type;

    private Material _origin;
    private Renderer _renderer;

    [Inject] private ITempleBuildSounds _sounds;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
    }

    private void OnEnable()
    {
        SetEmptyMaterial();
    }

    private void OnValidate()
    {
        if (_empty == null)
            throw new ArgumentNullException(nameof(_empty));
    }

    public void RemoveCube(MineralCubeView mineralCube)
    {
        _sounds.ToCompetedBlockBuild();

        MineralPool.Instance.Release(mineralCube);
        Open();
    }

    private void Open()
    {
        SetOriginMaterial();
    }

    private void SetEmptyMaterial()
    {
        _origin = _renderer.material;
        _renderer.material = _empty;
    }

    private void SetOriginMaterial()
    {
        _renderer.material = _origin;
    }
}
