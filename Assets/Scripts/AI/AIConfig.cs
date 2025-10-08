using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Enemy")]
public class AIConfig : ScriptableObject
{
    public float maxHealth = 25f;
    public float speed = 5f;
    public float rotationSpeed = 360f;
    public float movementStartThreshold = 0.5f;
}
