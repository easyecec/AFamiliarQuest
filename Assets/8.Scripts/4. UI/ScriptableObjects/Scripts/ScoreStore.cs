using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScoreStore : ScriptableObject
{
    [SerializeField] private int _score;
    [SerializeField] private int _hats;

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    public int Hats
    {
        get { return _hats; }
        set { _hats = value; }
    }
}
