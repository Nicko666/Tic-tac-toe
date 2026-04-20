using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersWinnerPresenter : MonoBehaviour
{
    [SerializeField] private Image _markImage;
    //[SerializeField] private Image _logicImage;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _winString, _drawString;


    internal void OutputPlayer(PlayerModel player)
    {
        if (player == null)
        {
            _markImage.gameObject.SetActive(false);
            _text.text = _drawString;
        }
        else
        {
            _markImage.gameObject.SetActive(true);      
            //_logicImage.sprite = player.logic.Icon;
            _markImage.sprite = player.mark.Icon;
            _markImage.color = Color.HSVToRGB(player.hue, player.saturation, 1);
            _text.text = _winString;
        }
    }

}
