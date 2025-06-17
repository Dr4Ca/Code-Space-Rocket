using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollissionHandler : MonoBehaviour
{
    // komponen
    AudioSource audioSource;

    // Parameter
    [SerializeField] float levelDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip finishSFX;
    [SerializeField] ParticleSystem crashVFX;

    bool isControlAble = true;
    bool isCollideAble = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKey();
    }

    // Cheat buat umlimited next level sama matiin collide
    void RespondToDebugKey()
    {
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollideAble = !isCollideAble;
        }
    }

    // Kalo namanya beda gak bakalan bisa, jangan dicoba, udah dicoba
    private void OnCollisionEnter(Collision collision)
    {
        if (!isControlAble || !isCollideAble) { return; } // Kalo gak true, balik lagi sampe true

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

    void WhenCrash()
    {
        isControlAble = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashVFX.Play();
        GetComponent<movement>().enabled = false; // Biar pas collide, player gak bisa terbang lagi.
        Invoke("ReloadLevel", levelDelay); // Biar ada jeda pas collide / Finish
    }

    void WhenFinish()
    {
        isControlAble = false;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSFX);
        GetComponent<movement>().enabled = false;
        Invoke("LoadNextLevel", levelDelay);
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
