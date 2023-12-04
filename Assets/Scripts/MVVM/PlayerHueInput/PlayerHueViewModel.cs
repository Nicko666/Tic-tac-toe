using System;

public class PlayerHueViewModel
{
    PlayerHueModel playerHueModel;

    public ReactiveProperty<float> hue = new();

    public Action<float> onHueChanged;


    public PlayerHueViewModel(PlayerHueModel model)
    {        
        playerHueModel = model;
    }

    public void InputHue(float value)
    {
        onHueChanged(value);
    }
    public void OutputHue(float value)
    {
        hue.Value = value;
    }


}
