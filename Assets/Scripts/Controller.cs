using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private bool death = false;
    public void YouDied() 
    {
        if(death == false) // Checks if the player has died or not
        {
            death = true;
            Debug.Log("You Died");
            Invoke("Reload", 3); // calls Reload function after a certain amount of time
        }
    }
    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the active scenen
    }
   
}

