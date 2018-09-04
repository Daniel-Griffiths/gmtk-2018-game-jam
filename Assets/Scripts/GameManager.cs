using Kino;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Gameobjects
    public GameObject menu;
    public GameObject enemy;
    public GameObject boss;
    public GameObject mainCamera;

    // Misc
    private int score = 0;
    private float timeSlow = 0.5f;
    public bool gameHasEnded = false;
    private float restartDelay = 1.5f;
    
    // Audio
    public AudioClip deathSound;
    private AudioSource audioSource;
    public AudioClip announcerStartSound;
    
    // GUI
    public Text scoreText;

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

            // spawn a boss every 20 enemies
            if (score > 0 && (score % 20) == 0) {
                GameObject newEnemy = Instantiate(boss);
                newEnemy.transform.position = new Vector2(Random.Range
                 (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x), 1f);
            } else {
                GameObject newEnemy = Instantiate(enemy);
                newEnemy.transform.position = new Vector2(Random.Range
                 (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x), 1f);
            }
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
