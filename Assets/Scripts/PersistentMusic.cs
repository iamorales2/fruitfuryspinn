using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    void Awake()
    {
        // If an instance already exists, destroy this duplicate
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Assign the instance and make it persistent
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Check if the active scene is the game scene
        if (SceneManager.GetActiveScene().name == "SampleScene") // Replace "GameScene" with your actual scene name
        {
            Destroy(gameObject);
        }
    }
}