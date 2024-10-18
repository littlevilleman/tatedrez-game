using Client.UI;
using Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchResultPopup : Popup<MatchResultPopupContext>
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private TMP_Text resultText;
    private Action confirmCallback;

    public override void Display(MatchResultPopupContext context)
    {
        confirmCallback = context.confirmCallback;
        resultText.text = $"{context.victoryPlayer.Name} wins!";
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        confirmButton.onClick.AddListener(OnClickConfirmButton);
    }

    private void OnClickConfirmButton()
    {
        confirmCallback?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        confirmButton.onClick.RemoveListener(OnClickConfirmButton);
    }
}


public class MatchResultPopupContext : PopupContext
{
    public Action confirmCallback;
    public IPlayer victoryPlayer;
}