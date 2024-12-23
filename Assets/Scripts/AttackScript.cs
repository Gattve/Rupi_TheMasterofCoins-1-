using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private Image cooldownFill;
    [SerializeField] private Image attackIcon;
    [SerializeField] private Text attackNameText;
    [SerializeField] private Text cooldownText;

    private float cooldownDuration;
    private float cooldownTimer;
    private bool isCooldownActive;


    public void InitializeAttackUI(Sprite icon, string attackName, float cooldown)
    {
        attackIcon.sprite = icon != null ? icon : null; // Fallback if icon null
        attackNameText.text = !string.IsNullOrEmpty(attackName) ? attackName : "Unknown";
        cooldownDuration = Mathf.Max(0, cooldown);
        cooldownTimer = 0f;
        isCooldownActive = true;

        cooldownFill.fillAmount = 0f;
        cooldownText.text = $"{cooldownDuration:F1}s";
        cooldownText.enabled = true;
    }



    private void Update()
    {
        if (!isCooldownActive) return;

        cooldownTimer += Time.deltaTime;

        if (cooldownTimer < cooldownDuration)
        {
            UpdateCooldownUI();
        }
        else
        {
            EndCooldown();
        }
    }

    private void UpdateCooldownUI()
    {
        cooldownFill.fillAmount = Mathf.Clamp01(cooldownTimer / cooldownDuration);
        cooldownText.text = $"{Mathf.Max(0, cooldownDuration - cooldownTimer):F1}s";
    }

    private void EndCooldown()
    {
        isCooldownActive = false;
        cooldownFill.fillAmount = 1f;
        cooldownText.enabled = false;
    }
}
