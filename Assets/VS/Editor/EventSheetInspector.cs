#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using VS;


[CanEditMultipleObjects]
[CustomEditor(typeof(EventSheet))]
public class EventSheetInspector : Editor
{
    EventSheet eventSheet = null;

    SerializedObject so = null;
    SerializedProperty sp = null;

    VariableTypes inputVaribleType = VariableTypes.String;

    string inputVaribleName = "";
    string inputEventName = "";
    string inputTriggerName = "";

    int tabIndex = 0;
    static readonly string[] tabStr = { "Event", "Trigger", "Variable" };

    private void OnEnable()
    {
        eventSheet = (EventSheet)target;
        so = new SerializedObject(eventSheet);
        sp = so.FindProperty("conditions"); //eventGroups //

        eventSheet.conditionsRL = new ReorderableList(so, sp, true, false, false, false)
        {
            //drawHeaderCallback = (Rect r) =>
            //{
            //    EditorGUI.LabelField(r, "Events:");
            //    //add
            //    if (GUI.Button(new Rect(r.x + 50, r.y, 20, EditorGUIUtility.singleLineHeight), "+"))
            //    {
            //        //eventSheet.eventGroups.Add(new EventGroup());
            //    }
            //},

            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 1.0f;
                rect.x += 10.0f;
                rect.width -= 10.0f;

                SerializedProperty element = sp.GetArrayElementAtIndex(index);
                SerializedProperty actionSP = element.FindPropertyRelative("actions");

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width - 25, EditorGUIUtility.singleLineHeight), element, true);

                if (element.isExpanded)
                {
                    //add condition Button
                    if (GUI.Button(new Rect(rect.x + rect.width - 20, rect.y + 20, 20, EditorGUIUtility.singleLineHeight), "+"))
                    {
                        //ConditionMenu(eventSheet.conditions);
                        ActionMenu(eventSheet.conditions[index].actions);
                    };
                }

                //remove Button
                if (GUI.Button(new Rect(rect.x + rect.width - 20, rect.y + 2, 20, EditorGUIUtility.singleLineHeight), "x"))
                {
                    eventSheet.conditions.RemoveAt(index);
                };
            },

            elementHeightCallback = (int index) =>
            {
                return EditorGUI.GetPropertyHeight(sp.GetArrayElementAtIndex(index)) + 4.0f;
            },

            //onAddCallback = (ReorderableList rl) =>
            //{
            //    eventSheet.eventGroups.Add(new EventGroup());
            //},

            //onRemoveCallback = (ReorderableList rl) =>
            //{
            //    int index = rl.index;
            //    eventSheet.eventGroups.RemoveAt(index);
            //},


        };

        SerializedProperty p = so.FindProperty("variables");
        eventSheet.variablesRL = new ReorderableList(so, p, true, false, false, false)
        {

            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 1.0f;
                rect.x += 10.0f;
                rect.width -= 10.0f;

                SerializedProperty element = p.GetArrayElementAtIndex(index);
                SerializedProperty v = element.FindPropertyRelative("value");
                SerializedProperty t = element.FindPropertyRelative("title");

                GUILayout.BeginHorizontal();

                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width - 25, EditorGUIUtility.singleLineHeight), t.stringValue);
                EditorGUI.PropertyField(new Rect(rect.x + rect.width / 5, rect.y, rect.width / 1.25f - 25, EditorGUIUtility.singleLineHeight), v, GUIContent.none, true);

                GUILayout.EndHorizontal();

                //remove
                if (GUI.Button(new Rect(rect.x + rect.width - 20, rect.y + 2, 20, EditorGUIUtility.singleLineHeight), "x"))
                {
                    eventSheet.variables.RemoveAt(index);
                };
            },

            elementHeightCallback = (int index) =>
            {
                SerializedProperty element = p.GetArrayElementAtIndex(index);
                SerializedProperty v = element.FindPropertyRelative("value");
                return EditorGUI.GetPropertyHeight(v/*p.GetArrayElementAtIndex(index)*/) + 4.0f;
            },

        };

        SerializedProperty t = so.FindProperty("triggers");
        eventSheet.triggersRL = new ReorderableList(so, t, true, false, false, false)
        {

            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 1.0f;
                rect.x += 10.0f;
                rect.width -= 10.0f;

                var element = t.GetArrayElementAtIndex(index);
                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width - 25, EditorGUIUtility.singleLineHeight), element.stringValue);

                //remove
                if (GUI.Button(new Rect(rect.x + rect.width - 20, rect.y + 2, 20, EditorGUIUtility.singleLineHeight), "x"))
                {
                    eventSheet.triggers.RemoveAt(index);
                };
            },

            elementHeightCallback = (int index) =>
            {
                return EditorGUI.GetPropertyHeight(t.GetArrayElementAtIndex(index)) + 4.0f;
            },

        };

    }
    public override void OnInspectorGUI()
    {
        tabIndex = GUILayout.Toolbar(tabIndex, tabStr);
        so.Update();
        if (tabIndex == 0)
        {
            eventSheet.conditionsRL.DoLayoutList();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Event Name:", EditorStyles.boldLabel, GUILayout.Width(100));
            inputEventName = EditorGUILayout.TextField(inputEventName);
            if (GUILayout.Button("Add"))
            {
                ConditionMenu(eventSheet.conditions);
            }

            EditorGUILayout.EndHorizontal();
        }
        else if (tabIndex == 1)
        {
            eventSheet.triggersRL.DoLayoutList();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Trigger Name:", EditorStyles.boldLabel, GUILayout.Width(100));
            inputTriggerName = EditorGUILayout.TextField(inputTriggerName);

            if (GUILayout.Button("Add") && inputTriggerName != "")
            {
                bool exist = false;

                foreach (string v in eventSheet.triggers)
                {
                    if (v == inputTriggerName)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    eventSheet.triggers.Add(inputTriggerName);
                    inputTriggerName = "";
                }
                else
                {
                    EditorUtility.DisplayDialog("Exist warning", "Trigger alredy exist", "Cancel");
                }
            }

            EditorGUILayout.EndHorizontal();
        }
        else if (tabIndex == 2)
        {
            eventSheet.variablesRL.DoLayoutList();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Variable Name:", EditorStyles.boldLabel, GUILayout.Width(100));

            inputVaribleName = EditorGUILayout.TextField(inputVaribleName);
            inputVaribleType = (VariableTypes)EditorGUILayout.EnumPopup(inputVaribleType, GUILayout.Width(100));

            if (GUILayout.Button("Add"))
            {
                if (inputVaribleName == "")
                    return;

                bool exist = false;

                foreach (Variable v in eventSheet.variables)
                {
                    if (v.title == inputVaribleName)
                    {
                        exist = true;
                        break;
                    }
                }

                if (!exist)
                {
                    dynamic temp = null;
                    switch (inputVaribleType)
                    {
                    case VariableTypes.Bool:
                        temp = new VBool() { title = inputVaribleName };break;
                    case VariableTypes.String:
                        temp = new VString() { title = inputVaribleName };break;
                    case VariableTypes.Int:
                        temp = new VInt() { title = inputVaribleName };break;
                    case VariableTypes.Float:
                        temp = new VFloat() { title = inputVaribleName };break;
                    case VariableTypes.GameObject:
                        temp = new VGameObject() { title = inputVaribleName };break;
                    case VariableTypes.Object:
                        temp = new VObject() { title = inputVaribleName };break;
                    case VariableTypes.Vector2:
                        temp = new VVector2() { title = inputVaribleName };break;
                    case VariableTypes.Vector3:
                        temp = new VVector3() { title = inputVaribleName };break;
                    case VariableTypes.Vector4:
                        temp = new VVector4() { title = inputVaribleName };break;
                    case VariableTypes.Color:
                        temp = new VColor() { title = inputVaribleName };break;
                    }
                    eventSheet.variables.Add(temp);

                    inputVaribleName = "";
                }
                else
                {
                    EditorUtility.DisplayDialog("Exist warning", "Variable name alredy exist", "Cancel");
                }

            }

            EditorGUILayout.EndHorizontal();
        }
        so.ApplyModifiedProperties();
    }


    void ConditionMenu(List<Condition> conditions)
    {
        //ConditionWindow window = (ConditionWindow)EditorWindow.GetWindow(typeof(ConditionWindow), false, "Conditions", true);
        //window.Show();

        ConditionWindow window = ScriptableObject.CreateInstance(typeof(ConditionWindow)) as ConditionWindow;
        window.titleContent = new GUIContent("Events");
        window.ShowUtility();

        window.conditions = conditions;

        //GenericMenu menu = new GenericMenu();
        //menu.allowDuplicateNames = false;
        //menu.AddItem(new GUIContent("Once/Start"), false, addCondition, index);
        ////menu.AddItem(new GUIContent("EveryFrame/Update"), false, conditionMenu, "Update");
        //menu.ShowAsContext();

    }

    void ActionMenu(List<VS.Action> behaviors)
    {
        //ActionWindow window = (ActionWindow)EditorWindow.GetWindow(typeof(ActionWindow), false, "Actions", true);
        //window.Show();

        ActionWindow actionWindow = ScriptableObject.CreateInstance(typeof(ActionWindow)) as ActionWindow;
        actionWindow.titleContent = new GUIContent("Actions");
        actionWindow.ShowUtility();

        actionWindow.behaviorList = behaviors;

    }
}

//[CustomPropertyDrawer(typeof(VS.Condition))]
//public class EventGroupDrawer : PropertyDrawer
//{
//    ReorderableList cRL = null;
//    SerializedProperty cLP = null;

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        float height;
//        try
//        {
//            height = cRL.GetHeight();
//        }
//        catch
//        {
//            height = 0.0f;
//        }
//        return height;
//    }
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        cLP = property.FindPropertyRelative("actions");

//        EditorGUI.BeginProperty(position, label, property);

//        if (cRL == null)
//        {
//            cRL = new ReorderableList(cLP.serializedObject, cLP, true, true, false, false)
//            {
//                //header
//                drawHeaderCallback = innerRect =>
//                {

//                    EditorGUI.LabelField(innerRect, "Condition:");

//                    //add
//                    if (GUI.Button(new Rect(innerRect.x + 65, innerRect.y, 20, EditorGUIUtility.singleLineHeight), "+"))
//                    {
//                        ActionMenu(cLP.value)
//                    }
//                },

//                //element
//                drawElementCallback = (Rect r, int index, bool isActive, bool isFocused) =>
//                {
//                    r.y += 1.0f;
//                    r.x += 10.0f;
//                    r.width -= 10.0f;
//                    SerializedProperty element = cLP.GetArrayElementAtIndex(index);
//                    EditorGUI.PropertyField(new Rect(r.x, r.y, r.width - 21, EditorGUIUtility.singleLineHeight), element, true);

//                    //remove
//                    if (GUI.Button(new Rect(r.x + r.width - 20, r.y + 2, 20, EditorGUIUtility.singleLineHeight), "x"))
//                    {
//                        //target.eventGroups[0].conditions.RemoveAt(index);
//                    }
//                },

//                elementHeightCallback = (int index) =>
//                {
//                    return EditorGUI.GetPropertyHeight(cLP.GetArrayElementAtIndex(index)) + 4.0f;
//                },
//            };
//        }

//        EditorGUI.EndProperty();

//        //position = EditorGUI.IndentedRect(position);

//        Rect cR = new(position.x, position.y, position.width, position.height);

//        cRL.DoList(cR);

//    }
//    void ActionMenu(List<VS.Action> behaviors)
//    {
//        //ActionWindow window = (ActionWindow)EditorWindow.GetWindow(typeof(ActionWindow), false, "Actions", true);
//        //window.Show();

//        ActionWindow actionWindow = ScriptableObject.CreateInstance(typeof(ActionWindow)) as ActionWindow;
//        actionWindow.titleContent = new GUIContent("Actions");
//        actionWindow.ShowUtility();

//        actionWindow.behaviorList = behaviors;

//    }

//    //void conditionMenu()
//    //{
//    //    GenericMenu menu = new GenericMenu();
//    //    menu.allowDuplicateNames = false;
//    //    menu.AddItem(new GUIContent("Once/Start"), false, addCondition);
//    //    //menu.AddItem(new GUIContent("EveryFrame/Update"), false, conditionMenu, "Update");
//    //    menu.ShowAsContext();

//    //}
//    //void addCondition()
//    //{
//    //    target.addCondition();
//    //}

//    //void actionMenu()
//    //{
//    //    GenericMenu menu = new GenericMenu();
//    //    menu.allowDuplicateNames = false;
//    //    menu.AddItem(new GUIContent("Once/Start"), false, addAction);
//    //    //menu.AddItem(new GUIContent("EveryFrame/Update"), false, conditionMenu, "Update");
//    //    menu.ShowAsContext();

//    //}
//    //void addAction()
//    //{
//    //    target.addAction();
//    //}
//}


public class ListViewExampleWindow : EditorWindow
{
    [MenuItem("Window/ListViewExampleWindow")]
    public static void OpenDemoManual()
    {
        GetWindow<ListViewExampleWindow>().Show();
    }

    public void OnEnable()
    {
        var items = new List<string>(1000);
        for (int i = 1; i <= 1000; i++)
            items.Add(i.ToString());

        // The "makeItem" function will be called as needed
        // when the ListView needs more items to render
        //Func<VisualElement> makeItem = () => new Label();

        // As the user scrolls through the list, the ListView object
        // will recycle elements created by the "makeItem"
        // and invoke the "bindItem" callback to associate
        // the element with the matching data item (specified as an index in the list)
        //Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i];

        // Provide the list view with an explict height for every row
        // so it can calculate how many items to actually display
        //const int itemHeight = 16;

        ListView listView = new(items/*, itemHeight, makeItem, bindItem*/);

        //listView.selectionType = SelectionType.Multiple;

        //listView.onItemsChosen += obj => Debug.Log(obj);
        //listView.onSelectionChange += objects => Debug.Log(objects);

        //listView.style.flexGrow = 1.0f;

        rootVisualElement.Add(listView);
    }
}


#endif