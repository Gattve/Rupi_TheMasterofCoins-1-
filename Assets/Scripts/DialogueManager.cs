using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] Button bNext;
    private string nextSceneName; 


    public static DialogueManager Instance { get; private set;}

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetNextScene(string sceneName) {
        nextSceneName = sceneName;  
    }

    public void ShowDialog(Dialog dialog, System.Action onDialogComplete = null){
        if (dialogBox != null)
        {
            dialogBox.SetActive(true);
            StartCoroutine(TypeAllDialog(dialog.Lines, onDialogComplete));
        }
        else
        {
            Debug.LogWarning("Dialogue Box is missing.");
        }
    }

    public IEnumerator TypeDialog(string line){
        dialogText.text = "";
        Debug.Log("Start typing line: " + line);
        foreach (var letter in line.ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        Debug.Log("Finished typing line");
        bNext.gameObject.SetActive(true);
    }

    private IEnumerator TypeAllDialog(List<string> lines, System.Action onDialogComplete = null)
    {
        foreach (var line in lines)
        {
            yield return StartCoroutine(TypeDialog(line));

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            bNext.gameObject.SetActive(false);
        }

        // if (!string.IsNullOrEmpty(nextSceneName))
        // {
            dialogBox.SetActive(false);
        //     SceneManager.LoadScene(nextSceneName);
        // }
        onDialogComplete?.Invoke();
    }

    private void OnNextButtonClicked()
    {
        bNext.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        bNext.gameObject.SetActive(false);
        bNext.onClick.AddListener(OnNextButtonClicked);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetButtonDown("Submit") && bNext.gameObject.activeSelf)
    // {
    //     OnNextButtonClicked();  // Fungsi untuk melanjutkan dialog
    // }
    // }
}
