using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIModule : MonoBehaviour, IModule
{
    public Image healthFill;
    public TMP_Text healthText;

    private Enemy _target;
    
    public void Register(AICharacter character)
    {
        _target = character as Enemy;
        _target.onDamaged += UpdateUI;
        _target.onHeal += UpdateUI;
        _target.onDeath += UpdateUI;
    }

    public void Deregister()
    {
        _target.onDamaged -= UpdateUI;
        _target.onHeal -= UpdateUI;
        _target.onDeath -= UpdateUI;
    }
    
    // Ignore argument, not relevant here
    private void UpdateUI(float _)
    {
        healthFill.fillAmount = _target.currentHealth / _target.maxHealth;
        healthText.text = _target.currentHealth.ToString("F0");
    }

    private void UpdateUI(Character _)
    {
        healthFill.fillAmount = _target.currentHealth / _target.maxHealth;
        healthText.text = _target.currentHealth.ToString("F0");
    }
}
