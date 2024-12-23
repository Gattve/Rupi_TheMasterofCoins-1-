using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWindow : GameManager
{
    private static DialogueWindow instance;
    //[SerializeField]
    //private TextMeshProUGUI text;
    public static DialogueWindow MyInstance
    {
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueWindow>();
            }
            return instance;
        }
    }
}
