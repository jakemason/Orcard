using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Card To Deck Effect", menuName = "Effects/Add Card To Deck Effect")]
public class AddCardToDeckEffect : Effect
{
    public Card.CardRarity RarityToAdd;

    public override void Activate()
    {
        Card toAdd = CardList.GetRandomCardOfRarity(RarityToAdd);
        PlayerController.Instance.DeckForCurrentRun.Cards.Add(toAdd);
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = "Add a " + RarityToAdd + " card to your deck.";

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}