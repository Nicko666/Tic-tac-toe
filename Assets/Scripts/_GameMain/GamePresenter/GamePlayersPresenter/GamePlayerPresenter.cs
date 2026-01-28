using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class GamePlayerPresenter : MonoBehaviour
{
    [SerializeField] private Image _logicImage, _markImage;
    [SerializeField] private TMP_Text _pointsText;

    internal void Output(GamePlayerModel gamePlayerModel)
    {
        _logicImage.sprite = gamePlayerModel.player.logic.Icon;
        _markImage.sprite = gamePlayerModel.player.mark.Icon;
        _markImage.color = Color.HSVToRGB(gamePlayerModel.player.hue, gamePlayerModel.player.saturation, 1f);
        _pointsText.text = gamePlayerModel.points.ToString();
    }
}