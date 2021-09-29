using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject gameOverPanel;
    
    private bool m_Started = false;
    private int m_Points;
    
    //private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        string _highScoreName = GameManager.gm.highScoreName;
        bestScoreText.text = $"Best Score : {_highScoreName} : {GameManager.gm.highScore}";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
    }

    void AddPoint(int point)
    {
        GameManager.gm.AddScore(point);
        ScoreText.text = $"Score : {GameManager.gm.playerScore}";
    }

    public void GameOver()
    {
        int _highScore = GameManager.gm.highScore;
        int _playerScore = GameManager.gm.playerScore;

        if(_playerScore > _highScore)
        {
            GameManager.gm.SaveGame();
        }

        string _highScoreName = GameManager.gm.highScoreName;
        bestScoreText.text = $"Best Score : {_highScoreName} : {GameManager.gm.highScore}";

        gameOverPanel.SetActive(true);
    }

    public void RestartButton()
    {
        GameManager.gm.playerScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        GameManager.gm.playerScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
