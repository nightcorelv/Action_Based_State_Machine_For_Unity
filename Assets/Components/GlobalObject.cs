using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class GlobalObject : MonoBehaviour
    {
        private static List<Components.GlobalObject> gameObjects = new List<Components.GlobalObject>();

        GlobalObject() => gameObjects.Add(this);

        public static void Setup()
        {
            if(gameObjects.Count > 0)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i] != null)
                        UnityEngine.Object.DontDestroyOnLoad(gameObjects[i].gameObject);
                    else
                        gameObjects.Remove(gameObjects[i--]);
                }
            }
        }
    }
}