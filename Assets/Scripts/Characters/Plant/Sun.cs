using Conf;
using System.Collections;
using UnityEngine;
using Managers;
using UI;
using Random = UnityEngine.Random;

namespace Characters.Plant
{
    //in real project, I believe this sun should have a base class, because there are many different kinds of suns that have different attributes and functionalities. But in this project, I just simplified it. 
    public class Sun : MonoBehaviour
    {
        [SerializeField] private float fallingSpeed;
        [SerializeField] private float jumpUpSpeed;
        [SerializeField] private float jumpDownSpeed;
        [SerializeField] private float flyingSpeed;
        private float landingPosY;
        private Coroutine countdownCoroutine;
       private Camera cameraMain;
       private Vector3 localPosition;
       private void Awake()
       {
           cameraMain = Camera.main;
           localPosition = transform.position;
       }

       private void OnMouseDown()
        {
            //actually I believe we should destroy the clicked sun and create a new one, create a new one first, then destroy the old one
            //TODO: generate a new one and destroy the clicked one
            Vector3 targetScreenPos = UICardGroup.Instance.GetSunTMPScreenPos();
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
                yield return null;
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

                yield return null;

            }

            countdownCoroutine = StartCoroutine(DestroyCountDown(5f));
        }

        private void DestroySelf()
        {
            // Destroy(gameObject);
            StopAllCoroutines();
            transform.position = localPosition;
            ObjectPoolManager.Instance.ReturnObject(PoolTypeEnum.Sun,gameObject);
        }

        private void SunCollectedAnim(Vector3 destinationPos)
        {
            StartCoroutine(SunFlying(destinationPos));
        }

        private IEnumerator SunFlying(Vector3 destinationPos)
        {
            //get the direction of destination
            if (cameraMain is null) yield break; 
            Vector3 screenStartPos = cameraMain.WorldToScreenPoint(transform.position);

            // Ensure destination position includes correct Z depth
            //destinationPos.z = screenStartPos.z;

            while (Vector3.Distance(destinationPos, screenStartPos) > 0.1f)
            {
                yield return null;
                screenStartPos = Vector3.MoveTowards(screenStartPos, destinationPos, flyingSpeed);

                transform.position = cameraMain.ScreenToWorldPoint(screenStartPos);
            }

            PlayerManager.Instance.SunAmount += (int)SunTypeEnum.Normal;

            DestroySelf();
        }

        private IEnumerator DestroyCountDown(float duration)
        {
            yield return new WaitForSeconds(duration);
            DestroySelf();
        }
    }
}
