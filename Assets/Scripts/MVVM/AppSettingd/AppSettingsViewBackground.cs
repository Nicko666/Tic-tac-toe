using UnityEngine;
using UnityEngine.UI;

public class AppSettingsViewBackground : MonoBehaviour
{
    [SerializeField] Image image;


    private void Start()
    {
        AppSettingsViewModel.backgroundColor.onValueChanged += OutputColor;
        
        OutputColor(AppSettingsViewModel.backgroundColor.Value);
    
    }

    void OutputColor(Color color)
    {
        image.color = color;
    
    }

    private void OnDestroy()
    {
        AppSettingsViewModel.backgroundColor.onValueChanged -= OutputColor;
    
    }


}
