using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    PlayerViewModel _viewModel;
    public PlayerViewModel ViewModel => _viewModel;

    [SerializeField] Image _playerMarkImage;
    [SerializeField] Image _playerBehaviourImage;
    [SerializeField] TMP_Text _tmpName;
    [SerializeField] MaskableGraphic[] _maskableGraphics;
    [SerializeField] Sprite _nullSprite;
    [SerializeField] TMP_Text _tmpPoints;

    [SerializeField] RectTransform _animatedRectTransform;

    public void Init(PlayerViewModel viewModel)
    {
        if (_viewModel != null)
            ViewUnsubscribe();

        _viewModel = viewModel;

        if (_viewModel != null)
            ViewSubscribe();

        ViweUpdate();

    }
    void ViewSubscribe()
    {
        _viewModel.Mark.onValueChanged += OutputMark;
        _viewModel.Hue.onValueChanged += HueOutput;
        _viewModel.Behaviour.onValueChanged += BehaviourOutput;
        _viewModel.Name.onValueChanged += NameOutput;
        _viewModel.Points.onValueChanged += PointsOutput;
        _viewModel.onShow += ShowOutput;
    }
    void ViewUnsubscribe()
    {
        _viewModel.Mark.onValueChanged -= OutputMark;
        _viewModel.Hue.onValueChanged -= HueOutput;
        _viewModel.Behaviour.onValueChanged -= BehaviourOutput;
        _viewModel.Name.onValueChanged -= NameOutput;
        _viewModel.Points.onValueChanged -= PointsOutput;
        _viewModel.onShow -= ShowOutput;
    }
    void ViweUpdate()
    {
        if (_viewModel == null)
        {
            OutputMark(null);
            BehaviourOutput(null);
            HueOutput(0.0f);
            NameOutput("");
            PointsOutput(0);
            return;
        }
        else
        {
            OutputMark(_viewModel.Mark.Value);
            BehaviourOutput(_viewModel.Behaviour.Value);
            HueOutput(_viewModel.Hue.Value);
            NameOutput(_viewModel.Name.Value);
            PointsOutput(_viewModel.Points.Value);
        }

    }


    void OutputMark(PlayerMark playerMarkModel)
    {
        if (_playerMarkImage == null) return;

        _playerMarkImage.sprite = playerMarkModel != null ? playerMarkModel.GetSprite() : _nullSprite;
    }

    void BehaviourOutput(PlayerBehaviour playerBehaviourModel)
    {
        if (_playerBehaviourImage == null) return;

        _playerBehaviourImage.sprite = playerBehaviourModel ? playerBehaviourModel.GetSprite() : _nullSprite;
    }

    void HueOutput(float value)
    {
        foreach (var maskableGraphic in _maskableGraphics)
        {
            maskableGraphic.color = ColorLibrary.ReadH(value, maskableGraphic.color);
        }
    }

    void NameOutput(string value)
    {
        if (_tmpName == null) return;

        _tmpName.text = value;
    }

    void PointsOutput(int value)
    {
        if (_tmpPoints == null) return;

        _tmpPoints.text = value.ToString();
    }

    public void SelectRequest()
    {
        _viewModel.InputSelect();
    }


    void ShowOutput()
    {
        if (_animatedRectTransform != null)
        {
            _animatedRectTransform.DOScale(0.5f, 0.2f);
            _animatedRectTransform.DOScale(1.0f, 0.2f).SetDelay(0.2f);
            _animatedRectTransform.DOScale(0.5f, 0.2f).SetDelay(0.4f);
            _animatedRectTransform.DOScale(1.0f, 0.2f).SetDelay(0.6f);

        }

    }


}
