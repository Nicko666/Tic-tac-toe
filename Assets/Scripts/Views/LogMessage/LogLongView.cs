using TMPro;
using UnityEngine;

internal class LogLongView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    internal void OutputLog(LogModel log) =>
        _text.text = log.stackTrace.ToString();
}
