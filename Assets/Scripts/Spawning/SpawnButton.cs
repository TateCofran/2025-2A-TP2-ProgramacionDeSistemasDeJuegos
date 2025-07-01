using System;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Reset()
        => button = GetComponent<Button>();

    private void Awake()
    {
        if (!button)
            button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (!button)
        {
            Debug.LogError($"{name} <color=grey>({GetType().Name})</color>: {nameof(button)} is null!");
            enabled = false;
            return;
        }
        button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        button?.onClick?.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        var spawner = FindFirstObjectByType<CharacterSpawner>();
        //spawner.Spawn();
    }
}