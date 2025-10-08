using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProfileManager : Manager<ProfileManager>
{
    public UserProfile currentProfile;

    public UnityEvent<UserProfile> onProfileUpdated;
    
    public override void Initialize()
    {
        base.Initialize();
        Register(this);

        if (Load(out UserProfile profile))
        {
            currentProfile = profile;
        }
        else
        {
            currentProfile = UserProfile.CreateTemp();
        }
        
        onProfileUpdated?.Invoke(currentProfile);
        Initialized = true;
    }

    public bool Save()
    {
        return false;
    }

    public bool Load(out UserProfile result)
    {
        result = null;
        return false;
    }

    public void UpdateProfile(int level = -1, int exp = -1)
    {
        if (level != -1)
        {
            currentProfile.level = level;
        }

        if (exp != -1)
        {
            currentProfile.exp = exp;
        }
        
        onProfileUpdated?.Invoke(currentProfile);
    }
}

[System.Serializable]
public class UserProfile
{
    public string uuid;
    public string userID;
    public int level;
    public int exp;

    public static UserProfile Create()
    {
        UserProfile profile = new UserProfile();
        profile.uuid = System.Guid.NewGuid().ToString();
        profile.userID = "User_" + profile.uuid;
        profile.level = 1;
        profile.exp = 0;
        return profile;
    }

    public static UserProfile Create(string _uuid, string _userID, int _level = 1, int _exp = 1)
    {
        UserProfile profile = new UserProfile();
        profile.uuid = _uuid;
        profile.userID = _userID;
        profile.level = _level;
        profile.exp = _exp;
        return profile;
    }

    public static UserProfile CreateTemp() => UserProfile.Create("", "_");
}
