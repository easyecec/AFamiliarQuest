using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartNetworkGame : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkRunner _networkRunner;
    [SerializeField] private string _roomName;
    [SerializeField] private string _sceneName;
    //[SerializeField] private UnityEvent<NetworkRunner, PlayerRef> OnPlayerJoinedEvent;
    //[SerializeField] private UnityEvent<NetworkRunner, PlayerRef> OnPlayerLeftEvent;
    [SerializeField] private StartGameSettings _playerSpawnSettings;

    [SerializeField] private GameObject _playerPrefab;

    //[SerializeField] private List<NetworkObject> _networkedObjects;
    public Dictionary<PlayerRef, NetworkObject> _spawnedObjects = new Dictionary<PlayerRef, NetworkObject>();


    [SerializeField] StartGameSettings _startGameSettings;

    private void Awake()
    {
        StartNewGame();
    }

    async void StartNewGame()
    {
        await _networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = _playerSpawnSettings.gameMode,
            SessionName = _roomName,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
        _networkRunner.SetActiveScene(_sceneName);
    }

    /*
    public void StartGameAsHost()
    {
        StartNewGame(GameMode.AutoHostOrClient);
    }

    public void StartGameAsClient()
    {
        StartNewGame(GameMode.Client);
    }
    */

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Debug.Log("Player Spawned");

            NetworkObject _object = runner.Spawn(_playerPrefab, _startGameSettings.spawnPosition, Quaternion.identity, player);
            _spawnedObjects.Add(player, _object);
            Debug.Log($" {_spawnedObjects.Count} objects in simulation. ");
        }
        else
        {
            Debug.Log("Connected as Client"); 
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedObjects.TryGetValue(player, out NetworkObject networkObject))
        {
            Debug.Log("Player Despawned");

            runner.Despawn(networkObject);
            _spawnedObjects.Remove(player);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData data = new NetworkInputData();

        if (Input.GetKey(KeyCode.D))
        {
            data.Direction.z += Input.GetAxis("Horizontal");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            data.Direction.z += Input.GetAxis("Horizontal");
        }
        if (Input.GetKey(KeyCode.A))
        {
            data.Direction.z += Input.GetAxis("Horizontal");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            data.Direction.z += Input.GetAxis("Horizontal");
        }

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
       
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
   
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }
    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }


}
