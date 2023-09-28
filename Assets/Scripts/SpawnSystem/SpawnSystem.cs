// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: SpawnSystem.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 28/09/2023
// //Last Modified On : 28/09/2023
// //Copy Rights : Anders P. Åsbø
// //Description :
// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////

using UnityEngine;
using UnityEngine.Events;

namespace SpawnSystem
{
    public class SpawnSystem : MonoBehaviour
    {
        [SerializeField] private GameObject spawnPoint;

        public UnityEvent gameOver = new();
        private bool _hasSpawnPoint;

        public Transform SpawnPoint { get; private set; }

        private void Awake()
        {
            _hasSpawnPoint = spawnPoint != null;
            if (!_hasSpawnPoint) return;

            SpawnPoint = spawnPoint.transform;
            _hasSpawnPoint = SpawnPoint != null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Ball")) return;

            var ball = other.gameObject.GetComponent<Ball.Ball>();
            if (ball == null) return;

            ball.Lives -= 1;
            if (ball.Lives > 0)
            {
                other.gameObject.transform.SetPositionAndRotation(SpawnPoint.position, Quaternion.identity);
            }
            else
            {
                gameOver.Invoke();
                Debug.Log("GameOver");
            }
        }
    }
}