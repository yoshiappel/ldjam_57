using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class BtnAnim : MonoBehaviour
    {
        [SerializeField] Vector2 startScale = new Vector2(1.0f, 1.0f);
        [SerializeField] Vector2 desiredScale = new Vector2(1.1f, 1.1f);
        [SerializeField] float animSpeed = .2f;

        [SerializeField] RectTransform rectTransform;

        public void SetStartScale()
        {
            rectTransform.LeanScale(startScale, animSpeed);
        }

        public void SetDesiredScale()
        {
            rectTransform.LeanScale(desiredScale, animSpeed);
        }
    }
}