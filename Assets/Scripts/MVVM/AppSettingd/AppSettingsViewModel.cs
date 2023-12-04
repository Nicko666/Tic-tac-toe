using UnityEngine;
using UnityEngine.Localization.Settings;

public class AppSettingsViewModel
{
    public AppSettingsModel model;

    public static ReactiveProperty<Color> backgroundColor = new();
    public static ReactiveProperty<Color> playerColor = new();
    public static ReactiveProperty<Color> uiColor = new();

    public static ReactiveProperty<float> volume = new();

    public static Locals locals;


    public AppSettingsViewModel(AppSettingsModel model)
    {
        ViewModelUnsubscribe();

        this.model = model;

        ViewModelSubscribe();

        ViewModelUpdate();

    }
    void ViewModelSubscribe()
    {
        if (model != null)
        {
            model.locals.onValueChanged += OutputLocal;
            model.theme.onValueChanged += OutputTheme;
            model.volume.onValueChanged += OutputVolume;
        }

    }
    void ViewModelUnsubscribe()
    {
        if (model != null)
        {
            model.locals.onValueChanged -= OutputLocal;
            model.theme.onValueChanged -= OutputTheme;
            model.volume.onValueChanged -= OutputVolume;
        }
    }
    void ViewModelUpdate()
    {
        if (model != null)
        {
            OutputLocal(model.locals.Value);
            OutputTheme(model.theme.Value);
            OutputVolume(model.volume.Value);
        }

    }

    public void InputLocalisation(Locals locals)
    {
        model.locals.Value = locals;
    }
    public void IntputTheme(Theme value)
    {
        model.theme.Value = value;

    }
    public void InputVolume(float value)
    {
        model.volume.Value = value;
    }

    public void OutputLocal(Locals value)
    {
        locals = value; 
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(value.Code);
    }
    void OutputTheme(Theme value)
    {
        backgroundColor.Value = value.backgroundColor;
        playerColor.Value = value.playerFontColor;
        uiColor.Value = value.uiFontColor;
    }
    void OutputVolume(float value)
    {
        volume.Value = value;
    }



}
