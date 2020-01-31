using UnityEngine;

public abstract class Conditional : ScriptableObject
{
    [ReadOnly] public string InstructionText;
    public abstract bool ConditionMet();
}