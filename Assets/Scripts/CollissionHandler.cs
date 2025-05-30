using System.ComponentModel;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollissionHandler : MonoBehaviour
{
    // Variable
    [SerializeField] float levelDelay = 2f;

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
                WhenFinish(); // Kalo kena bakalan ke level selanjutnya
                break;
            default:
                WhenCrash(); // Apa yang terjadi ketika kena object diluar tag diatas
                break;
        }
    }

    void WhenFinish()
    {
        GetComponent<movement>().enabled = false; // Biar pas kena gak bakalan bisa terbang
        Invoke("LoadNextLevel", levelDelay); // Pas kena obstacle, bakalan ada delay dulu biar gak error
    }

    void WhenCrash()
    {
        GetComponent<movement>().enabled = false; // Biar pas kena keliatan rusak jadi gak terbang
        Invoke("ReloadLevel", levelDelay); // Pas kena obstacle, bakalan ada delay dulu biar gak error (mungkin)
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
        // Biar bisa load scene tanpa input angka dan balik ke scene awal banget, jadi level loop
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
