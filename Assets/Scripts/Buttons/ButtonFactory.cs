using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFactory : MonoBehaviour, IButtonFactory<ButtonsPrefabConfig.ButtonPrefab>
{
    [SerializeField] private Button prefabs;
    private Transform parents;

    public ButtonFactory(Button buttonPrefab)
    {
        prefabs = buttonPrefab;
    }

    public void Setup(Transform parent)
    {
        parents = parent;
    }

    public Button CreateButton(ButtonsPrefabConfig.ButtonPrefab entry, Action<ButtonsPrefabConfig.ButtonPrefab> onClick)
    {
        var buttonInstance = Instantiate(prefabs, parents);
        var text = buttonInstance.GetComponentInChildren<TextMeshProUGUI>();
        text.text = entry.bttnTextTittle;
        buttonInstance.onClick.AddListener(() => onClick?.Invoke(entry));
        return buttonInstance;
    }
}
