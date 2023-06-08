using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class CameraBehaviour : MonoBehaviour
    {
        public static Components.CameraBehaviour instance;
        public static UnityEngine.Camera mainCamera = null;

        CameraBehaviour() => instance = this;

        public static void Setup()
        {
            //mainCamera = instance.GetComponent<UnityEngine.Camera>();

            //cinemachineBrain = instance.GetComponent<CinemachineBrain>();
            //cinemachineVirtualCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
        }
        public static void Process()
        {
            //CinemachineCore.GetInputAxis = GetAxisCustom;
        }
    }
}
