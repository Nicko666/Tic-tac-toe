using UnityEngine;
using UnityEngine.Events;

public class AndroidInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
        {
            //Application.Quit();
            anyButtonEvent?.Invoke();
            return;
        }
    }

    public UnityEvent anyButtonEvent;




}
