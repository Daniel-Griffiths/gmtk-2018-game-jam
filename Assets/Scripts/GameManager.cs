using Kino;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Text scoreText;
    private int score = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAudio(announcerStartSound);
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
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.position = new Vector2(Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x), 1f);

        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Enemies Destroyed: " + score.ToString();
    }

    public void PlayAudio(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
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
