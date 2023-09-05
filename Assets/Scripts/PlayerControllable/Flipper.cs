// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: Flipper.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 05/09/2023
// //Last Modified On : 05/09/2023
// //Copy Rights : Anders P. Åsbø
// //Description :
// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////

using System;
using UnityEngine;

namespace PlayerControllable
{
    public class Flipper : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private KeyCode triggerKey;
        [SerializeField] private AnimationCurve turningCurve;
        [SerializeField] private float maxAngle = 90f;

        [Header("Physics")] [SerializeField] private float impactForceStrength = 1;

        [Header("Scoring")] [SerializeField] private int scorevalue = 1;

        private bool _hasAnimationCurve;
        private bool _hasTrigger;
        private float _turnTimer = 2;
        private float _prevRotation;

        private void Awake()
        {
            _hasAnimationCurve = turningCurve != null;
            _hasTrigger = triggerKey != KeyCode.None;

            _prevRotation = transform.localEulerAngles.y;
        }


        private void Update()
        {
            if (!_hasTrigger && !_hasAnimationCurve) return;

            if (Input.GetKeyDown(triggerKey)) _turnTimer = 0;

            var transform1 = transform;
            var rotationAngle = (maxAngle * turningCurve.Evaluate(_turnTimer));

            var parent = transform1.parent;
            transform1.RotateAround(parent.position, parent.up, rotationAngle-_prevRotation);
            _prevRotation = rotationAngle;

            _turnTimer += Time.deltaTime;
            if (_turnTimer > 2) _turnTimer = 1.1f;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                var up = transform.forward;
                var impulseVec = -up * Vector3.Dot( -up, other.relativeVelocity) * 2f;
                other.rigidbody.AddForce(impulseVec, ForceMode.Impulse);
            }
        }
    }
}