// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: BouncePad.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 28/09/2023
// //Last Modified On : 29/09/2023
// //Copy Rights : Anders P. Åsbø
// //Description :
// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////

using UnityEngine;

public class BouncePad : MonoBehaviour
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
        _prevScale = transform.localScale.y;
        _hasAnimation = scaleOnBounce != null;
    }

    private void LateUpdate()
    {
        if (!_hasAnimation || !(_timer < 1f)) return;

        var transformLocalScale = transform.localScale;
        transformLocalScale.y = (scaleOnBounce.Evaluate(_timer) + 1f) * _prevScale;
        transform.localScale = transformLocalScale;
        _timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;

        var rigidBall = other.gameObject.GetComponent<Ball.Ball>();

        var transform1 = transform;

        var normal = transform1.up;
        var diffVec = rigidBall.RigidBody.position - transform1.position;
        var velocity = rigidBall.RigidBody.velocity;

        if (Vector3.Dot(normal, diffVec) < 0f) return;

        var impulse = (impulseStrength - Vector3.Dot(velocity, normal)) * normal;

        rigidBall.RigidBody.AddForce(impulse, ForceMode.VelocityChange);

        _timer = 0f;
    }
}