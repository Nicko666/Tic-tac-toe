using UnityEngine;

public class PlayersQueuePresenter : MonoBehaviour
{
    [SerializeField] private PlayersQueueItemPresenter _lastItem;
    [SerializeField] private PlayersQueueItemPresenter _currentItem;
    [SerializeField] private Animator _animator;
    private static int AnimatorTrigger => Animator.StringToHash("Next");

    private PlayerModel _currentPlayer;

    public void OutputPlayer(PlayerModel player)
    {
        if (_currentPlayer == player) return;

        _lastItem.OutputPlayer(_currentPlayer);
        _currentPlayer = player;
        _currentItem.OutputPlayer(_currentPlayer);
        _animator.SetTrigger(AnimatorTrigger);
    }
}
