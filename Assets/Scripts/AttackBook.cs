using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBook : MonoBehaviour
{
    [SerializeField] private Text attackName;
    [SerializeField] private Attack[] attacks;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject attackBar;

    private Coroutine fadeCoroutine;

    void Start()
    {
        if (attackBar != null)
            attackBar.SetActive(false);
        else
            Debug.LogError("Attack bar is not assigned.");
    }

    public void InitializeAttacks()
    {
        // Placeholder: Tambahkan logika inisialisasi serangan jika diperlukan
    }

    public Attack Attacking(int index)
    {
        // Validasi indeks serangan
        if (index < 0 || index >= attacks.Length)
        {
            Debug.LogError($"Index {index} is out of bounds for attacks array.");
            return null;
        }

        // Validasi serangan pada indeks yang dipilih
        if (attacks[index] == null)
        {
            Debug.LogError($"Attack at index {index} is null.");
            return null;
        }

        // Atur UI untuk serangan yang dipilih
        attackName.text = attacks[index].Name;
        icon.sprite = attacks[index].Icon;
        attackBar.SetActive(true);

        // Jika coroutine `FadeBar` sebelumnya berjalan, hentikan terlebih dahulu
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        // Mulai coroutine untuk memproses fade
        fadeCoroutine = StartCoroutine(FadeBar(attacks[index].Cooldown));

        return attacks[index];
    }

    private IEnumerator FadeBar(float cooldown)
    {
        if (attackBar == null)
        {
            Debug.LogError("Attack bar is not assigned.");
            yield break;
        }

        // Tampilkan bar selama cooldown berlangsung
        yield return new WaitForSeconds(cooldown);

        // Durasi fade-out
        float fadeDuration = 0.5f;
        float elapsedTime = 0f;

        // Ambil komponen canvas group untuk memudahkan fading
        CanvasGroup canvasGroup = attackBar.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = attackBar.AddComponent<CanvasGroup>();
        }

        // Fade out attack bar
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // Pastikan attack bar disembunyikan
        canvasGroup.alpha = 0f;
        attackBar.SetActive(false);

        // Hapus referensi coroutine setelah selesai
        fadeCoroutine = null;
    }
}
