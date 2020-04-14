using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Timed Effect", menuName = "Effects/Timed Effect")]
public class TimedEffect : Effect
{
    public float Delay = 20f;
    public float DelayCoolddown;
    public bool IsPermanent;
    public List<Effect> Effects;

#if UNITY_EDITOR
    public void OnValidate()
    {
        DelayCoolddown  = Delay;
        InstructionText = $"After {Delay} seconds, ";
        foreach (Effect effect in Effects)
        {
            InstructionText += effect.InstructionText;
        }

        if (InstructionText[InstructionText.Length - 1] != '.')
        {
            InstructionText += ".";
        }

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif

    public override void Activate()
    {
        TurnManager.TimedEffects.Add(this);
    }
}