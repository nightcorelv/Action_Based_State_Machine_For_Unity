using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public static Components.PlayerBehaviour instance = null;

        //inspector
        public float moveSpeed = 1;

        PlayerBehaviour() => instance = this;

        public static void Setup()
        {

        }
        public static void Process()
        {

        }
        public static void Move()
        {
            if (Components.InputBehaviour.horizontal != 0 || Components.InputBehaviour.vertical != 0)
            {
                instance.transform.rotation = Quaternion.Euler(
                    instance.transform.rotation.x,
                    Components.CameraBehaviour.instance.transform.eulerAngles.y,
                    instance.transform.rotation.z);

                instance.transform.Translate(
                    Components.InputBehaviour.horizontal * Time.deltaTime * instance.moveSpeed,
                    0,
                    Components.InputBehaviour.vertical * Time.deltaTime * instance.moveSpeed,
                    Space.Self);
            }
        }
    }
}

