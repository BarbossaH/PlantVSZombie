
namespace HouseBorder
{

    using UnityEngine;

    public class BorderDetector : MonoBehaviour
    {
        // private readonly float detectionRadius=15;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log("Zombie has entered the house");

            if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie"))
            {
                Debug.Log("Zombie has entered the house");
            }
        }

        // private void OnDrawGizmosSelected()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawLine(transform.position,transform.position+ Vector3.right*detectionRadius);
        //     Gizmos.DrawWireSphere(transform.position+ Vector3.right*detectionRadius,0.2f);
        // }
    }
}