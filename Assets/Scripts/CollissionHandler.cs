using System.ComponentModel;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollissionHandler : MonoBehaviour
{
    // komponen
    AudioSource audioSource;

    // Variable
    [SerializeField] float levelDelay = 2f;
    [SerializeField] AudioClip CrashSound;
    [SerializeField] AudioClip FinishSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
                WhenFinish();
                break;
            default:
                WhenCrash();
                break;
        }
    }

    void WhenFinish()
    {
        audioSource.PlayOneShot(FinishSound);
        GetComponent<movement>().enabled = false; // Biar pas kena gak bakalan bisa terbang
        Invoke("LoadNextLevel", levelDelay); // Pas kena obstacle, bakalan ada delay dulu biar gak error
    }

    void WhenCrash()
    {
        audioSource.PlayOneShot(CrashSound);
        GetComponent<movement>().enabled = false;
        Invoke("ReloadLevel", levelDelay);
    }

    void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex;

        // Inisialisasi awal dari level loop
        int loadToNextLevel = nextLevel + 1;
        if (loadToNextLevel == SceneManager.sceneCountInBuildSettings)
        {
            loadToNextLevel = 0;
        }

        SceneManager.LoadScene(loadToNextLevel);
    }

    void ReloadLevel()
    {
        // Eksekusi Level Loop
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
