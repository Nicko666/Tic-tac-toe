using UnityEngine;
using UnityEngine.UI;

public class AppSettingsViewPlayerSV : MonoBehaviour
{
    [SerializeField] MaskableGraphic[] maskableGraphics;


    private void Start()
    {
        AppSettingsViewModel.playerColor.onValueChanged += OutputColor;

        OutputColor(AppSettingsViewModel.playerColor.Value);

    }

    void OutputColor(Color color)
    {
        foreach (var graphic in maskableGraphics)
        {
            ColorLibrary.ReadSV(graphic.color, color);
        }

    }

    private void OnDestroy()
    {
        AppSettingsViewModel.playerColor.onValueChanged -= OutputColor;

    }


}
