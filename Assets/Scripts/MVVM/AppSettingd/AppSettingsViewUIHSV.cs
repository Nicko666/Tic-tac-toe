using UnityEngine;
using UnityEngine.UI;

public class AppSettingsViewUIHSV : MonoBehaviour
{
    [SerializeField] MaskableGraphic[] maskableGraphics;


    private void Start()
    {
        AppSettingsViewModel.uiColor.onValueChanged += OutputColor;

        OutputColor(AppSettingsViewModel.uiColor.Value);

    }

    void OutputColor(Color color)
    {
        foreach (var graphic in maskableGraphics)
        {
            graphic.color = color;
        }

    }

    private void OnDestroy()
    {
        AppSettingsViewModel.uiColor.onValueChanged -= OutputColor;

    }


}
