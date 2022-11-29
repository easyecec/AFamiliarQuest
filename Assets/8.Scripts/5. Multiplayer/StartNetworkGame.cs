using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNetworkGame : MonoBehaviour
{
    [SerializeField] private NetworkRunner _networkRunner;
    [SerializeField] private string _roomName;
    [SerializeField] private string _sceneName;

    // private void Awake()
    // {
    //      DontDestroyOnLoad(this);
    //}
    async void StartNewGame(GameMode mode)
    {
        // var gameArgs = new StartGameArgs();
        // gameArgs.GameMode= mode;
        await _networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = _roomName,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
        _networkRunner.SetActiveScene(_sceneName);

    }

    public void StartGameAsHost()
    {
        StartNewGame(GameMode.AutoHostOrClient);
    }

    public void StartGameAsClient()
    {
        StartNewGame(GameMode.Client);
    }

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("Player Joined");
    }
}
