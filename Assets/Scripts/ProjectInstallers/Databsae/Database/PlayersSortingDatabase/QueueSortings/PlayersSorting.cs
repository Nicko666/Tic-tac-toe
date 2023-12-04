  using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayersSorting", menuName = "ScriptableObjects/PlayersSorting")]
public class PlayersSorting : ScriptableObject, ISprite
{
    [SerializeField] Sprite _sprite;
    [SerializeField] string _description;
    [SerializeField] SortingType _sortingType;

    public string Description => _description;
    public IPlayersSortingHeandler SortingComand
    {
        get
        {
            switch (_sortingType)
            {
                case SortingType.WinnserFirst: return new WinnerFirstSortingCommand();
                case SortingType.WinnserLast: return new WinnerLastSortingCommand();
                case SortingType.LiderFirst: return new LiderFirstCommand();
                case SortingType.LiderLast: return new LiderLastCommand();
                default: Debug.LogError("Unknown QueueSortingType"); return null;
            }

        }

    }

    //public void Sort(ReactiveCollection<PlayerViewModel> playersViewModels)
    //{
    //    switch (_sortingType)
    //    {
    //        case SortingType.WinnserFirst: return new WinnerFirstSortingCommand();
    //        case SortingType.WinnserLast: return new WinnerLastSortingCommand();
    //        case SortingType.LiderFirst: return new LiderFirstCommand();
    //        case SortingType.LiderLast: return new LiderLastCommand();
    //        default: Debug.LogError("Unknown QueueSortingType"); return null;
    //    }
    //}

    public Sprite GetSprite() => _sprite;


}


enum SortingType
{
    WinnserFirst,
    WinnserLast,
    LiderFirst,
    LiderLast

}