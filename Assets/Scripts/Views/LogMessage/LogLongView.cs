using TMPro;
using UnityEngine;

public class LogLongView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    internal void OutputLog(LogModel log) =>
        _text.text = log.stackTrace.ToString();
}
