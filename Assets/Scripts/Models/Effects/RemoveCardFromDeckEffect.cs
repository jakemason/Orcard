using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Remove Card From Deck Effect", menuName = "Effects/Add Remove Card From Deck Effect")]
public class RemoveCardFromDeckEffect : Effect
{
    public override void Activate()
    {
        int cardsInDeck = Player.Instance.DeckForCurrentRun.Cards.Count;
        if (cardsInDeck == 0) return;

        int index = Random.Range(0, cardsInDeck);
        Player.Instance.DeckForCurrentRun.Cards.RemoveAt(index);
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = "Remove a random card from your deck.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}