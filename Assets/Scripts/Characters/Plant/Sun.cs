namespace Characters.Plant
{

    using System.Collections;
    using UnityEngine;
    using Managers;
    public class Sun : MonoBehaviour
    {
        [SerializeField] private float fallingSpeed = 5.0f;
        [SerializeField] private float jumpUpSpeed = 10.0f;
        [SerializeField] private float jumpDownSpeed = -2.5f;
        [SerializeField] private float flyingSpeed = 10f;
        private float landingPosY;
        private Coroutine countdownCoroutine;

        private void OnMouseDown()
        {
            //actually I believe we should destroy the clicked sun and create a new one, create a new one first, then destroy the old one
            //TODO: generate a new one and destroy the clicked one
            Vector3 targetScreenPos = UIManager.Instance.GetSunTMPScreenPos();
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
            }

            SunCollectedAnim(targetScreenPos);
        }

        public void InitPosForSky(float landPosY, float createPosX, float createPosY)
        {
            this.landingPosY = landPosY;
            transform.position = new Vector2(createPosX, createPosY);
        }

        public void SunFallingFromSky()
        {
            FallingSunCoroutine();
        }

        private void FallingSunCoroutine()
        {
            StartCoroutine(FallingAnim());
        }

        private IEnumerator FallingAnim()
        {
            while (transform.position.y > landingPosY)
            {
                transform.Translate(fallingSpeed *  Time.deltaTime*Vector3.down);
                yield return new WaitForSeconds(0.005f);
            }

            countdownCoroutine = StartCoroutine(DestroyCountDown(5f));

        }

        public void JumpAnimationFromFlower()
        {
            JumpCoroutine();
        }

        private void JumpCoroutine()
        {
            StartCoroutine(JumpAnim());
        }

        private readonly float  jumpMaxY = 1.5f;

        private IEnumerator JumpAnim()
        {
            float speedX = Random.Range(-3f, 3f);
            float speedY = jumpUpSpeed;
            float startY = transform.position.y;
            while (transform.position.y >= startY)
            {
                if (transform.position.y >= startY + jumpMaxY)
                {
                    speedY = jumpDownSpeed;
                }

                transform.Translate(new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, 0));

                yield return new WaitForSeconds(0.005f);

            }

            countdownCoroutine = StartCoroutine(DestroyCountDown(5f));
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void SunCollectedAnim(Vector3 destinationPos)
        {
            StartCoroutine(SunFlying(destinationPos));
        }

        private IEnumerator SunFlying(Vector3 destinationPos)
        {
            //get the direction of destination
            Vector3 screenStartPos = Camera.main.WorldToScreenPoint(transform.position);

            // Ensure destination position includes correct Z depth
            //destinationPos.z = screenStartPos.z;

            while (Vector3.Distance(destinationPos, screenStartPos) > 0.1f)
            {
                yield return new WaitForSeconds(0.005f);
                screenStartPos = Vector3.MoveTowards(screenStartPos, destinationPos, flyingSpeed);

                transform.position = Camera.main.ScreenToWorldPoint(screenStartPos);
            }

            PlayerManager.Instance.SunAmount += 50;

            DestroySelf();
        }

        private IEnumerator DestroyCountDown(float duration)
        {
            yield return new WaitForSeconds(duration);
            DestroySelf();
        }
    }
}
