using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    [SerializeField] private ButtonFactory buttonFactory;
    [SerializeField] private Transform parent;
    [SerializeField] private ButtonsPrefabConfig[] configs;

    private void Start()
    {
        buttonFactory.Setup(parent);

        foreach (var config in configs)
        {
            foreach (var entry in config.buttons)
            {
                buttonFactory.CreateButton(entry, OnButtonClick);
            }
        }
    }

    private void OnButtonClick(ButtonsPrefabConfig.ButtonPrefab entry)
    {
        Debug.Log("Spawn Character on click");

        var spawner = FindFirstObjectByType<CharacterSpawner>();
        if (spawner && entry.characterPrefab)
            spawner.Spawn(entry.characterPrefab);
    }
}
