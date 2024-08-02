using Reflex.Attributes;
using System;
using System.Collections;
using UnityEngine;

public abstract class WorkProcess<TWork> : MonoBehaviour, IWorkerProcess where TWork : PlayerWorker
{

    [SerializeField] private float _speedTimeResistance = 0.1f;
    [SerializeField] private float _forceResistance = 3;
    [SerializeField] private float _maxProgress = 100;

    public event Action<int> ChangeCount;
    public event Action<float> ChangeProgress;

    protected Transform Transform;
    protected TWork Player;

    private Coroutine _progressMining;
    private WaitForSeconds _waitForSeconds;
    private float _currentProgress;
    private int _currentCount;

    [Inject] private IWorkerView _workerView;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_speedTimeResistance);
        Transform = transform;

        GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out TWork player))
        {
            Player = player;

            _workerView.Binding(this);
            ChangeCount?.Invoke(_currentCount);
            ChangeProgress?.Invoke(GetNormalazeProgressPercent());

            if (_progressMining == null)
                _progressMining = StartCoroutine(ProgressMining());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Player = null;
            _workerView.Unbinding();
        }
    }

    private void OnDisable()
    {
        if (_progressMining != null)
        {
            StopCoroutine(_progressMining);
            _progressMining = null;
        }
    }

    private void OnDestroy()
    {
        if(_workerView!=null)
            _workerView.Unbinding();
    }

    public void SetSpeedTimeResistance(float speedTime)
    {
        _speedTimeResistance = speedTime;
    }

    public void SetForceResistance(float force)
    {
        _forceResistance = force;
    }

    public void SetMaxProgress(float progress)
    {
        _maxProgress = progress;
    }

    public abstract void ToWork();

    protected abstract void Complete();

    protected void CheckComplete()
    {
        if (_currentProgress >= _maxProgress)
        {
            _currentProgress = _currentProgress % _maxProgress;

            Complete();
            ChangeCount?.Invoke(_currentCount);
            ChangeProgress?.Invoke(GetNormalazeProgressPercent());
        }
    }

    protected void SetCurrentCount(int count)
    {
        _currentCount = count;
        ChangeCount?.Invoke(_currentCount);
    }

    protected void AddProgress(float value)
    {
        _currentProgress += value;
    }

    private IEnumerator ProgressMining()
    {
        while (enabled || _currentProgress != 0)
        {
            _currentProgress = Mathf.Clamp(_currentProgress -= _forceResistance, 0f, _maxProgress);
            ChangeProgress?.Invoke(GetNormalazeProgressPercent());

            yield return _waitForSeconds;
        }

        _progressMining = null;
    }

    private float GetNormalazeProgressPercent()
    {
        const float MaxPercent = 100;

        return (MaxPercent / (_maxProgress / _currentProgress)) / MaxPercent;
    }
}
