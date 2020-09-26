using TMPro;
using UnityEngine;

public class PermitIndicator : MonoBehaviour
{
    public TextMeshProUGUI Text;

    //TODO: Don't do this in update, just a quick testing hack
    private void Update()
    {
        Text.text = BuildingManager.BuildingPermitsUsed + " / " + BuildingManager.BuildingPermitsAvailable;
    }
}