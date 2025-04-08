using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class CamShake : MonoBehaviour
    {
        public bool start;
        public AnimationCurve curve;
        public float duration = 1f;

        private void Update()
        {
            if (start)
            {
                start = false;
                StartCoroutine(Shaking());
            }
        }

        public IEnumerator Shaking()
        {
            Vector3 startPos = transform.localPosition;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float strength = curve.Evaluate(elapsedTime / duration);
                transform.localPosition = startPos + Random.insideUnitSphere * strength;
                yield return null;
            }

            transform.localPosition = startPos;
        }
    }
}
