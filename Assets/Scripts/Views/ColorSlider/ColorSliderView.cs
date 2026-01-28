using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorSliderView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _image;
    [SerializeField] private RawImage _rawImage;

    public event UnityAction<float> onValueChanged
    {
        add => _slider.onValueChanged.AddListener(value);
        remove => _slider.onValueChanged.RemoveListener(value);
    }

    public void OutputTexture(Texture2D texture, Color color)
    {
        if (texture != null)
            _rawImage.texture = texture;
        
        _image.color = color;
    }

    public void SetValueWithoutNotify(float hue) =>
        _slider.SetValueWithoutNotify(hue);
}
