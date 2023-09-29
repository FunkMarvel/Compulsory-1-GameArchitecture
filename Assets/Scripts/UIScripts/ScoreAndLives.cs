// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: ScoreAndLives.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 28/09/2023
// //Last Modified On : 29/09/2023
// //Copy Rights : Anders P. Åsbø
// //Description :
// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class ScoreAndLives : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text livesText;
        [SerializeField] private TMP_Text gameOverText;
        [SerializeField] private TMP_Text controlsPromptText;
        [SerializeField] private GameObject restartButton;
        [SerializeField] private GameObject exitButton;
        [SerializeField] private GameObject ballObject;
        [SerializeField] private GameObject spawner;
        [SerializeField] private string mainMenuScene;

        private Ball.Ball _ball;
        private bool _gameOver;
        private bool _hasBall;
        private bool _hasSpawner;
        private SpawnSystem.SpawnSystem _spawner;

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

        private void Start()
        {
            controlsPromptText.text = "Controls:\n'A' and 'D'";
            StartCoroutine(ExecuteAfterTime(5));
        }

        private void LateUpdate()
        {
            if (_gameOver) return;
            scoreText.text = $"Score: {_ball.Score}";
            livesText.text = $"Lives: {_ball.Lives}";
        }

        private IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            controlsPromptText.text = "";
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
    }
}