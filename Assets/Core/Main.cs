using System.Collections.Generic;
using UnityEngine;
using Components;

namespace Core
{
    static partial class Main
    {
        //loop X times per sec 
        static byte loopPerSec = 60;


        //after unity Awake
        static void Awake()
        {
            
        }

        //before unity Start
        static void Start()
        {
            GlobalObject.Setup();
            InputBehaviour.Setup();
            CameraBehaviour.Setup();
            //Player.Setup();
        }

        //once per frame
        static void Process()
        {
            InputBehaviour.Process();
        }

        //loop loopPerSec times per sec 
        static void FixedProcess()
        {
            //Camera.Process();
            //Player.Process();
            //Player.Move();
            InputBehaviour.Reload();

        }

    }

}

