using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControlMultiplayer : MonoBehaviour
{

    [SerializeField] private GameObject _playButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateButton()
    {
        Invoke("DeactivateWithDelay", 5);
    }

    void DeactivateWithDelay()
    {
        _playButton.SetActive(false);
    }
}
