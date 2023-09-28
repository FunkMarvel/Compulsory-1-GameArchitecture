// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: ScoreAndLives.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 28/09/2023
// //Last Modified On : 28/09/2023
// //Copy Rights : Anders P. Åsbø
// //Description :
// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIScripts
{
    public class ScoreAndLives : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text livesText;
        [SerializeField] private TMP_Text gameOverText;
        [SerializeField] private GameObject restartButton;
        [SerializeField] private GameObject exitButton;
        [SerializeField] private GameObject ballObject;
        [SerializeField] private GameObject spawner;

        private Ball.Ball _ball;
        private SpawnSystem.SpawnSystem _spawner;
        private bool _hasBall;
        private bool _hasSpawner;
        private bool _gameOver;
        [SerializeField] private string mainMenuScene;

        private void Awake()
        {
            _hasBall = ballObject != null;

            if (_hasBall)
            {
                _ball = ballObject.GetComponent<Ball.Ball>();
                _hasBall = _ball != null; 
            }

            _hasSpawner = spawner != null;

            if (_hasSpawner)
            {
                _spawner = spawner.GetComponent<SpawnSystem.SpawnSystem>();
                _hasSpawner = _spawner != null;
                if (_hasSpawner) _spawner.gameOver.AddListener(OnGameOver);
            }

            gameOverText.text = "";
            restartButton.SetActive(false);
            exitButton.SetActive(false);
        }

        public void OnGameOver()
        {
            Debug.Log("Callbacked");
            gameOverText.text = $"Game Over\nFinal score: {_ball.Score}";
            scoreText.text = livesText.text = "";
            
            restartButton.SetActive(true);
            exitButton.SetActive(true);
            
            _gameOver = true;
        }

        public void OnExit()
        {
            SceneManager.LoadScene(mainMenuScene);
        }

        public void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void LateUpdate()
        {
            if (_gameOver) return;
            scoreText.text = $"Score: {_ball.Score}";
            livesText.text = $"Lives: {_ball.Lives}";
        }
    }
}