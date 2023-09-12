// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: Ball.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 05/09/2023
// //Last Modified On : 12/09/2023
// //Copy Rights : Anders P. Åsbø
// //Description :
// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////

using UnityEngine;

namespace Ball
{
    public class Ball : MonoBehaviour
    {
        public Rigidbody RigidBody { get; private set; }
        public Vector3 PreviousVelocity { get; private set; }

        private void Awake()
        {
            RigidBody = gameObject.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            PreviousVelocity = RigidBody.velocity;
        }

        private void LateUpdate()
        {
            PreviousVelocity = RigidBody.velocity;
        }
    }
}