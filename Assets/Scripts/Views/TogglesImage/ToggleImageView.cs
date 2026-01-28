using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

internal class ToggleImageView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Animator _animator;
    private bool _isToggled;

    private static readonly int _animatorToggledBool = Animator.StringToHash("Toggled");

    public UnityAction<ToggleImageView> onClick;

    public void OutputSprite(Sprite sprite) =>
        _button.image.sprite = sprite;

    public void OutputIsToggled(bool value)
    {
        _isToggled = value;
        _animator.SetBool(_animatorToggledBool, _isToggled);
    }

    private void Awake() =>
        _button.onClick.AddListener(Input);
    private void OnDestroy() =>
        _button.onClick.RemoveListener(Input);

    private void OnEnable() =>
        _animator.SetBool(_animatorToggledBool, _isToggled);
    private void OnDisable() =>
        _animator.SetBool(_animatorToggledBool, false);

    private void Input() =>
        onClick.Invoke(this);
}