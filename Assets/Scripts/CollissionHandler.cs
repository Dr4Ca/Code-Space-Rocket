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
                LoadNextLevel();
                break;
            default:
                ReloadLevel();
                break;
        }

        void LoadNextLevel()
        {
            // Biar bisa load level selanjutnya
            int nextLevel = SceneManager.GetActiveScene().buildIndex;

            // Begini biar nanti kalo level udah mentok, balik ke awal
            int loadToNextLevel = nextLevel + 1;
            if (loadToNextLevel == SceneManager.sceneCountInBuildSettings)
            {
                loadToNextLevel = 0;
            }

            // Lanjut ke level atau scene selanjutnya
            SceneManager.LoadScene(loadToNextLevel);
        }

        void ReloadLevel()
        {
            // Biar bisa load scene tanpa input angka dan balik ke scene awal banget
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
        
    }
}
