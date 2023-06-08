using System.Collections.Generic;
using UnityEngine.LowLevel;
using UnityEngine;

namespace Core
{
    static partial class Main
    {
        static float intervalTime;



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void AfterAssembliesLoaded()
        {

        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void SubsystemRegistration()
        {
            PlayerLoopSystem defaultSystemRoot = PlayerLoop.GetDefaultPlayerLoop();

            //0 TimeUpdate
            //1 Initialization
            //2 EarlyUpdate
            //3 FixedUpdate
            //4 PreUpdate
            //5 Update
            //6 PreLateUpdate
            //7 PostLateUpdate

            System.Array.Resize(ref defaultSystemRoot.subSystemList[5].subSystemList, defaultSystemRoot.subSystemList[5].subSystemList.Length + 1);
            defaultSystemRoot.subSystemList[5].subSystemList[defaultSystemRoot.subSystemList[5].subSystemList.Length - 1] = new PlayerLoopSystem() { updateDelegate = Iterate };

            PlayerLoop.SetPlayerLoop(defaultSystemRoot);
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void BeforeSplashScreen()
        {

        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void BeforeSceneLoad()
        {

            if (loopPerSec == 0)
                loopPerSec = 60;
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void AfterSceneLoad()
        {

            Time.Reset();

            Awake();
            Start();

        }

        //custom Process
        static void Iterate()
        {

            intervalTime += Time.deltaTime;
            while (intervalTime >= 1.0f / loopPerSec)
            {
                intervalTime -= 1.0f / loopPerSec;

                FixedProcess();

            }
            Process();

            Time.Reset();
        }

    }

}

