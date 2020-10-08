using UnityEngine;

/// <summary>
/// Making this a ScriptableObject is clunky, but it allows us to easily set
/// these through the inspector interface and allows us to mix and match them
/// with other CastingRequirements.
/// </summary>
public abstract class Requirement : ScriptableObject
{
    public abstract bool RequirementMet();
    public string RequirementFailMessage;
}