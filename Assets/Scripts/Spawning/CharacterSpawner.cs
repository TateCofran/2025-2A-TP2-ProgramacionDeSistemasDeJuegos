using UnityEngine;


public class CharacterSpawner : MonoBehaviour
{
    private CharacterFactory factory;

    private void Awake()
    {
        factory = new CharacterFactory();
    }
    public void Spawn(CharacterPrefabConfig prefab)
    {
        factory.CreateCharacter(prefab, transform.position, transform.rotation);
    }
}