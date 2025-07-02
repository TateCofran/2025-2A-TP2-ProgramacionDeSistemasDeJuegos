using UnityEngine;

[System.Serializable]
public class AnimatorParams
{
    public string cmdName; 
    public string animatorStateName;
    public AnimatorParameter[] parameters;
}

[System.Serializable]
public class AnimatorParameter
{
    public string name;
    public AnimatorControllerParameterType type;
    public float floatValue;
    public bool boolValue;
}
