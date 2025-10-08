using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Menu : UIScreen
{
    public Button startGameBtn;

    public override void Show()
    {
        base.Show();
        startGameBtn.onClick.AddListener(StartGame);
    }

    public override void Hide()
    {
        base.Hide();
        startGameBtn.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
