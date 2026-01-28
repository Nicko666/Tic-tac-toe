using System;
using UnityEngine;
using UnityEngine.Events;

internal class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PanelView _panel;
    [SerializeField] private TogglesImageView _marksToggles, _logicsToggles;
    [SerializeField] private ColorSliderView _hueSlider, _saturationSlider;

    private PlayerSettingsModel _playerSettings = new();
    private MarkModel[] _marks = new MarkModel[0];
    private LogicModel[] _logics = new LogicModel[0];

    internal UnityAction<PlayerSettingsModel> onInputPlayerSettings;
    internal event UnityAction onInputClosePanel
    {
        add => _panel.onInputClose += value;
        remove => _panel.onInputClose -= value;
    }

    private void Awake()
    {
        _hueSlider.onValueChanged += InputHue;
        _saturationSlider.onValueChanged += InputSaturation;
        _marksToggles.onInput += InputMark;
        _logicsToggles.onInput += InputLogic;

        _hueSlider.OutputTexture(GetHueTexture(), Color.HSVToRGB(_playerSettings.hue, 1, 1));
        _saturationSlider.OutputTexture(GetSaturationTexture(), Color.HSVToRGB(_playerSettings.hue, _playerSettings.saturation, 1));
        Texture2D GetHueTexture()
        {
            Texture2D texture = new Texture2D(256, 1);
            for (int x = 0; x < texture.width; x++)
                texture.SetPixel(x, 0, Color.HSVToRGB(((float)x) / texture.width, 1, 1));
            texture.Apply();
            return texture;
        }
        Texture2D GetSaturationTexture()
        {
            Texture2D texture = new Texture2D(256, 1);
            for (int x = 0; x < texture.width; x++)
                texture.SetPixel(x, 0, Color.HSVToRGB(_playerSettings.hue, ((float)x) / texture.width, 1));
            texture.Apply();
            return texture;
        }
    }

    internal void OutputPanel(bool value) =>
        _panel.OutputOpen(value);

    internal void OutputPlayer(PlayerModel player)
    {
        if (player == null) return;

        _playerSettings.mark = player.mark;
        _marksToggles.OutputIsToggled(Array.IndexOf(_marks, _playerSettings.mark));
        
        _playerSettings.logic = player.logic;
        _logicsToggles.OutputIsToggled(Array.IndexOf(_logics, _playerSettings.logic));

        _playerSettings.hue = player.hue;
        _hueSlider.SetValueWithoutNotify(_playerSettings.hue);
        _hueSlider.OutputTexture(null, Color.HSVToRGB(_playerSettings.hue, 1, 1));

        _playerSettings.saturation = player.saturation;
        _saturationSlider.SetValueWithoutNotify(_playerSettings.saturation);
        _saturationSlider.OutputTexture(GetSaturationTexture(), Color.HSVToRGB(_playerSettings.hue, _playerSettings.saturation, 1));

        Texture2D GetSaturationTexture()
        {
            Texture2D texture = new Texture2D(256, 1);
            for (int x = 0; x < texture.width; x++)
                texture.SetPixel(x, 0, Color.HSVToRGB(_playerSettings.hue, ((float)x) / texture.width, 1));
            texture.Apply();
            return texture;
        }
    }

    internal void OutputDatabase(MarkModel[] marks, LogicModel[] logics)
    {
        _marks = marks;
        _marksToggles.OutputToggles(Array.ConvertAll(_marks, mark => mark.Icon));

        _logics = logics;
        _logicsToggles.OutputToggles(Array.ConvertAll(_logics, logic => logic.Icon));
    }

    private void InputMark(int index)
    {
        _playerSettings.mark = _marks[index];
        onInputPlayerSettings.Invoke(_playerSettings);
    }

    private void InputLogic(int index)
    {
        _playerSettings.logic = _logics[index];
        onInputPlayerSettings.Invoke(_playerSettings);
    }

    private void InputHue(float hue)
    {
        _playerSettings.hue = hue;
        onInputPlayerSettings.Invoke(_playerSettings);
    }

    private void InputSaturation(float saturation)
    {
        _playerSettings.saturation = saturation;
        onInputPlayerSettings.Invoke(_playerSettings);
    }
}