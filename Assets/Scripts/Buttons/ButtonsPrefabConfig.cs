using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonsPrefab", menuName = "Scriptable Objects/ButtonsPrefab")]
public class ButtonsPrefabConfig : ScriptableObject
{
    public ButtonPrefab[] buttons;

    [Serializable]
    public class ButtonPrefab
    {
        public string bttnTextTittle;
        public CharacterPrefabConfig characterPrefab;
    }

}