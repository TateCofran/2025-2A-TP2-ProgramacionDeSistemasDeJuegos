using UnityEngine;

[CreateAssetMenu(fileName = "CharacterPrefab", menuName = "Scriptable Objects/CharacterPrefab")]
public class CharacterPrefabConfig : ScriptableObject
{
    public GameObject prefab;
    public CharacterModel characterModel;
    public PlayerControllerModel controllerModel;
    public RuntimeAnimatorController animatorController;

}
