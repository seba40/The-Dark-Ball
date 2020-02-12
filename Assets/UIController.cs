using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    
    public void StartGame()
    {
        Debug.Log("Game started");
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Debug.Log("You quit");
        Application.Quit();
    }
        
}
