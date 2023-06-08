using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using VS;

public class ActionWindow : EditorWindow
{
    List<Action> subClassList = new();

    Vector2 scrollPos;

    List<ActionCategory> actionCategories = new();

    int selected = -1;
    int preCategory = -1;
    double clickTime;
    bool anyTrue;

    List<bool> expand = new();

    public List<Action> behaviorList;

    private void OnEnable()
    {
        subClassList.Clear();
        // get all sub types
        List<System.Type> behaviorTypes = GetSubclassTypes(typeof(Action), Assembly.GetAssembly(typeof(Action)));

        //create instance for each sub types
        foreach (System.Type type in behaviorTypes)
        {
            subClassList.Add((Action)System.Activator.CreateInstance(type));
        }

        // add space and upper case to names
        Refresh();

        //collaps all categories
        if (subClassList.Count > 0)
        {
            for (int i = 0; i < actionCategories.Count; i++)
            {
                expand.Add(false);
            }
        }
    }

    private void OnGUI()
    {
        if (subClassList.Count > 0)
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, false);
            anyTrue = false;
            for (int i = 0; i < actionCategories.Count; i++)
            {
                expand[i] = GUILayout.Toggle(expand[i], actionCategories[i].title, "button");
                if (expand[i])
                {
                    for (int k = 0; k < expand.Count; k++)
                    {
                        if (expand[k])
                        {
                            expand[k] = false;
                        }
                    }
                    anyTrue = true;
                    expand[i] = true;

                    if (preCategory != i)
                    {
                        selected = -1;
                        preCategory = i;
                    }
                };

                if (expand[i])
                {
                    //Action action in actionCategories[i].actions
                    for (int k = 0; k < actionCategories[i].actions.Count; k++)
                    {
                        if (selected != k)
                        {
                            if (GUILayout.Button("    " + actionCategories[i].actions[k].title, "label"))
                            {
                                selected = k;
                                clickTime = EditorApplication.timeSinceStartup;
                            }
                        }
                        else
                        {

                            if (GUILayout.Button("    " + actionCategories[i].actions[k].title, "OL SelectedRow", GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                            {
                                if ((EditorApplication.timeSinceStartup - clickTime) < 0.2f && k == selected)
                                {
                                    Action temp = (Action)System.Activator.CreateInstance(actionCategories[i].actions[k].GetType());
                                    temp.title = actionCategories[i].actions[k].title;
                                    temp.category = actionCategories[i].actions[k].category;

                                    //Undo.RecordObject(eventSheet, "Add Action");
                                    Debug.Log(temp);

                                    behaviorList.Add(temp);

                                    //EditorWindow window = GetWindow(typeof(ActionWindow));
                                    //window.Close();
                                }
                                else
                                {
                                    selected = k;
                                    clickTime = EditorApplication.timeSinceStartup;
                                }
                            }

                        }




                    }

                }

            }
            if (!anyTrue)
                selected = -1;

            GUILayout.EndScrollView();
        }
    }

    void Refresh()
    {
        void insertSpace(ref string org)
        {
            for (int i = 0; i < org.Length; i++)
            {
                //insert space between lowercase and uppercase
                if (i > 0 && char.IsUpper(org[i]) && char.IsLower(org[i - 1]))
                {
                    org = org.Insert(i++, " ");
                }
                //insert space between number and letter
                if (char.IsDigit(org[i]) && char.IsLetter(org[i - 1]))
                {
                    org = org.Insert(i++, " ");
                }
                //insert space between letter and number
                if (char.IsDigit(org[i]) && char.IsLetter(org[i + 1]))
                {
                    org = org.Insert(++i, " ");
                }
            }
        }

        actionCategories = new List<ActionCategory>();
        expand = new List<bool>();

        foreach (Action action in subClassList)
        {
            bool found = false;
            foreach (ActionCategory ac in actionCategories)
            {
                action.category = action.category[0].ToString().ToUpper() + action.category.Substring(1);
                if (action.category == ac.title)
                {
                    //get class name
                    action.title = action.GetType().Name;
                    //inser space
                    insertSpace(ref action.title);
                    ac.actions.Add(action);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                ActionCategory ac = new();
                ac.title = action.category;
                ac.title = ac.title[0].ToString().ToUpper() + ac.title.Substring(1);

                ac.actions = new List<Action>();
                //get class name
                action.title = action.GetType().Name;
                action.category = action.category[0].ToString().ToUpper() + action.category.Substring(1);

                //inser space
                insertSpace(ref action.title);
                ac.actions.Add(action);
                actionCategories.Add(ac);
            }
        }
    }

    public List<System.Type> GetSubclassTypes(System.Type baseType, Assembly assembly)
    {
        System.Type[] types = assembly.GetTypes();
        List<System.Type> derivedTypes = new();
        foreach (System.Type type in types)
        {
            if (type.IsSubclassOf(baseType))
                derivedTypes.Add(type);
        }
        return derivedTypes;
    }
}

//[CustomPropertyDrawer(typeof())]
//public class VBoolProperty : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        base.OnGUI(position, property, label);
//    }

//}