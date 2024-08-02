using System;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    const int MaxLife = 5;

    [SerializeField] private float _damage;
    [SerializeField] private float _maxHealth;
    [SerializeField] private int _life;

    public UnityEvent OnTakeDamage;
    public UnityEvent OnDie;

    public float Damage => _damage;
    public float MaxHealth => _maxHealth;
    public int Life => _life;
    public bool IsDead() => _life == 0;
    public event Action Died;

    public event Action<float> HealthChanged;
    public event Action<int> LifeChanged;

    protected float CurrentHealth;

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch(ArgumentNullException ex)
        {
            enabled = false;
            throw ex;
        }

        HealthChanged?.Invoke(GetNormalizePercentCurrentHealth());
        LifeChanged?.Invoke(_life);
    }

    public void TakeDamage(float damage)
    {
        if (IsDead())
            return;

        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, _maxHealth);

        PlayHitSound();
        
        if (CurrentHealth == 0)
        {
            CurrentHealth = _maxHealth;
            _life--;
            LifeChanged?.Invoke(_life);
        }

        HealthChanged?.Invoke(GetNormalizePercentCurrentHealth());

        if (IsDead())
            Die();
    }

    public void AddLife(int count)
    {
        _life = Mathf.Clamp(_life + count, 0 ,MaxLife);
        LifeChanged?.Invoke(_life);

        if(IsDead())
            Die();
    }

    public void AddHealth(float health)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + health, 0, _maxHealth);
        HealthChanged?.Invoke(GetNormalizePercentCurrentHealth());
    }

    public void AddDamage(float damage)
    {
        _damage = Mathf.Clamp(_damage+=damage, 0, int.MaxValue);
    }

    protected void UpadateViewChanges()
    {
        HealthChanged?.Invoke(GetNormalizePercentCurrentHealth());
        LifeChanged?.Invoke(_life);
    }

    protected void SetDamage(float damage)
    {
        _damage = damage;
    }

    protected void SetMaxHealth(float health)
    {
        _maxHealth = health;
    }

    protected void SetLife(int life)
    {
        _life = life;
    }

    private void Die()
    {
        OnDie?.Invoke();
        Died?.Invoke();
    }

    private void PlayHitSound()
    {
        OnTakeDamage?.Invoke();
    }

    private void Validate()
    {

    }

    private float GetNormalizePercentCurrentHealth()
    {
        const float MaxPercent = 100;

        return (MaxPercent / (_maxHealth / CurrentHealth)) / MaxPercent;
    }
}
