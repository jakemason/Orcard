using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Card To Deck Effect", menuName = "Effects/Add Card To Deck Effect")]
public class AddCardToDeckEffect : Effect
{
    public Card.CardRarity RarityToAdd;
    public bool RandomRarity;

    public override void Activate()
    {
        Card toAdd = RandomRarity ? CardList.GetRandomCard() : CardList.GetRandomCardOfRarity(RarityToAdd);
        PlayerController.Instance.DeckForCurrentRun.Cards.Add(toAdd);
    }

    public override void Deactivate()
    {
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText =
            RandomRarity ? "Add a random card to your deck." : "Add a " + RarityToAdd + " card to your deck.";

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}