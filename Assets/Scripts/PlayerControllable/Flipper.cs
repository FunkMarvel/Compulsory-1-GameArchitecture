// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: Flipper.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 05/09/2023
// //Last Modified On : 29/09/2023
// //Copy Rights : Anders P. Åsbø
// //Description :
// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////

using UnityEngine;

namespace PlayerControllable
{
    public class Flipper : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private KeyCode triggerKey;
        [SerializeField] private AnimationCurve turningCurve;
        [SerializeField] private float maxAngle = 90f;
        [SerializeField] [Range(0f, 1f)] private float holdTime = 0.4f;

        [Header("Physics")] [SerializeField] private float impactImpulseStrength = 1;
        [SerializeField] [Min(0)] private float applyImpulseSpeedThreshold = 1f;

        [Header("Scoring")] [SerializeField] private int scoreValue = 1;

        private bool _hasAnimationCurve;
        private bool _hasRigidBody;
        private bool _hasTrigger;
        private float _prevRotation;

        private Rigidbody _rigidbody;
        private float _turnTimer = 2;

        private void Awake()
        {
            _hasAnimationCurve = turningCurve != null;
            _hasTrigger = triggerKey != KeyCode.None;

            _prevRotation = transform.rotation.eulerAngles.y;

            _rigidbody = GetComponent<Rigidbody>();
            _hasRigidBody = _rigidbody != null;
        }

        private void Update()
        {
            if (_hasTrigger && Input.GetKeyDown(triggerKey)) ResetTimer();
            if (_hasTrigger && Input.GetKey(triggerKey) && _turnTimer >= holdTime) _turnTimer = holdTime;
        }

        private void FixedUpdate()
        {
            if (!_hasTrigger && !_hasAnimationCurve) return;

            var transform1 = transform;
            var rotationAngle = maxAngle * turningCurve.Evaluate(_turnTimer);

            var parent = transform1.parent;

            if (_hasRigidBody)
            {
                var rot = Quaternion.AngleAxis(rotationAngle - _prevRotation, parent.forward);

                var position = parent.position;
                _rigidbody.MovePosition(rot * (_rigidbody.position - position) + position);
                _rigidbody.MoveRotation(_rigidbody.rotation * rot);
            }

            // _rigidbody.AddTorque(transform1.parent.forward, ForceMode.Acceleration);

            _prevRotation = rotationAngle;

            _turnTimer += Time.fixedDeltaTime;
            if (_turnTimer > 2) _turnTimer = 1.1f;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ball") || _turnTimer > 1f) return;

            var rigidBall = other.gameObject.GetComponent<Ball.Ball>();
            var up = transform.up;

            var impulse = Mathf.Abs(Vector3.Dot(up, rigidBall.RigidBody.velocity));
            impulse = impulse < applyImpulseSpeedThreshold ? 0f : impulse;
            impulse = _turnTimer < holdTime ? impulse : 0f;

            var impulseVec = up * impactImpulseStrength * (impulse > 0.5f ? impulse : 0f);
            Vector3.ClampMagnitude(impulseVec, impactImpulseStrength);
            if (impulse > 0.5f) rigidBall.Score += scoreValue;

            rigidBall.RigidBody.AddForce(impulseVec, ForceMode.VelocityChange);
        }

        private void ResetTimer()
        {
            _turnTimer = 0;
        }
    }
}