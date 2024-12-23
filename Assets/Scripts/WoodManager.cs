using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WoodManager : MonoBehaviour
{
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private GameObject hammerPrefab;
    [SerializeField] private Text tWood;
    [SerializeField] private Text tHammer;
    [SerializeField] private GameObject[] trees;    
    [SerializeField] private int totalWood = 5;     
    
    private int woodCollected = 0;
    private int hammerCollected = 0;
    private Vector2 spawnAreaMin = new Vector2(-6, -1); 
    private Vector2 spawnAreaMax = new Vector2(6, 1);  

    private GameObject currentWood;
    private GameObject currentHammer;

    void Start()
    {
        if (woodPrefab == null || hammerPrefab == null || tWood == null || trees == null || trees.Length == 0)
        {
            Debug.LogError("WoodManager: One or more required fields are not assigned!");
            return;
        }
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateCoinUI();  
            GameManager.Instance.UpdateHealthUI();  
        }
        SpawnWood();
        UpdateUI();
    }

    public void CollectWood()
    {
        woodCollected++;
        UpdateUI();

        if (currentWood != null)
        {
            Destroy(currentWood);
        }

        if (woodCollected < totalWood)
        {
            SpawnWood(); 
        }
        else
        {
            AllWoodCollected();
        }
    }

    public void CollectHammer(){
        hammerCollected++;
        UpdateUI();

        if (currentHammer != null)
        {
            Destroy(currentHammer);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameManager.Instance.SavePlayerState(player.transform, "Level_1");
            Debug.Log($"Saved Player Position: {player.transform.position}");
        }
        else
        {
            Debug.LogWarning("Player object not found when collecting hammer.");
        }
        
        SceneManager.LoadScene("Level_1");
    }
    private void SpawnWood()
    {
        Vector2 spawnPosition;
        int maxAttempts = 100;
        int attempts = 0;
        bool validPosition;

        do
        {
            attempts++;
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector2(x, y);

            validPosition = true;

            foreach (GameObject tree in trees)
            {
                if (Vector2.Distance(spawnPosition, tree.transform.position) < 1.0f)
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
            {
                currentWood = Instantiate(woodPrefab, spawnPosition, Quaternion.identity);
                currentWood.tag = "Wood";
                return;
            }

        } while (attempts < maxAttempts);

        Debug.LogWarning("WoodManager: Failed to find a valid spawn position for wood.");
    }
    
    void UpdateUI()
    {
        if (tWood != null)
        {
            tWood.text = $"{woodCollected}/{totalWood}";
        }
        else
        {
            Debug.LogWarning("WoodManager: tWood is not assigned.");
        }

        if (tHammer != null)
        {
            tHammer.text = $"{hammerCollected}/1";
        }
    }

    private void AllWoodCollected()
    {
        Debug.Log("All wood collected!");

        Vector2 hammerPosition = new Vector2(0, 0);
        currentHammer = Instantiate(hammerPrefab, hammerPosition, Quaternion.identity);
    }
}
