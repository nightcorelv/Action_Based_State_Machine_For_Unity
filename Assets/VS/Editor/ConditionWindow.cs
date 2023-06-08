using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using VS;


public class ConditionWindow : EditorWindow
{
    List<Condition> subClassList = new();

    Vector2 scrollPos;

    List<ConditionCategory> conditionCategories = new();

    double clickTime;

    List<bool> expand = new();

    public List<Condition> conditions;

    int selected = -1;
    int preCategory = -1;
    private bool anyTrue;

    private void OnEnable()
    {
        subClassList.Clear();
        List<System.Type> conditionTypes = GetSubclassTypes(typeof(Condition), Assembly.GetAssembly(typeof(Condition)));
        foreach (System.Type type in conditionTypes)
        {
            subClassList.Add((Condition)System.Activator.CreateInstance(type));
        }
        Refresh();

        if (subClassList.Count > 0)
        {
            for (int i = 0; i < conditionCategories.Count; i++)
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
            for (int i = 0; i < conditionCategories.Count; i++)
            {
                expand[i] = GUILayout.Toggle(expand[i], conditionCategories[i].title, "button");
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
                    for (int k = 0; k < conditionCategories[i].conditions.Count; k++)
                    {
                        if (selected != k)
                        {
                            if (GUILayout.Button("    " + conditionCategories[i].conditions[k].title, "label"))
                            {
                                selected = k;
                                clickTime = EditorApplication.timeSinceStartup;
                            }
                        }
                        else
                        {

                            if (GUILayout.Button("    " + conditionCategories[i].conditions[k].title, "OL SelectedRow", GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                            {
                                if ((EditorApplication.timeSinceStartup - clickTime) < 0.2f && k == selected)
                                {
                                    Condition temp = (Condition)System.Activator.CreateInstance(conditionCategories[i].conditions[k].GetType());
                                    temp.title = conditionCategories[i].conditions[k].title;
                                    temp.category = conditionCategories[i].conditions[k].category;

                                    bool exist = false;

                                    if(temp.title == "Start" || temp.title == "Update")
                                    {
                                        foreach (var obj in conditions)
                                        {
                                            if (obj.title == temp.title)
                                            {
                                                exist = true;
                                                EditorUtility.DisplayDialog("Exist warning", "Event alredy exist", "Cancel");
                                            }
                                        }
                                    }


                                    if(!exist)
                                    {
                                        //Undo.RecordObject(eventSheet, "Add Action");
                                        conditions.Add(temp);
                                    }

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

        conditionCategories = new List<ConditionCategory>();
        expand = new List<bool>();

        foreach (Condition condition in subClassList)
        {
            bool found = false;
            foreach (ConditionCategory cc in conditionCategories)
            {
                condition.category = condition.category[0].ToString().ToUpper() + condition.category.Substring(1);
                if (condition.category == cc.title)
                {
                    //get class name
                    condition.title = condition.GetType().Name;
                    //inser space
                    insertSpace(ref condition.title);
                    cc.conditions.Add(condition);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                ConditionCategory cc = new();
                cc.title = condition.category;
                cc.title = cc.title[0].ToString().ToUpper() + cc.title.Substring(1);

                cc.conditions = new List<Condition>();
                //get class name
                condition.title = condition.GetType().Name;
                condition.category = condition.category[0].ToString().ToUpper() + condition.category.Substring(1);

                //inser space
                insertSpace(ref condition.title);
                cc.conditions.Add(condition);
                conditionCategories.Add(cc);
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
            {
                derivedTypes.Add(type);
            }
        }
        return derivedTypes;
    }
}

//[CustomPropertyDrawer(typeof(VBool))]
//public class VBoolProperty : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        base.OnGUI(position, property, label);
//    }

//}