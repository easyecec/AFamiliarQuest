using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private List<NetworkObject> _networkedObjects;
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
        NetworkObject _object = runner.Spawn(_playerPrefab, Vector3.zero, Quaternion.identity);
        _networkedObjects.Add(_object);
        Debug.Log($" {_networkedObjects.Count} objects in simulation. ");
    }
}
