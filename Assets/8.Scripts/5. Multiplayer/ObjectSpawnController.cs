using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    //[SerializeField] private List<NetworkObject> _networkedObjects;
    public Dictionary<PlayerRef,NetworkObject> _spawnedObjects = new Dictionary<PlayerRef,NetworkObject>(); 

    
    [SerializeField] StartGameSettings _startGameSettings;

    public void SpawnPlayer(NetworkRunner runner, PlayerRef playerRef)
    {
        Debug.Log("Player Spawned");
      
        NetworkObject _object = runner.Spawn(_playerPrefab, _startGameSettings.spawnPosition, Quaternion.identity, playerRef);
        _spawnedObjects.Add(playerRef, _object);
        Debug.Log($" {_spawnedObjects.Count} objects in simulation. ");
    }

    public void DespawnPlayer(NetworkRunner runner, PlayerRef playerRef)
    {
        if(_spawnedObjects.TryGetValue(playerRef, out NetworkObject networkObject))
        {
            Debug.Log("Player Despawned");

            runner.Despawn(networkObject);
            _spawnedObjects.Remove(playerRef);
        }
    }
}
