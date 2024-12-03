using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KatanaSkins : MonoBehaviour
{
    public string[] katanaNames;
    public Button[] katanaButtons;
    public TextMeshProUGUI katanaNameText;
    // Start is called before the first frame update
    void Start()
    {
        // Assign button click listeners
        for (int i = 0; i < katanaButtons.Length; i++)
        {
            int index = i; // Cache index to avoid closure issues
            katanaButtons[i].onClick.AddListener(() => DisplayKatanaName(index));
        }
    }

    public void DisplayKatanaName(int index)
    {
        if (index >= 0 && index < katanaNames.Length)
        {
            katanaNameText.text = $"{katanaNames[index]}";
        }
        else
        {
            Debug.LogWarning("Index out of range for katana names!");
        }
    }
}
