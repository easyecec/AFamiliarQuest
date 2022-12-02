using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    //[SerializeField] private List<NetworkObject> _networkedObjects;
    private Dictionary<PlayerRef,NetworkObject> _spawnedObjects = new Dictionary<PlayerRef,NetworkObject>();
    [SerializeField] StartGameSettings _startGameSettings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer(NetworkRunner runner, PlayerRef playerRef)
    {
        Debug.Log("Player Spawned");
        NetworkObject _object = runner.Spawn(_playerPrefab, _startGameSettings.spawnPosition, Quaternion.identity);
        _spawnedObjects.Add(playerRef, _object);
        Debug.Log($" {_spawnedObjects.Count} objects in simulation. ");
    }


}
