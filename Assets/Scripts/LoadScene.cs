using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LoadScene : MonoBehaviour
{

    private RectTransform rect;
    [SerializeField] private float speed = 0;
    private Image loadbar;
    [SerializeField] private Image outline;
    [SerializeField] private Text tloading;
    [SerializeField] private Text tclick;
    private bool isLoadComplete = false;
    [SerializeField] GameStarter gameStarter;
    [SerializeField] private float blinkInterval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        loadbar = rect.GetComponent<Image>();
        loadbar.fillAmount = 0;
        tloading.text = "Loading...";
        
        tclick.gameObject.SetActive(false);

        StartCoroutine(Blinktloading());
    }

    // Update is called once per frame
    void Update()
    {
        if (loadbar.fillAmount < 1) {
            loadbar.fillAmount += Time.deltaTime*speed;
        } else {
            isLoadComplete = true;
            speed = 0;
            StartCoroutine(HideLoadBar());
        }

        if (isLoadComplete && Input.GetMouseButtonDown(0)) {
            gameStarter.StartGame();
        }
    }

    IEnumerator Blinktloading(){
        while (true) {
            tloading.enabled = !tloading.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    IEnumerator HideLoadBar(){
        yield return new WaitForSeconds(1.5f);
        
        outline.gameObject.SetActive(false);
        loadbar.gameObject.SetActive(false);
        
        StopCoroutine(Blinktloading());
        tloading.gameObject.SetActive(false);

        tclick.gameObject.SetActive(true);
    }
}
