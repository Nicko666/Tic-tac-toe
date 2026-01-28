using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglesTextView : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private ToggleTextView _togglePrefab;
    private readonly List<ToggleTextView> _toggles = new();

    public Action<int> onInput;

    public void OutputToggles(string[] text)
    {
        while (_toggles.Count < text.Length)
        {
            ToggleTextView toggle;

            toggle = Instantiate(_togglePrefab, _scrollRect.content);
            _toggles.Add(toggle);
            toggle.onClick += InputToggle;
        }
        while (_toggles.Count > text.Length)
        {
            ToggleTextView toggle = _toggles[^1];

            _toggles[^1].onClick -= InputToggle;
            _toggles.Remove(toggle);
            Destroy(toggle.gameObject);
        }
        for (int i = 0; i < text.Length; i++)
            _toggles[i].OutputSprite(text[i]);
    }

    public void OutputIsToggled(int index)
    {
        for (int i = 0; i < _toggles.Count; i++)
            _toggles[i].OutputIsToggled(i == index);
    }

    private void InputToggle(ToggleTextView toggle)
    {
        int index = _toggles.IndexOf(toggle);
        onInput.Invoke(index);
    }
}
