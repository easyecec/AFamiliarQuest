using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelCompleted_UI : MonoBehaviour
{
    [SerializeField] private GameObject explotionGO;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            explotionGO.SetActive(true);

            Destroy(gameObject);
        }
    }
}
