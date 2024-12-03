using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect mouse click or touch
        {
            Debug.Log("Screen pressed!");
            SceneManager.LoadScene("MainMenu");
        }
    }
}