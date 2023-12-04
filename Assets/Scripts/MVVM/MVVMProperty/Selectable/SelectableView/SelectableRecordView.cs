using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectableRecordView : SelectableView<Record>
{
    [SerializeField] CanvasGroup _canvasGroup;

    [SerializeField] Image _fieldImage;
    [SerializeField] Image _playersSortingImage;
    [SerializeField] Image _maxPointsImage;

    [SerializeField] RectTransform _content;
    [SerializeField] PlayerView _playerViewPrefab;


    protected override void OutputProperty(Record property)
    {
        _fieldImage.sprite = property.field.GetSprite();
        _playersSortingImage.sprite = property.playersSorting.GetSprite();
        _maxPointsImage.sprite = property.maxPoints.GetSprite();

        var playermodels = SetQue(property.playersModels);
        foreach (var playerModel in playermodels)
        {
            PlayerViewModel playerViewModel = new(playerModel);
            PlayerView newPlayerView = Instantiate(_playerViewPrefab, _content);
            newPlayerView.Init(playerViewModel);
        }

    }

    IEnumerable<PlayerModel> SetQue(IEnumerable<PlayerModel> players)
    {
        PlayerModel[] playersModels = players.ToArray();

        for (var i = 1; i < playersModels.Count(); i++)
        {
            for (var j = 0; j < playersModels.Count() - 1; j++)
            {
                if (playersModels[j].Points.Value < playersModels[j+1].Points.Value)
                {
                    var temp = playersModels[j];
                    playersModels[j] = playersModels[j + 1];
                    playersModels[j + 1] = temp;
                }
            }
        }

        return playersModels;
    }

    protected override void OutputSelected(bool value)
    {
        _canvasGroup.DOFade(value ? 1 : 0, 0.1f);
    }


}
