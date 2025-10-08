using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_HUD : UIScreen
{
    public TMP_Text weapon0Text;
    public Image healthBar;
    private Player _player;
    public override void Show()
    {
        base.Show();
        StartCoroutine(SetupRoutine());
    }

    IEnumerator SetupRoutine()
    {
        yield return 2;
        _player = GameManager.Instance.player;
        _player.onWeaponUpdate += OnPlayerWeaponUpdate;
        _player.onDamaged += OnPlayerDamaged;
        OnPlayerWeaponUpdate();
    }

    private void OnPlayerDamaged(float arg0)
    {
        healthBar.fillAmount = _player.currentHealth / _player.maxHealth;
    }

    private void OnPlayerWeaponUpdate()
    {
        var weapons = _player.weapons;
        if (weapons.Count > 0)
        {
            weapon0Text.text = _player.weapons[0].GetType().FullName;
        }
        else
        {
            weapon0Text.text = "No Weapon";
        }
        
    }

    private void OnHide()
    {
        _player.onWeaponUpdate -= OnPlayerWeaponUpdate;

    }
}
