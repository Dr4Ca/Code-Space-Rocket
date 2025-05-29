using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollissionHandler : MonoBehaviour
{
    // Kalo namanya beda gak bakalan bisa, jangan dicoba, udah dicoba
    void OnCollisionEnter(Collision collision)
    {
        // Lebih efektif ketimbang if statement
        switch (collision.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("This is fine");
                break;
            case "Finish":
                Debug.Log("You've finished!");
                break;
            default:
                ReloadLevel();
                break;
        }
        
        void ReloadLevel()
        {
            // Biar bisa load scene lain tanpa input angka
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }
}
