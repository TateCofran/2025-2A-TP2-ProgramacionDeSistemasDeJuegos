using UnityEngine;
public class CharacterAbstractFactory : ICharacterAbstractFactory
{
    private readonly ICharacterFactory factory;
    public CharacterAbstractFactory(ICharacterFactory defaultFactory)
    {
        factory = defaultFactory;
    }

    public ICharacterFactory GetFactory(CharacterPrefabConfig config) => factory;
}
public class CharacterFactory: ICharacterFactory
{
    public GameObject CreateCharacter(CharacterPrefabConfig config, Vector3 position, Quaternion rotation)
    {
        if (config == null)
        {
            Debug.LogError("CharacterFactory.CreateCharacter: null");
            return null;
        }
        var result = Object.Instantiate(config.prefab, position, rotation);

        if (!result.TryGetComponent<ISetup<CharacterModel>>(out var characterSetup))
            characterSetup = result.AddComponent<Character>();
        characterSetup.Setup(config.characterModel);

        if (!result.TryGetComponent<ISetup<IPlayerControllerModel>>(out var controllerSetup))
            controllerSetup = result.AddComponent<PlayerController>();
        controllerSetup.Setup(config.controllerModel);

        var animator = result.GetComponentInChildren<Animator>();
        if (animator == null)
            animator = result.AddComponent<Animator>();
        animator.runtimeAnimatorController = config.animatorController;

        return result.gameObject;
    }
}