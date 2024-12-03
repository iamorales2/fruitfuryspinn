using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Optionally, you can use Start or Update if you need to perform any initialization or checks
    
    // Method to change the scene
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}