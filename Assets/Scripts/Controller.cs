using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the active scene
    }
}

