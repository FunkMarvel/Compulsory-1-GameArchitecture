// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: BallBondage.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 28/09/2023
// //Last Modified On : 28/09/2023
// //Copy Rights : Anders P. Åsbø
// //Description :
// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////

using UnityEngine;

namespace Ball
{
    public class BallBondage : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Ball")) return;

            var rigidBall = other.gameObject.GetComponent<Ball>();
            rigidBall.RigidBody.velocity = -rigidBall.RigidBody.velocity;
        }
    }
}