public abstract class SkillStratigy
{
    public float Value { get; private set; }

    public virtual void SetValue(float value)
    {
        Value = value;
    }

    public abstract void AddProperty(PlayerStats stat);
}