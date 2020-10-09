using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayOneShotSound.Play(SoundLibrary.Global.ButtonHover);
    }
}