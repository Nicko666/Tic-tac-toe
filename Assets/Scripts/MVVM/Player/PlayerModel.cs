public class PlayerModel
{
    public ReactiveProperty<float> Hue = new();
    public ReactiveProperty<PlayerMark> Mark = new();
    public ReactiveProperty<string> Name = new();
    public ReactiveProperty<PlayerBehaviour> Behaviour = new();
    public ReactiveProperty<int> Points = new();


    public PlayerModel(float hue, PlayerMark mark, string name, PlayerBehaviour playerBehaviour, int points)
    {
        Hue.Value = hue; 
        Mark.Value = mark; 
        Name.Value = name; 
        Behaviour.Value = playerBehaviour; 
        Points.Value = points;
    }


}
