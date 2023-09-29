// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: CylinderBouncer.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 28/09/2023
// //Last Modified On : 28/09/2023
// //Copy Rights : Anders P. Åsbø
// //Description : Class that bounces ball normal to a cylindrical surface.
// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////

using UnityEngine;

namespace Bouncers
{
    public class CylinderBouncer : MonoBehaviour
    {
        [Header("Physics")] [SerializeField] [Min(0)]
        private float impulseStrength = 1;

        [SerializeField] private AnimationCurve scaleOnBounce;

        [Header("Score")] [SerializeField] [Min(0)]
        private int scoreValue;

        private bool _hasAnimation;

        private float _prevScale;
        private float _timer = 1.1f;

        private void Awake()
        {
            _prevScale = transform.localScale.x;
            _hasAnimation = scaleOnBounce != null;
        }

        private void LateUpdate()
        {
            if (!_hasAnimation || !(_timer < 1f)) return;

            var transformLocalScale = transform.localScale;
            transformLocalScale.x = transformLocalScale.z = (scaleOnBounce.Evaluate(_timer) + 1f) * _prevScale;
            transform.localScale = transformLocalScale;
            _timer += Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ball")) return;

            var rigidBall = other.gameObject.GetComponent<Ball.Ball>();
            var normal = (rigidBall.RigidBody.position - transform.position).normalized;
            var velocity = rigidBall.RigidBody.velocity;

            var impulse = (impulseStrength - Vector3.Dot(velocity, normal)) * normal;

            rigidBall.RigidBody.AddForce(impulse, ForceMode.VelocityChange);
            if (impulse.sqrMagnitude > 0f) rigidBall.Score += scoreValue;

            _timer = 0f;
        }
    }
}