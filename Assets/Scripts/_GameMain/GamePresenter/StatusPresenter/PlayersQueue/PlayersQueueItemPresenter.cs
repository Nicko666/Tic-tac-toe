using UnityEngine;
using UnityEngine.UI;

internal class PlayersQueueItemPresenter : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _logicImage, _markImage;

    internal void OutputPlayer(PlayerModel playerModel)
    {
        if (playerModel == null)
            _canvasGroup.alpha = 0;
        else
        {
            _canvasGroup.alpha = 1;

            _logicImage.sprite = playerModel.logic.Icon;
            _markImage.sprite = playerModel.mark.Icon;
            _markImage.color = Color.HSVToRGB(playerModel.hue, playerModel.saturation, 1);
        }
    }
}
