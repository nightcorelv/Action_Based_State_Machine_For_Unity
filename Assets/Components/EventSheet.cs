using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

public class EventSheet : MonoBehaviour
{
    public string title;

#if UNITY_EDITOR
    public ReorderableList variablesRL;
#endif
    [SerializeReference]
    public List<VS.Variable> variables;


#if UNITY_EDITOR
    public ReorderableList triggersRL;
#endif
    public List<string> triggers;

#if UNITY_EDITOR
    public ReorderableList conditionsRL;
#endif
    [SerializeReference]
    public List<VS.Condition> conditions;
    
    bool allfinish = false;


    private void Awake()
    {
        if (conditions.Count > 0)
        {
            if (conditions[0].actions.Count > 0)
            {
                conditions[0].actions[0].run = true;
            }
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (allfinish)
            return;

        bool allfinished = true;
        for(int i = 0; i < conditions.Count; i++)
        {
            for(int k = 0; k< conditions[i].actions.Count; k++)
            {
                if (conditions[i].actions[k].run)
                {
                    if(!conditions[i].actions[k].finished)
                    {
                        conditions[i].actions[k].Process();
                        allfinished = false;
                    }
                    else
                    {
                        if (k + 1 < conditions[i].actions.Count)
                        {
                            conditions[i].actions[k + 1].run = true;
                        }
                    }
                }
                
            }
        }

        allfinish = allfinished;
        if(allfinish)
        {
            enabled = false;
        }

    }

}