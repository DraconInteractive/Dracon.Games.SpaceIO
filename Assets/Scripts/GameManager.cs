using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public enum GameState
    {
        NotStarted, 
        Running, 
        Ended
    }

    public static GameState state;

    [Space] 
    public GameConfig config;
    
    [Space]
    public Player playerPrefab;
    public Player player;
    
    public static float DistanceToPlayer(Vector3 point) => Vector3.Distance(point, Instance.player.transform.position);
    public override void Initialize()
    {
        base.Initialize();
        Register(this);
        CameraManager.Instance.SelectCamera(0);
        UIManager.Instance.ShowScreen(typeof(Screen_Menu));
        state = GameState.NotStarted;
        Initialized = true;
    }

    public void StartGame()
    {
        SpawnPlayer(Vector3.zero);
        EnemyManager.Instance.SpawnWave();
        
        CameraManager.Instance.SelectCamera(1);
        UIManager.Instance.ShowScreen(typeof(Screen_HUD));
        
        EnemyManager.Instance.onWaveFinished += OnWaveFinished;
        EnemyManager.Instance.onAllWavesFinished += OnAllWavesFinished;
        
        state = GameState.Running;
    }

    public void EndGame()
    {
        CameraManager.Instance.SelectCamera(0);
        UIManager.Instance.ShowScreen(typeof(Screen_EndGame));
        EnemyManager.Instance.onWaveFinished -= OnWaveFinished;
        EnemyManager.Instance.onAllWavesFinished -= OnAllWavesFinished;
        
        state = GameState.Ended;
    }

    private void Update()
    {
        if (state == GameState.Running)
        {
            GameTick();
        }
    }

    void GameTick()
    {
    }
    
    public void SpawnPlayer(Vector3 pos)
    {
        if (player != null)
        {
            Destroy(player.gameObject);
        }

        player = Instantiate(playerPrefab, pos, Quaternion.identity, this.transform);
        player.Initialize();
    }

    private void OnWaveFinished()
    {
        // Show UI?
        EnemyManager.Instance.NextWave();
    }

    private void OnAllWavesFinished()
    {
        EndGame();
    }
}
