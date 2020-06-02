using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Draw Until Effect", menuName = "Effects/Draw Until Effect")]
public class DrawUntilEffect : Effect
{
    public int CardsToDrawTo = 1;

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = CardsToDrawTo > 1
            ? $"Draw until you have {CardsToDrawTo} cards in hand."
            : $"Draw until you have {CardsToDrawTo} card in hand.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif

    public override void Activate()
    {
        int cardsInHand = PlayerHand.Instance.CardsInHand();
        int toDraw      = CardsToDrawTo - cardsInHand;
        //Debug.LogFormat("Cards in hand {0}, cards to draw {1}", cardsInHand, toDraw);
        if (toDraw > 0)
        {
            PlayerController.Instance.Draw(toDraw);
        }
    }

    public override void Deactivate()
    {
    }
}