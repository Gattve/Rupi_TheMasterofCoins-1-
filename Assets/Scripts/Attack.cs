using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack
{
    [SerializeField] private string name;
    [SerializeField] private int damage;
    [SerializeField] private Sprite icon;
    [SerializeField] private float speed;
    [SerializeField] private float cooldown; // waktu cooldown dalam detik
    private float lastUsedTime; // waktu terakhir attack digunakan

    public string Name => name;
    public int Damage => Mathf.Max(damage, 0); // Pastikan damage tidak negatif
    public Sprite Icon => icon;
    public float Speed => speed;
    public float Cooldown => Mathf.Max(cooldown, 0); // Pastikan cooldown tidak negatif
    public float LastUsedTime => lastUsedTime;

    public bool IsReady() => Time.time >= lastUsedTime + cooldown;

    public float CooldownRemaining() => Mathf.Max(0, (lastUsedTime + cooldown) - Time.time);

    public void Use()
    {
        lastUsedTime = Time.time;
    }
}
