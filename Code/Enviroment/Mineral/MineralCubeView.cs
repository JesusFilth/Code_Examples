using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MineralCubeView : MonoBehaviour
{
    [SerializeField] private float _speedMove = 5f;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private List<MineralMaterial> _mineralMaterials;

    public MineralType Type => _mineralSettings.Type;

    private Rigidbody _rigidbody;
    private Transform _transform;

    private Coroutine _moving;

    private MineralMovmentSettings _mineralSettings;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        if(_moving != null)
        {
            StopCoroutine(_moving);
            _moving = null;
        }
    }

    public void Init(MineralMovmentSettings mineralSettings)
    {
        _mineralSettings = mineralSettings;
        _rigidbody.isKinematic = true;
;
        _transform.position = _mineralSettings.StartPoint.position;

        ChangeMaterial();
        ToMove();
    }

    public void DisableKinematic()
    {
        _rigidbody.isKinematic = false;
    }

    private void ToMove()
    {
        if (_moving == null)
            _moving = StartCoroutine(Moving());
    }

    private void ChangeMaterial()
    {
        if (_renderer == null)
            return;

        Material material = GetMaterialByType();

        if (material != null)
            _renderer.material = material;
    }

    private Material GetMaterialByType()
    {
        if (_mineralMaterials == null)
            throw new ArgumentNullException(nameof(_mineralMaterials));

        MineralMaterial mineralMaterial = _mineralMaterials.Find(mineral=> mineral.Type == _mineralSettings.Type);
        return mineralMaterial.Material;
    }

    private IEnumerator Moving()
    {
        while (_transform.position != _mineralSettings.EndPoint.position)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _mineralSettings.EndPoint.position, _speedMove * Time.deltaTime);

            yield return null;
        }

        _mineralSettings.FinalAction.RemoveCube(this);

        _moving = null;
    }
}
