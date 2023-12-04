using System;

public class PlayerNameViewModel
{
    PlayerNameModel playerNameModel;

    public ReactiveProperty<string> playerName = new();

    public Action<string> onNameChanged;


    public PlayerNameViewModel(PlayerNameModel playerNameModel)
    {
        this.playerNameModel = playerNameModel;

        ViewModelUpdate();

    }

    public void ViewModelUpdate()
    {
        playerName.Value = default(string);

    }

    public void InputName(string value)
    {
        onNameChanged?.Invoke(value);

    }

    public void OutputName(string value)
    {
        playerName.Value = value;

    }


}
