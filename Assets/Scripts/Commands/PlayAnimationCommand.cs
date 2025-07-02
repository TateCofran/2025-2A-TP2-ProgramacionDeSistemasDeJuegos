using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayAnimationCommand : ICommand<string>
{
    private readonly IDebugConsole<string> console;
    private AnimationLibrary animLibrary;

    public PlayAnimationCommand(IDebugConsole<string> console, AnimationLibrary animLibrary)
    {
        this.console = console;
        this.animLibrary = animLibrary;
    }
    public void Execute(Action<string> log, params string[] args)
    {
        if (args.Length == 0)
        {
            Debug.Log("Use: playanimation <animation>. Available Animations:");
            foreach (var a in animLibrary.animations)
                Debug.Log($"- {a.parameters}");
            return;
        }
        var cmdNameOrAlias = args[0];
        var animCommand = animLibrary.animations.FirstOrDefault(c => c.cmdName == cmdNameOrAlias);
        if (animCommand == null)
        {
            Debug.Log($"Command '{cmdNameOrAlias}' not found.");
            return;
        }
        var animators = GameObject.FindObjectsByType<CharacterAnimator>(FindObjectsSortMode.None);
        foreach (var charAnim in animators)
        {
            charAnim.ForceAnimation(animCommand, 2f);
        }
        Debug.Log($"Animation command '{cmdNameOrAlias}' executed on all characters.");
    }

    public string Name => "playAnimation";
    public IEnumerable<string> Aliases => new[] { "playanimation", "PLAYANIMATION", "playanim" };
    public string Description => $"Reproduce the animation for all characters";
}
