using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class PlayersListPresenter : MonoBehaviour
{
    [SerializeField] private CanvasesPresenter _canvases;
    [SerializeField] private Transform _itemCellPrefab, _draggedItemContent;
    [SerializeField] private PlayersListItemPresenter _itemPrefab;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _addButton, _removeButton;
    [SerializeField] private Hoover _deleteHoover, _scrollUpHoover, _scrollDownHoover;
    [SerializeField] private float _scrollSensitivity, _itemsSpeed;
    private readonly List<(Transform cell, PlayersListItemPresenter item, PlayerModel model)> _items = new();
    private PlayersListItemPresenter _draggingItem, _removingItem;

    internal Action<SoundModel> onInputSound;
    internal Action<PlayerModel> onInputPlayer;
    internal Action<PlayerModel, int> onInputPlayerIndex;
    internal Action<PlayerModel> onInputRemovePlayer;
    internal Action onInputAddLastPlayer;
    internal Action onInputRemoveLastPlayer;

    internal void OutputPlayers(PlayerModel[] players)
    {
        while (players.Length > _items.Count)
        {
            Transform cell = Instantiate(_itemCellPrefab, _scrollRect.content);
            PlayersListItemPresenter item = Instantiate(_itemPrefab, cell);
            item.onInputClick += InputItemClick;
            item.onInputHold += InputItemPress;
            item.onInputBeginDrag += InputBeginDrag;
            item.onInputDrag += InputDrag;
            item.onInputEndDrag += InputEndDrag;
            item.onInputEnter += InputEnter;
            _items.Add(new (cell, item, null));
        }
        while (players.Length < _items.Count)
        {
            Transform cell = _items[^1].cell;
            PlayersListItemPresenter item = _items[^1].item;
            item.onInputClick -= InputItemClick;
            item.onInputHold -= InputItemPress;
            item.onInputBeginDrag -= InputBeginDrag;
            item.onInputDrag -= InputDrag;
            item.onInputEndDrag -= InputEndDrag;
            item.onInputEnter -= InputEnter;
            _items.Remove(_items[^1]);
            Destroy(item.gameObject);
            Destroy(cell.gameObject);
        }

        for (int i = 0; i < _items.Count; i++)
        {
            int oldIndex = _items.Exists(j => j.model == players[i]) ?
                _items.FindIndex(j => j.model == players[i]) :
                _items.FindIndex(j => !Array.Exists(players, x => j.model == x));

            var playerPresenter = _items[oldIndex].item;
            var playerModel = players[i];

            _items[oldIndex] = new(_items[oldIndex].cell, _items[i].item, _items[i].model);
            _items[i] = new(_items[i].cell, playerPresenter, playerModel);
            if (_items[i].item != _draggingItem)
                //StartCoroutine(SetParentRoutne(_items[i].cell, _items[i].item));
                  _items[i].item.transform.SetParent(_items[i].cell, false);

            _items[i].item.OutputPlayer(players[i]);
            _items[i].item.OutputIsDraggin(_draggingItem != null);
        }

        _deleteHoover.OutputInteractable(players.Length > 2);
    }


    private IEnumerator SetParentsRoutne(Transform cell, PlayersListItemPresenter item)
    {
        
        //for (int i = 0; i < _items.Count; i++)
        //    if (_items[i].item != _draggingItem)
        //        _items[i].item.transform.SetParent(_items[i].cell, false);

        item.transform.SetParent(cell, true);

        while (item.transform.localPosition != Vector3.zero && _itemsSpeed > 0)
        {
            Debug.Log(item.transform.localPosition);

            item.transform.localPosition = 
                Vector3.MoveTowards(item.transform.localPosition, Vector3.zero, _itemsSpeed/Time.deltaTime);
            yield return null;
        }

        item.transform.SetParent(cell, false);

        //item.transform.localPosition = Vector3.zero;
    }

    private void Awake()
    {
        _deleteHoover.onEnter += InputRemoveItemHoover;
        _deleteHoover.onDrop += InputRemoveItemDrop;
        _scrollUpHoover.onHoover += InputScrollUp;
        _scrollDownHoover.onHoover += InputScrollDown;
        _addButton.onClick.AddListener(InputAddLastPlayer);
        _removeButton.onClick.AddListener(InputRemoveLastPlayer);
    }
    private void OnDestroy()
    {
        _deleteHoover.onEnter -= InputRemoveItemHoover;
        _deleteHoover.onDrop -= InputRemoveItemDrop;
        _scrollUpHoover.onHoover -= InputScrollUp;
        _scrollDownHoover.onHoover -= InputScrollDown;
        _addButton.onClick.RemoveListener(InputAddLastPlayer);
        _removeButton.onClick.RemoveListener(InputRemoveLastPlayer);
    }
    private void Start() =>
        _canvases.OutputDragged(false);

    private void InputAddLastPlayer()
    {
        onInputAddLastPlayer.Invoke();
        onInputSound.Invoke(SoundModel.Accept);
    }

    private void InputRemoveLastPlayer()
    {
        onInputRemoveLastPlayer.Invoke();
        onInputSound.Invoke(SoundModel.Accept);
    }

    private void InputScrollUp() =>
        _scrollRect.verticalNormalizedPosition += _scrollSensitivity / _scrollRect.content.rect.height;
    private void InputScrollDown() =>
        _scrollRect.verticalNormalizedPosition -= _scrollSensitivity / _scrollRect.content.rect.height;

    private void InputRemoveItemHoover(bool value) =>
        _removingItem = value ? _draggingItem : null;
    private void InputRemoveItemDrop()
    {
        if (_removingItem)
        {
            PlayerModel playerModel = _items.Find(i => i.item == _removingItem).model;
            onInputRemovePlayer.Invoke(playerModel);
        }
    }

    private void InputItemClick(PlayerModel player)
    {
        onInputPlayer.Invoke(player);
        onInputSound.Invoke(SoundModel.Accept);
    }

    private void InputItemPress(PlayersListItemPresenter item, bool isHolding)
    {
        if (isHolding)
        {
            _draggingItem = item;
            _draggingItem?.transform.SetParent(_draggedItemContent);
            onInputSound.Invoke(SoundModel.Accept);
        }
        else
        {
            _draggingItem?.transform.SetParent(_items.Find(i => i.item == item).cell, false);
            _draggingItem = null;
            onInputSound.Invoke(SoundModel.Accept);
        }
        _items.ForEach(i => i.item.OutputIsDraggin(isHolding));

        for (int i = 0; i < _items.Count; i++)
            if (_items[i].item != _draggingItem)
                _items[i].item.transform.SetParent(_items[i].cell, false);

        _canvases.OutputDragged(_draggingItem != null);
    }

    private void InputEnter(PlayersListItemPresenter presenter)
    {
        if (!_draggingItem) return;
        var draggingModel = _items.Find(i => i.item == _draggingItem).model;

        var item = _items.Find(i => i.item == presenter);
        int index = _items.IndexOf(item);
        onInputPlayerIndex.Invoke(draggingModel, index);
    }

    private void InputBeginDrag(PlayersListItemPresenter presenter, PointerEventData eventData)
    {
        if (!_draggingItem)
            _scrollRect.OnBeginDrag(eventData);
    }
    private void InputEndDrag(PlayersListItemPresenter presenter, PointerEventData eventData)
    {
        if (!_draggingItem)
            _scrollRect.OnEndDrag(eventData);
    }

    private void InputDrag(PlayersListItemPresenter presenter, PointerEventData eventData)
    {
        if (_draggingItem)
            _draggingItem.OutputDrag(Vector2.up * (eventData.delta / _canvas.scaleFactor));
        else
            _scrollRect.OnDrag(eventData);
    }


    [Serializable]
    class CanvasesPresenter
    {
        [SerializeField] private CanvasGroup _dragCanvasGroup, _editCanvasGroup;
        [SerializeField] private Animator _animator;
        private static readonly int AnimatorDraggedBool = Animator.StringToHash("Dragged");

        internal void OutputDragged(bool value)
        {
            _editCanvasGroup.interactable = !value;
            _editCanvasGroup.blocksRaycasts = !value;
            _dragCanvasGroup.interactable = value;
            _dragCanvasGroup.blocksRaycasts = value;
            _animator.SetBool(AnimatorDraggedBool, value);
        }
    }
}
