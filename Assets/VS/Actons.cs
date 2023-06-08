using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace VS
{
    public class ActionCategory
    {
        [HideInInspector]
        public string title;
        public List<Action> actions;
    }

    /// <summary>
    /// base level class of all Actions
    /// </summary>
    [System.Serializable]
    public abstract class Action 
    {
        [HideInInspector]
        public string title = "Name";

        /// <summary>
        /// action category in editor
        /// </summary>
        [HideInInspector]
        public string category = "Custom";

        [HideInInspector]
        public bool finished = false;

        [HideInInspector]
        public bool run = false;

        /// <summary>
        /// abstract enter function
        /// </summary>
        public abstract void Process();

        public void Finish()
        {
            finished = true;
        }

    }
}

namespace VS.Actions
{
    public class If : Action
    {
        public Object a;

        public enum Operation
        {
            Equal,
            NotEqual,
        }
        public Operation operation;
        public Object b;

        [SerializeReference]
        public List<Action> actions;

        public override void Process()
        {
            void ProcessActions()
            {
                foreach (Action action in actions)
                    action.Process();
            }

            switch(operation)
            {
                case Operation.Equal:
                    if (a == b)
                        ProcessActions();
                    break;
                case Operation.NotEqual:
                    if (a != b)
                        ProcessActions();
                    break;
            }
        }

        public If() { category = "Compare"; }
    }

    public class Wait : Action
    {
        public float seconds = 2.0f;
        private float timer = 0.0f;

        public override void Process()
        {
            timer += Time.deltaTime;
            if (timer >= seconds)
            {
                Finish();
            }
        }

        public Wait() { category = "Time"; }
    }

    public class DestroyGameObject : Action
    {
        public GameObject gameObject;

        public override void Process()
        {
            Object.Destroy(gameObject);
            Finish();
        }

        public DestroyGameObject() { category = "GameObject"; }
    }

}
