using UnityEngine;

public interface ICharacterAbstractFactory
{
    ICharacterFactory GetFactory(CharacterPrefabConfig config);
}
public interface ICharacterFactory
{
    GameObject CreateCharacter(CharacterPrefabConfig config, Vector3 position, Quaternion rotation);
}