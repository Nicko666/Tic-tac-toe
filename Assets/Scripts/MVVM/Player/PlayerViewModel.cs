using System;

public class PlayerViewModel
{
    PlayerModel _model;
    //public PlayerModel Model => _model;
    public PlayerModel Model { get => _model;
        set {
            if (_model != null)
                ViewUnsubscribe();

            _model = value;

            if (_model != null)
                ViewSubscribe();

            UpdateViewModel();
        }
    }


    public ReactiveProperty<float> Hue = new();
    public ReactiveProperty<PlayerMark> Mark = new();
    public ReactiveProperty<string> Name = new();
    public ReactiveProperty<PlayerBehaviour> Behaviour = new();
    public ReactiveProperty<int> Points = new();

    public Action<PlayerViewModel> onSelected;

    public Action onAddPoint;
    public Action onShow;


    public PlayerViewModel(PlayerModel model) 
    {
        Model = model;
    }

    void ViewSubscribe()
    {
        _model.Hue.onValueChanged += OutputHue;
        _model.Mark.onValueChanged += OutputMark;
        _model.Name.onValueChanged += OutputName;
        _model.Behaviour.onValueChanged += OutputBehaviour;
        _model.Points.onValueChanged += OutputPoints;
    }
    void ViewUnsubscribe()
    {
        _model.Hue.onValueChanged += OutputHue;
        _model.Mark.onValueChanged += OutputMark;
        _model.Name.onValueChanged += OutputName;
        _model.Behaviour.onValueChanged += OutputBehaviour;
        _model.Points.onValueChanged += OutputPoints;
    }
    public void UpdateViewModel()
    {
        if (_model != null)
        {
            OutputHue(_model.Hue.Value);
            OutputMark(_model.Mark.Value);
            OutputName(_model.Name.Value);
            OutputBehaviour(_model.Behaviour.Value);
            OutputPoints(_model.Points.Value);
        }
        else
        {
            OutputHue(0.0f);
            OutputMark(null);
            OutputName("");
            OutputBehaviour(default);
            OutputPoints(0);
        }
        
    }

    void OutputHue(float value) => Hue.Value = value;
    void OutputMark(PlayerMark value) => Mark.Value = value;
    void OutputName(string value) => Name.Value = value;
    void OutputBehaviour(PlayerBehaviour value) => Behaviour.Value = value;
    void OutputPoints(int value)
    {
        Points.Value = value;
        onAddPoint?.Invoke();
    }
    //public void OutputSelect()
    //{
        
    //}

    public void InputHue(float value) => Model.Hue.Value = value;
    public void InputMark(PlayerMark value) => Model.Mark.Value = value;
    public void InputName(string value) => Model.Name.Value = value;
    public void InputBehaviour(PlayerBehaviour value) => Model.Behaviour.Value = value;
    public void InputAddPoint()
    {
        Model.Points.Value += 1;
    }
    public void InputSelect()
    {
        onSelected?.Invoke(this);
    }
    public void InputShow()
    {
        onShow?.Invoke();
    }


}