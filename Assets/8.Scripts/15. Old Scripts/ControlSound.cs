using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlSound : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private Slider volumeFX;
    [SerializeField] private Slider volumeMaster;
    [SerializeField] private Toggle mute;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private float lastVolume;

    private void Awake()
    {
        volumeFX.onValueChanged.AddListener(ChangeVolumeFX);
        volumeMaster.onValueChanged.AddListener(ChangeVolumeMaster);
    }

    public void SetMute()
    {
        if (mute.isOn)
        {
            mixer.GetFloat("VolMaster", out lastVolume);
            mixer.SetFloat("VolMaster", -80);
        }
        else
        {
            mixer.SetFloat("VolMaster", lastVolume);
        }
    }
    public void ChangeVolumeMaster(float volumen)
    {
        mixer.SetFloat("VolMaster", volumen);
    }

    public void ChangeVolumeFX(float volumen)
    {
        mixer.SetFloat("VolFx", volumen);
    }
}
