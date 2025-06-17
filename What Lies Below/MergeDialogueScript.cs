using System.Collections;
using UnityEngine;
using TMPro;

public class CombinedDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI tutorialText; // Reference to the TextMeshProUGUI component for tutorial
    public GameObject panel;              // Panel to show/hide
    public float typeSpeed = 0.05f;       // Time between each character for typewriter effect

    [Header("Dialogue Settings")]
    [TextArea(3, 10)]
    public string[] instructions = {
        "[Press TAB to skip Dialogue Actions]",
        "Welcome, Sean, its your Dad, I'm here to guide you through this ädventure.",
        "Use W A S D or arrow keys to move 🕹️",
        "Press E to shoot your gun 🔫",
        "Hover over shiny objects to collect them ✨",
        "Click the camera button to take photos 📸",
        "Remember, you're here to find the Deepstone to finish off what I couldn't do",
        "You're ready! Go explore!"
    };

    private int index = 0;
    private bool isTyping = false;

    void Start()
    {
        ShowNextInstruction();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // Instantly complete the line if it's still typing
                StopAllCoroutines();
                tutorialText.text = instructions[index];
                isTyping = false;
            }
            else
            {
                ShowNextInstruction();
            }
        }
    }

    void ShowNextInstruction()
    {
        if (index < instructions.Length)
        {
            StartCoroutine(TypeLine(instructions[index]));
            index++;
        }
        else
        {
            panel.SetActive(false); // Hide panel when done
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        tutorialText.text = "";
        foreach (char c in line.ToCharArray())
        {
            tutorialText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }
    private static CombinedDialogue instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // prevent duplicates
            return;
        }
        instance = this;
    }//destroy text after instance
}
