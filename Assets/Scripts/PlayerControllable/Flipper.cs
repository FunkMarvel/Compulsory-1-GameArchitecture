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

        private void Awake()
        {
            _hasAnimationCurve = turningCurve != null;
            _hasTrigger = triggerKey != KeyCode.None;
        }


        private void Update()
        {
            if (!_hasTrigger && !_hasAnimationCurve) return;

            if (Input.GetKeyDown(triggerKey)) _turnTimer = 0;

            var transform1 = transform;
            transform.localEulerAngles = Vector3.up * (maxAngle * turningCurve.Evaluate(_turnTimer));

            _turnTimer += Time.deltaTime;
            if (_turnTimer > 2) _turnTimer = 1.1f;
        }
    }
}