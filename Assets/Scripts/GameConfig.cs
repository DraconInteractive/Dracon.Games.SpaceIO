using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Game")]
public class GameConfig : ScriptableObject
{
    public float arenaExtent = 100f;
    public float playerBaseRange = 7f;
    public float enemySpacing = 5f;
}
