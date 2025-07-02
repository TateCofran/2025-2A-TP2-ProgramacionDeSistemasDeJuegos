using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string speedParameter = "Speed";
    [SerializeField] private string isJumpingParameter = "IsJumping";
    [SerializeField] private string isFallingParameter = "IsFalling";
    private static readonly List<CharacterAnimator> instances = new();
    public static IReadOnlyList<CharacterAnimator> Instances => instances;

    private Coroutine forceCorrutine;
    private bool isForcing = false;
    private void Reset()
    {
        character = GetComponentInParent<Character>();
        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void Awake()
    {
        if (!character)
            character = GetComponentInParent<Character>();
        if (!animator)
            animator = GetComponentInParent<Animator>();
        if (!spriteRenderer)
            spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (!instances.Contains(this))
            instances.Add(this);

        if (!character || !animator || !spriteRenderer)
        {
            Debug.LogError($"{name} <color=grey>({GetType().Name})</color>: At least one reference is null!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (isForcing)
            return;

        var speed = character.Velocity;
        animator.SetFloat(speedParameter, Mathf.Abs(speed.x));
        animator.SetBool(isJumpingParameter, character.Velocity.y > 0);
        animator.SetBool(isFallingParameter, character.Velocity.y < 0);
        spriteRenderer.flipX = speed.x < 0;
    }
    public void ForceAnimation(AnimatorParams animParams, float duration = 1f)
    {
        if (forceCorrutine != null)
            StopCoroutine(forceCorrutine);
        forceCorrutine = StartCoroutine(ForceAnimationCorrutine(animParams, duration));
    }

    private IEnumerator ForceAnimationCorrutine(AnimatorParams animParams, float duration)
    {
        isForcing = true;
        foreach (var param in animParams.parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(param.name, param.boolValue);
                    break;
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(param.name, param.floatValue);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animator.SetTrigger(param.name);
                    break;
            }
        }
        animator.Play(animParams.animatorStateName);

        yield return new WaitForSeconds(duration);

        isForcing = false;
        forceCorrutine = null;
    }

}
