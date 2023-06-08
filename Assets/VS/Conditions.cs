using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace VS
{

    public class ConditionCategory
    {
        [HideInInspector]
        public string title;
        public List<Condition> conditions;
    }

    [System.Serializable]
    public abstract class Condition //base level class of all conditions
    {
        [HideInInspector]
        public string title = "Name";

        [HideInInspector]
        public string category = "Custom";

        [SerializeReference]
        public List<Action> actions;

        public abstract void Process();
    }
    
}


namespace VS.Conditions
{

    public class TriggerEnter : Condition
    {
        public Collider a;
        public Collider b;

        public override void Process()
        {
            void ProcessActions()
            {
                foreach (Action action in actions)
                    action.Process();
            }

            if (Physics.ComputePenetration(
                a, a.transform.position, a.transform.rotation, 
                b, b.transform.position, b.transform.rotation, 
                out _, out _))
            {
                ProcessActions();
            }

        }

        public TriggerEnter() { category = "Trigger"; }
    }

    public class Start : Condition
    {
        public override void Process()
        {

        }

        public Start() { category = "Event"; }
    }

    public class Update : Condition
    {
        public override void Process()
        {

        }

        public Update() { category = "Event"; }
    }
}