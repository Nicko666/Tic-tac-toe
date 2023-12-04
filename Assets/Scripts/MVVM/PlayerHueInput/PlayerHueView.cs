using UnityEngine;
using UnityEngine.UI;

public class PlayerHueView : MonoBehaviour
{
    PlayerHueViewModel viewModel;

    [SerializeField] MaskableGraphic[] _maskableGraphic;
    [SerializeField] Slider _slider;


    public void Init(PlayerHueViewModel viewModel)
    {
        if (this.viewModel != null)
            ViewUnsubscribe();

        this.viewModel = viewModel;

        if (this.viewModel != null)
            ViewSubscribe();

        ViewUpdate();

    }

    void ViewSubscribe()
    {
        viewModel.hue.onValueChanged += OutputHue;
    }
    void ViewUnsubscribe()
    {
        viewModel.hue.onValueChanged -= OutputHue;
    }
    void ViewUpdate()
    {
        OutputHue(viewModel != null ? viewModel.hue.Value : default(float));
    }

    private void Start()
    {
        _slider.onValueChanged.AddListener(InputHue);
    }

    void InputHue(float value)
    {
        viewModel.InputHue(value);
        OutputHue(value);
    }
    void OutputHue(float value)
    {
        _slider.SetValueWithoutNotify(value);
        foreach(var item in _maskableGraphic)
            item.color = ColorLibrary.ReadH(value, item.color);

    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(InputHue);
    }


}
