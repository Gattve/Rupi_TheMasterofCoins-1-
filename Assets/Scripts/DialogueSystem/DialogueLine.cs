using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;

        [Header ("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;
        
        [Header ("Time Parameters")]
        [SerializeField] private float delay = 0.01f;
        [SerializeField] private float delayBetweenLine;

        [Header ("Sound")]
        [SerializeField] private AudioClip sound;

        [Header ("Character Image")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        private void Awake()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";

            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void Start()
        {
            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delay, sound, delayBetweenLine));
        }
        
    }
}

