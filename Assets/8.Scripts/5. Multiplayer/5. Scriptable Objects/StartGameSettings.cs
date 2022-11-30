using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[CreateAssetMenu(menuName = "ScriptableObjects/CreateGameSettings", fileName ="GameSettings")]
public class StartGameSettings : ScriptableObject
{
    public GameMode gameMode;

    public Vector3 spawnPosition;

}
