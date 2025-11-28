using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    public Slider volumeSlider; // permite arrastrar el slider desde el inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // cargar el volumen guardado
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
            volumeSlider.value = audioSource.volume;
        }

        // agregar listener al slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); // guardar la preferencia
    }
}