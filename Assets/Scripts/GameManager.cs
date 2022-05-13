using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region PRIVATE VARIABLES
    private int maxNumLives = 3;
    private int lives;

    private int score;
    #endregion
    #region #region SINGLETON REGION
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    #region MONOBEHAVIOUR METHODS
    void Start()
    {
        lives = maxNumLives;
        //mainCamera = Camera.main;

        StartCoroutine(SpawnAsteroids());
    }
    #endregion
    #region PUBLIC METHODS
    // Lose a life.
    public void LoseLife()
    {
        lives--;

        if (lives == 0)
            Restart();
    }
    // Gain points.
    public void GainPoints(int points)
    {
        score += points;
    }
    // Restart the game.
    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
    #region PRIVATE METHODS
    // Spawn asteroids every few seconds.
    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            //SpawnAsteroid();

            yield return new WaitForSeconds(Random.Range(2f, 8f));
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}