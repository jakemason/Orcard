using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [ReadOnly] public string InstructionText;
    public abstract void Activate();
}