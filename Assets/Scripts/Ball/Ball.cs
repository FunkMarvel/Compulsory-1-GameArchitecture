// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: Ball.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 05/09/2023
// //Last Modified On : 29/09/2023
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
        public int Score { get; set; }
        public int Lives { get; set; }

        private void Awake()
        {
            RigidBody = gameObject.GetComponent<Rigidbody>();
            Lives = 3;
        }

        private void FixedUpdate()
        {
            RigidBody.velocity = Vector3.ClampMagnitude(RigidBody.velocity, 100f);
            if (RigidBody.position.magnitude > 100) transform.position = Vector3.zero;
        }
    }
}