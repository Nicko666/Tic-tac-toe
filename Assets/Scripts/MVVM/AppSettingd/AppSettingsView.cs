using UnityEngine;
using UnityEngine.UI;

public class AppSettingsView : MonoBehaviour
{
    AppSettingsViewModel _viewModel;

    [SerializeField] Slider _slider;


    private void Awake()
    {
        _slider.onValueChanged.AddListener(InputVolume);
    }

    public void Init(AppSettingsViewModel viewModel)
    {
        if (_viewModel != null)
            ViewUnsubscribe();

        _viewModel = viewModel;

        if (_viewModel != null)
            ViewSubscribe();

        ViewUpdate();

    }

    void ViewSubscribe()
    {
        AppSettingsViewModel.volume.onValueChanged += OutputVolume;
    }
    void ViewUnsubscribe()
    {
        AppSettingsViewModel.volume.onValueChanged -= OutputVolume;
    }
    void ViewUpdate()
    {
        OutputVolume(AppSettingsViewModel.volume.Value);
    }

    void OutputVolume(float value)
    {
        _slider.value = value;

    }
    void InputVolume(float value)
    {
        _viewModel.InputVolume(value);

    }


}
