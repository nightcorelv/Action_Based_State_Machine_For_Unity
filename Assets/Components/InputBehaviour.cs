using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class InputBehaviour : MonoBehaviour
    {
        public static Components.InputBehaviour instance = null;

        public static float horizontal, vertical;

        public static bool anyKeyDown;
        public static bool
            enter,
            cancel,

            jump,
            crouch,
            attack,
            block,

            mouseLeft,
            mouseRight;

        public static UnityEngine.Vector3 mousePosition;


        //[Header("Animator Parameter")]
        //public string horizontal = "Horizontal";
        //public string vertical = "Vertical";

        //[Space(10)]
        //public string isGround = "isGround";

        //[Space(10)]
        //public string enter = "Enter";
        //public string cancel = "Cancel";

        //[Space(10)]
        //public string jump = "Jump";
        //public string crouch = "Crouch";

        //[Space(10)]
        //public string attack = "Attack";
        //public string block = "Block";


        InputBehaviour() => instance = this;

        public static void Setup()
        {

        }
        public static void Process()
        {
            mousePosition = UnityEngine.Input.mousePosition;

            if (UnityEngine.Input.anyKeyDown)
            {
                anyKeyDown = true;

                if (UnityEngine.Input.GetButtonDown("Enter"))
                    enter = true;
                if (UnityEngine.Input.GetButtonDown("Cancel"))
                    cancel = true;
                if (UnityEngine.Input.GetButtonDown("Jump"))
                    jump = true;
                if (UnityEngine.Input.GetButtonDown("Crouch"))
                    crouch = true;

                if (UnityEngine.Input.GetButtonDown("Attack"))
                    attack = true;
                if (UnityEngine.Input.GetButtonDown("Block"))
                    block = true;

                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse0))
                    mouseLeft = true;
                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse1))
                    mouseRight = true;
            }


        }

        public static void Reload()
        {
            if (anyKeyDown)
            {
                anyKeyDown = false;

                if (enter)
                    enter = false;
                if (cancel)
                    cancel = false;
                if (jump)
                    jump = false;
                if (crouch)
                    crouch = false;

                if (attack)
                    attack = true;
                if (block)
                    block = true;

                if (mouseLeft)
                    mouseLeft = false;
                if (mouseRight)
                    mouseRight = false;
            }

        }
    }
}

