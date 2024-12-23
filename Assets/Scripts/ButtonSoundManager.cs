using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonSoundMapping
    {
        public Button button;      // Tombol yang ingin diberi suara
        public AudioClip clickSound; // Suara untuk tombol tersebut
    }

    public List<ButtonSoundMapping> buttonSoundMappings = new List<ButtonSoundMapping>();
    private AudioSource audioSource;

    void Awake()
    {
        // Jangan hancurkan GameObject saat pindah scene
        DontDestroyOnLoad(gameObject);

        // Ambil atau tambahkan komponen AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        // Tambahkan event listener untuk setiap tombol dalam mapping
        foreach (ButtonSoundMapping mapping in buttonSoundMappings)
        {
            if (mapping.button != null && mapping.clickSound != null)
            {
                // Variabel lokal untuk menghindari masalah referensi
                Button currentButton = mapping.button;
                AudioClip currentClip = mapping.clickSound;

                // Tambahkan listener untuk memainkan suara
                currentButton.onClick.AddListener(() => PlayClickSound(currentClip));
            }
        }
    }

    public void PlayClickSound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // Mainkan suara
        }
    }
}
