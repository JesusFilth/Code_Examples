using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStats))]
public class EnemyComponentHide : MonoBehaviour
{
    [SerializeField] private GameObject[] _hideObjects;
    [SerializeField] private Component[] _hideComponents;

    private EnemyStats _stats;

    private void Awake()
    {
        _stats = GetComponent<EnemyStats>();
    }

    public void On()
    {
        if (_hideObjects != null && _hideObjects.Length != 0)
            foreach (var obj in _hideObjects)
                obj.SetActive(true);

        if (_hideComponents != null && _hideComponents.Length != 0)
            foreach (Component component in _hideComponents)
            {
                if (component is Behaviour behaviour)
                    behaviour.enabled = true;

                if (component is Collider collider)
                    collider.enabled = true;
            }

        _stats.ResetToDefault();
    }

    public void Off()
    {
        if (_hideObjects != null && _hideObjects.Length != 0)
            foreach (var obj in _hideObjects)
                obj.SetActive(false);

        if (_hideComponents != null && _hideComponents.Length != 0)
            foreach (Component component in _hideComponents)
            {
                if (component is Behaviour behaviour)
                    behaviour.enabled = false;

                if(component is Collider collider)
                    collider.enabled = false;
            }   
    }
}
