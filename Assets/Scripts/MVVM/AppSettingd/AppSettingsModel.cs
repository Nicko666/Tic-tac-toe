public class AppSettingsModel
{
    public ReactiveProperty<Locals> locals = new();
    public ReactiveProperty<Theme> theme = new();
    public ReactiveProperty<float> volume = new();


    public AppSettingsModel(Locals locals, Theme theme, float volume) 
    {
        this.locals.Value = locals;
        this.theme.Value = theme;
        this.volume.Value = volume;

    }


}
