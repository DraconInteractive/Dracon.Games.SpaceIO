using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class AICharacter : Character
{
    [Header("Character - AI")]
    public AIConfig config;

    public BehaviourTree SetupTree;
    [FormerlySerializedAs("bt")]
    public BehaviourTree UpdateTree;
    public AIBlackboard blackboard;
    public List<IModule> modules;
    protected Player _player;
    
    public override void Initialize()
    {
        maxHealth = config.maxHealth;
        
        base.Initialize();
        
        blackboard = new AIBlackboard();
        blackboard.targetPosition = transform.position;
        _player = GameManager.Instance.player;
        UpdateTree.Initialize(this);
        SetupTree.Initialize(this);
        foreach (var mod in modules)
        {
            mod.Register(this);
        }
        
        SetupTree.Evaluate();
    }

    private void FixedUpdate()
    {
        if (!alive)
        {
            return;
        }

        if (!UpdateTree.evaluating)
        {
            UpdateTree.Evaluate();
        }
        
        Move();
    }

    protected virtual void Move()
    {
        if (Vector3.Distance(transform.position, blackboard.targetPosition) > config.movementStartThreshold)
        {
            Vector3 dir = blackboard.targetPosition - _cachedTransform.position;
            Quaternion targetRot = Quaternion.LookRotation(dir);
            if (blackboard.canStrafe)
            {
                _cachedTransform.position += dir.normalized * (config.speed * Time.fixedDeltaTime);
            }
            else
            {
                _cachedTransform.position += _cachedTransform.forward * (config.speed * Time.fixedDeltaTime);
            }
            if (blackboard.faceVelocity)
            {
                _cachedTransform.rotation = Quaternion.RotateTowards(_cachedTransform.rotation, targetRot, config.rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!EditorApplication.isPlaying) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(blackboard.targetPosition, 0.1f);
        Gizmos.DrawLine(transform.position, blackboard.targetPosition);
    }

    public T GetModuleOfType<T>() where T : IModule
    {
        T module = modules.OfType<T>().FirstOrDefault();
        return module;
    }
}

public class AIBlackboard
{
    public Vector3 targetPosition;
    public bool canStrafe;
    public bool faceVelocity;
}
