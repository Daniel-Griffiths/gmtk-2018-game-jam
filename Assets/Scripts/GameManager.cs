using Kino;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float timeSlow = 0.5f;
    public GameObject mainCamera;
    private float restartDelay = 1.5f;
    public bool gameHasEnded = false;
    public GameObject menu;
    private AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip announcerStartSound;
    public GameObject enemy;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(announcerStartSound);
    }

    void FixedUpdate()
    {
        if (audioSource.pitch > .2f && gameHasEnded == true) {
            audioSource.pitch -= 0.02f;
        }

        if (Input.GetKeyDown("escape")) {
            GameQuit();
        }

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 3) {
            Instantiate(enemy);
        }
    }

    public void GameOver()
    {
        if (gameHasEnded == false) {
            gameHasEnded = true;
            audioSource.PlayOneShot(deathSound);
            Time.timeScale = timeSlow;
            mainCamera.GetComponent<AnalogGlitch>().enabled = true;
            mainCamera.GetComponent<DigitalGlitch>().enabled = true;
            Invoke("ShowMenu", restartDelay);
        }
    }

    public void GameRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    void ShowMenu()
    {
        menu.SetActive(true);
    }
}
