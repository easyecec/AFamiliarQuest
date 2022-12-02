using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplayMulti : MonoBehaviour
{
    ObjectSpawnController spawnController;

    [SerializeField] private GameObject[] coinDisplays;
    private Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        spawnController = GameObject.Find("ObjectSpawnController").GetComponent<ObjectSpawnController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var player in spawnController._spawnedObjects)
        {
            for (int i = 0; i < spawnController._spawnedObjects.Count; i++)
            {
                coinDisplays[i].SetActive(true);
                coinDisplays[i].GetComponentInChildren<Text>().text = player.Value.gameObject.GetComponent<PlayerManagerMulty>().Coins.ToString("00");
            }
            //player.Value.gameObject;
        }
    }
}
