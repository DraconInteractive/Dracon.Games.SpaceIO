using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIC_Profile : UIComponent
{
    public TMP_Text usernameText;
    public TMP_Text levelText;
    
    protected override void OnShow()
    {
        base.OnShow();
        UpdateUI(ProfileManager.Instance.currentProfile);
        ProfileManager.Instance.onProfileUpdated.AddListener(UpdateUI);
    }

    protected override void OnHide()
    {
        base.OnHide();
        ProfileManager.Instance.onProfileUpdated.RemoveListener(UpdateUI);
    }

    private void UpdateUI(UserProfile profile)
    {
        usernameText.text = profile.userID;
        levelText.text = profile.level.ToString();
    }
}
