using TMPro;
using UnityEngine;

public class PlayerNameView : MonoBehaviour
{
    PlayerNameViewModel playerNameViewModel;

    [SerializeField] TMP_InputField inputField;


    public void Init(PlayerNameViewModel playerNameViewModel)
    {
        if (this.playerNameViewModel != null)
            ViewUnsubscribe();

        this.playerNameViewModel = playerNameViewModel;

        ViewUpdate();

        if (this.playerNameViewModel != null)
            ViewSubscribe();

    }

    public void ViewSubscribe()
    {
        playerNameViewModel.playerName.onValueChanged += OutputName;
    }
    public void ViewUnsubscribe()
    {
        playerNameViewModel.playerName.onValueChanged -= OutputName;
    }
    public void ViewUpdate()
    {
        OutputName(playerNameViewModel.playerName.Value);
    }

    private void Start()
    {
        inputField.onValueChanged.AddListener(InputName);
    }

    public void InputName(string value)
    {
        playerNameViewModel.InputName(value);
    }
    void OutputName(string value)
    {
        inputField.SetTextWithoutNotify(value);
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(InputName);
    }


}
