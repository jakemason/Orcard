using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public abstract void Enable(Vector2 position);
    public abstract void Disable(Vector2 position);
}