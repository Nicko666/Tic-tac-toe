using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglesImageView : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private ToggleImageView _togglePrefab;
    private readonly List<ToggleImageView> _toggles = new();

    public Action<int> onInput;

    public void OutputToggles(Sprite[] sprites)
    {
        while (_toggles.Count < sprites.Length)
        {
            ToggleImageView toggle;
            
            toggle = Instantiate(_togglePrefab, _scrollRect.content);
            _toggles.Add(toggle);
            toggle.onClick += InputToggle;
        }
        while (_toggles.Count > sprites.Length)
        {
            ToggleImageView toggle = _toggles[^1];

            _toggles[^1].onClick -= InputToggle;
            _toggles.Remove(toggle);
            Destroy(toggle.gameObject);
        }
        for (int i = 0; i < sprites.Length; i++)
            _toggles[i].OutputSprite(sprites[i]);
    }

    public void OutputIsToggled(int index)
    {
        for (int i = 0; i < _toggles.Count; i++)
            _toggles[i].OutputIsToggled(i == index);
    }

    private void InputToggle(ToggleImageView toggle)
    {
        int index = _toggles.IndexOf(toggle);
        onInput.Invoke(index);
    }
}
