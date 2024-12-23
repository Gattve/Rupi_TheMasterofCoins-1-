using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameChangeScene : MonoBehaviour
{
    // Dictionary untuk menyimpan tombol dan nama scene
    [System.Serializable]
    public struct ButtonSceneMapping
    {
        public Button button;      // Tombol UI
        public string sceneName;   // Nama scene untuk tombol tersebut
    }

    [SerializeField] private List<ButtonSceneMapping> buttonSceneMappings;

    void Start()
    {
        // Pasang event listener untuk setiap tombol
        foreach (var mapping in buttonSceneMappings)
        {
            if (mapping.button != null && !string.IsNullOrEmpty(mapping.sceneName))
            {
                string sceneToLoad = mapping.sceneName; // Variabel lokal untuk memastikan nilai tetap
                mapping.button.onClick.AddListener(() => ChangeScene(sceneToLoad));
            }
        }
    }

    // Fungsi untuk mengganti scene
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
