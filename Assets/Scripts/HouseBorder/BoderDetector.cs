namespace HouseBorder
{

    using UnityEngine;

    public class BorderDetector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log("Zombie has entered the house");

            if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie"))
            {
                Debug.Log("Zombie has entered the house");
            }
        }
    }
}