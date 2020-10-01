using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Card To Deck Effect", menuName = "Effects/Add Card To Deck Effect")]
public class AddCardToDeckEffect : Effect
{
    public Card.CardRarity RarityToAdd;
    public Card CardToAdd;
    public int NumberOfCopiesToAdd = 1;
    public bool RandomRarity;

    public override void Activate()
    {
        if (CardToAdd)
        {
            for (int i = 0; i < NumberOfCopiesToAdd; i++)
            {
                PlayerController.Instance.DeckForCurrentRun.Cards.Add(CardToAdd);
            }
        }
        else
        {
            Card toAdd = RandomRarity ? CardList.GetRandomCard() : CardList.GetRandomCardOfRarity(RarityToAdd);
            for (int i = 0; i < NumberOfCopiesToAdd; i++)
            {
                PlayerController.Instance.DeckForCurrentRun.Cards.Add(toAdd);
            }
        }
    }

    public override void Deactivate()
    {
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText =
            RandomRarity ? "Add a random card to your deck." : $"Add a {RarityToAdd} card to your deck.";

        if (CardToAdd != null)
        {
            InstructionText = NumberOfCopiesToAdd == 1
                ? $"Add a copy of {CardToAdd.Name} to your deck."
                : $"Add {NumberOfCopiesToAdd} copies of {CardToAdd.Name} to your deck.";
        }


        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}