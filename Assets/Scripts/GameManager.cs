using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Text tCoin;
    [SerializeField] private Stat healthUI;
    [SerializeField] private WoodManager woodManager;

    private int coinCount;
    private int playerHealth = 100;
    public Vector3 lastPlayerPosition; // Menyimpan posisi terakhir pemain
    public string lastScene; 

    public int CoinCount
    {
        get => coinCount;
        private set
        {
            coinCount = value;
            UpdateCoinUI();
        }
    }

    public int PlayerHealth
    {
        get => playerHealth;
        set
        {
            playerHealth = Mathf.Clamp(value, 0, 100);
            UpdateHealthUI();

            if (playerHealth <= 0)
            {
                Debug.Log("Player died.");
                // Tambahkan logika game over
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerState(Transform playerTransform, string currentScene)
    {
        lastPlayerPosition = playerTransform.position;
        lastScene = currentScene;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene {scene.name} loaded.");

        FindAndAssignUI();

        if (scene.name == "MiniGame_Kayu")
        {
            FindAndAssignWoodManager();
        }

        if (scene.name == lastScene && lastPlayerPosition != Vector3.zero)
        {
            // Restor posisi pemain setelah scene dimuat
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = lastPlayerPosition;
                Debug.Log($"Restored Player Position: {player.transform.position}");
            } else
            {
                Debug.LogWarning("Player object not found after scene load.");
            }
        }
    }

    private void FindAndAssignUI()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            Debug.Log("Canvas found in scene.");
            GameObject frame = GameObject.Find("Frame");
            Debug.Log("Frame found in scene.");
            tCoin = frame.transform.Find("coin")?.GetComponent<Text>();

            GameObject healthBar = GameObject.Find("HealthBar");
            healthUI = healthBar.transform.Find("Health")?.GetComponent<Stat>();

            if (tCoin == null)
                Debug.LogWarning("Coins UI element not found!");
            if (healthUI == null)
                Debug.LogWarning("Health UI element not found!");
            else
            {
                UpdateCoinUI();
                UpdateHealthUI();
            }
        }
        else
        {
            Debug.LogWarning("Canvas not found in the new scene!");
        }
    }

    private void FindAndAssignWoodManager()
    {
        WoodManager manager = FindObjectOfType<WoodManager>();
        if (manager != null)
        {
            woodManager = manager;
            Debug.Log("WoodManager assigned successfully.");
        }
        else
        {
            Debug.LogWarning("WoodManager not found in the scene.");
        }
    }

    // private void Start()
    // {
    //     UpdateCoinUI();
    //     UpdateHealthUI();
    // }

    public void CollectCoin(int amount)
    {
        CoinCount += amount;
    }

    public void CollectHeart()
    {
        PlayerHealth += 10;
    }

    public void CollectWood()
    {
        woodManager?.CollectWood();
    }

    public void CollectHammer(){
        CollectCoin(5000);
        woodManager?.CollectHammer();
    }

    public void TakeDamage(int damage)
    {
        PlayerHealth -= damage;
    }

    public void UpdateCoinUI()
    {
        if (tCoin != null)
        {
            tCoin.text = "Coins: " + coinCount;
        }
    }

    public void UpdateHealthUI()
    {
        if (healthUI != null)
        {
            healthUI.MyCurrentValue = PlayerHealth;
        }
    }
}
