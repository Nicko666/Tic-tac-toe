using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

internal class RulesButtonPresenter : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _boardImage, _levelsImage;

    internal event UnityAction onInputButton
    {
        add => _button.onClick.AddListener(value);
        remove => _button.onClick.RemoveListener(value);
    }

    internal void OutputFieldModel(RulesModel rules)
    {
        _boardImage.sprite = rules.board.Icon;
        _levelsImage.sprite = rules.levels.Icon;
    }
}
