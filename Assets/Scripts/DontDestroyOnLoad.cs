using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Membuat GameObject tetap ada
        }
        else
        {
            Destroy(gameObject); // Hapus GameObject baru jika sudah ada instance
        }
    }
}
