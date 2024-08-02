using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private List<EnemyTransition> _transitions;

    public void Enter()
    {
        if(enabled == false)
        {
            enabled = true;

            foreach(EnemyTransition transition in _transitions)
            {
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach(EnemyTransition transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public EnemyState GetNextState()
    {
        if(_transitions == null)
            return null;

        foreach (EnemyTransition transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }
}
