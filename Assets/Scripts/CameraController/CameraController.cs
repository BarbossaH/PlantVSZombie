using System;
using System.Collections;
using UnityEngine;

namespace CameraController
{
    public class MainCamera : MonoBehaviour
    {
        private Vector3 originalPosition;
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private float moveDuration=1.5f;
        private void Start()
        {
            originalPosition = transform.position;
            // Debug.Log(originalPosition);
        }

        public void MoveCamera(Action callback)
        {
            StartCoroutine(MoveCameraRoutine(callback));
        }

        private IEnumerator MoveCameraRoutine(Action callback)
        {
            // Move to target position
            yield return MoveBetweenPositions(originalPosition, targetPosition, moveDuration);

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Move back to the original position
            yield return MoveBetweenPositions(targetPosition, originalPosition, moveDuration);

            // Invoke callback
            callback?.Invoke();
        }

        private IEnumerator MoveBetweenPositions(Vector3 startPosition, Vector3 endPosition, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
                yield return null;
            }
        }

        // private IEnumerator MoveCameraRoutine(Action callback)
        // {
        //     float speed = 0f;
        //     Vector3 currentPosition = originalPosition;
        //     while (speed<moveDuration)
        //     {
        //         speed += Time.deltaTime;
        //         transform.position =Vector3.Lerp(currentPosition,targetPosition,speed/moveDuration);
        //         yield return null;
        //         
        //     }
        //     speed = 0f;
        //     yield return new WaitForSeconds(1f);
        //     currentPosition = transform.position;
        //     while (speed<moveDuration)
        //     {
        //         speed += Time.deltaTime;
        //         transform.position =Vector3.Lerp(currentPosition,originalPosition,speed/moveDuration);
        //         yield return null;
        //     }
        //     callback?.Invoke();
        // }
    }
}