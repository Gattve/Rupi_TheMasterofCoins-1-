using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] string nextSceneName;
    public Transform playerTransform; 
    public void Interact(){
        if (DialogueManager.Instance == null) 
        {
            Debug.LogWarning("DialogueManager instance is missing.");
            return;
        }

        DialogueManager.Instance.ShowDialog(dialog, OnDialogComplete);
    }

    private void OnDialogComplete()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // Simpan posisi pemain sebelum pindah scene
            if (GameManager.Instance != null && playerTransform != null)
            {
                GameManager.Instance.SavePlayerState(playerTransform, SceneManager.GetActiveScene().name);
                Debug.Log($"Saved Player Position: {playerTransform.position}, Scene: {SceneManager.GetActiveScene().name}");
            }

            // Pindah scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
    
    void Start()
    {
        if (playerTransform == null)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player transform not found!");
        }
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
