using UnityEngine;

public class RandomPlayerGenerator
{
    DatabaseModel<PlayerMark> _playerMarksDatabase;
    DatabaseModel<PlayerBehaviour> _playerBehavioursDatabase;


    public RandomPlayerGenerator(DatabaseModel<PlayerMark> playerMarksDatabase, DatabaseModel<PlayerBehaviour> playerBehavioursDatabase)
    {
        _playerMarksDatabase = playerMarksDatabase;
        _playerBehavioursDatabase = playerBehavioursDatabase;

    }

    public PlayerModel PlayerModel
    {
        get
        {
            float randomHue = UnityEngine.Random.Range(0.0f, 1.0f);
            PlayerMark randomPlayerMark = _playerMarksDatabase.GetRandomItem();
            //string randomNume = "name";
            string randomNume = GetRandomName();
            PlayerBehaviour randomPlayerBehaviour = _playerBehavioursDatabase.GetRandomItem();

            return new PlayerModel(randomHue, randomPlayerMark, randomNume, randomPlayerBehaviour, 0);
        }

    }

    string GetRandomName()
    {
        string result = "";

        string local = AppSettingsViewModel.locals.Code;

        switch (local)
        {
            case "en":
                result = "New player";
                break;
            case "ru":
                result = "Новый игрок";
                break;
            default:
                result = "New player";
                Debug.Log("Unindentefied languege");
                break;

        }

        return result;

    }


}
