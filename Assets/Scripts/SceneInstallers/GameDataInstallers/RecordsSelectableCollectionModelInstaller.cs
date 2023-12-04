using System.Collections.Generic;
using UnityEngine;

public class RecordsSelectableCollectionModelInstaller : PersistentGameDataInstaller
{
    CollectionModel<Record> _recordsSelectableCollectionModel;


    public override void InstallBindings()
    {
        Container.Bind<CollectionModel<Record>>().FromInstance(_recordsSelectableCollectionModel).AsSingle().NonLazy();
    }

    public override void Load(GameData gameData, DatabaseInstaller database, RandomPlayerGenerator randomPlayerGenerator)
    {
        List<Record> records = new List<Record>();

        foreach (var recordData in gameData.recordsData)
        {
            Record newRecord = GetRecord(recordData, database);
            if (newRecord != null)
                records.Add(newRecord);
        }

        _recordsSelectableCollectionModel = new(records);
        
    }

    public override void Save(GameData gameData, DatabaseInstaller database)
    {
        gameData.recordsData.Clear();

        foreach (var record in _recordsSelectableCollectionModel.collection.Value)
        {
            RecordData recordData = GetRecordData(record.property.Value, database);
            if (recordData != null)
                gameData.recordsData.Add(recordData);
        }

    }

    Record GetRecord(RecordData recordData, DatabaseInstaller database)
    {
        List<PlayerModel> playersModels = new();
        foreach (PlayerData playerData in recordData.playersDatas)
        {
            float hue = playerData.hue;
            PlayerMark mark = database.playerMarksDatabase.GetItemByIndex(playerData.markID);
            string name = playerData.name;
            PlayerBehaviour playerBehaviour = database.playerBehavioursDatabase.GetItemByIndex(playerData.playerBehaviourID);
            int points = playerData.points;

            playersModels.Add(new PlayerModel(hue, mark, name, playerBehaviour, points));
        }

        Field field = database.fieldDatabase.GetItemByIndex(recordData.fieldIndex);
        PlayersSorting playersSorting = database.playersSortingDatabase.GetItemByIndex(recordData.playersSortingIndex);
        MaxPoints maxPoints = database.maxPointsDatabase.GetItemByIndex(recordData.maxPointsIndex);

        return new(playersModels, field, playersSorting, maxPoints);

    }

    RecordData GetRecordData(Record record, DatabaseInstaller database)
    {
        List<PlayerData> playersDatas = new();
        foreach (var player in record.playersModels)
        {
            float hue = player.Hue.Value;
            int markID = database.playerMarksDatabase.GetIndexByItem(player.Mark.Value);
            string name = player.Name.Value;
            int playerBehaviourID = database.playerBehavioursDatabase.GetIndexByItem(player.Behaviour.Value);
            int points = player.Points.Value;

            playersDatas.Add(new PlayerData(hue, markID, name, playerBehaviourID, points));
        }

        int fieldIndex = database.fieldDatabase.GetIndexByItem(record.field);
        int playersSortingIndex = database.playersSortingDatabase.GetIndexByItem(record.playersSorting);
        int maxPointsIndex = database.maxPointsDatabase.GetIndexByItem(record.maxPoints);

        return new(playersDatas, fieldIndex, playersSortingIndex, maxPointsIndex);

    }


}
