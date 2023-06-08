using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace VS
{

    [System.Serializable]
    public class Variable
    {
        [HideInInspector]
        public string title;
    }

    [System.Serializable]
    public class VBool : Variable
    {
        public bool value;
    }

    [System.Serializable]
    public class VString : Variable
    {
        public string value;
    }

    [System.Serializable]
    public class VFloat : Variable
    {
        public float value;
    }

    [System.Serializable]
    public class VInt : Variable
    {
        public int value;
    }

    [System.Serializable]
    public class VGameObject : Variable
    {
        public GameObject value;
    }

    [System.Serializable]
    public class VObject : Variable
    {
        public Object value;
    }

    [System.Serializable]
    public class VColor : Variable
    {
        public Color value;
    }

    [System.Serializable]
    public class VVector2 : Variable
    {
        public Vector2 value;
    }

    [System.Serializable]
    public class VVector3 : Variable
    {
        public Vector3 value;
    }

    [System.Serializable]
    public class VVector4 : Variable
    {
        public Vector4 value;
    }



    public enum VariableTypes
    {
        Bool,
        Int,
        Float,
        String,
        Vector2,
        Vector3,
        Vector4,
        Color,
        GameObject,
        Object
    }

}