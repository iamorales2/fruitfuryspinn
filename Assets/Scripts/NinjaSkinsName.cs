using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NinjaSkinsName : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] ninjaNames;
    public Button[] ninjaButtons;
    public TextMeshProUGUI ninjaNameText;
    // Start is called before the first frame update
    void Start()
    {
        // Assign button click listeners
        for (int i = 0; i < ninjaButtons.Length; i++)
        {
            int index = i; // Cache index to avoid closure issues
            ninjaButtons[i].onClick.AddListener(() => DisplayKatanaName(index));
        }
    }

    public void DisplayKatanaName(int index)
    {
        if (index >= 0 && index < ninjaNames.Length)
        {
            ninjaNameText.text = $"{ninjaNames[index]}";
        }
        else
        {
            Debug.LogWarning("Index out of range for katana names!");
        }
    }
}
