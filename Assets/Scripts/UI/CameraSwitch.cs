using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class CameraSwitch : MonoBehaviour
    {
        [SerializeField] Camera scene1Cam;
        [SerializeField] Camera scene2Cam;
        [SerializeField] Camera scene3Cam;
        [SerializeField] Camera scene4Cam;

        public void SwitchCam(int cam)
        {
            if (cam == 1)
            {
                TurnAllCamsOff();
                scene1Cam.gameObject.SetActive(true);
            }
            if (cam == 2)
            {
                TurnAllCamsOff();
                scene2Cam.gameObject.SetActive(true);
            }
            if (cam == 3)
            {
                Debug.Log("3");
                TurnAllCamsOff();
                scene3Cam.gameObject.SetActive(true);
            }
            if (cam == 4)
            {
                TurnAllCamsOff();
                scene4Cam.gameObject.SetActive(true);
            }
        }

        private void TurnAllCamsOff()
        {
            scene1Cam.gameObject.SetActive(false);
            scene2Cam.gameObject.SetActive(false);
            scene3Cam.gameObject.SetActive(false);
            scene4Cam.gameObject.SetActive(false);
        }
    }
}