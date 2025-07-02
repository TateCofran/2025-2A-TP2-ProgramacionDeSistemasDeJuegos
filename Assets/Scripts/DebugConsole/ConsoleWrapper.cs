using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Debug Console/Debug Console", fileName = "DebugConsole", order = 1000)]
public class ConsoleWrapper : ScriptableObject, IDebugConsole<string>
{
    [SerializeField] protected List<CommandSo> commands;
    [SerializeField] protected char[] separators;
    [SerializeField] private AnimationLibrary animationLibrary;

    protected IDebugConsole<string> DebugConsole;

    public Action<string> log = delegate { };

    public HashSet<ICommand<string>> Commands
    {
        get => DebugConsole.Commands;
        set => DebugConsole.Commands = value;
    } 
    protected void OnEnable()
    {
        Debug.unityLogger.logHandler = new DebugLogHandler((msg) => {log(msg);});

        DebugConsole = new DebugConsole<string>((str) => log(str), commands.Cast<ICommand<string>>().ToArray());
        var aliasesCommand = new AliassesCommand(DebugConsole);
        DebugConsole.AddCommand(aliasesCommand);
        var helpCommand = new HelpCommand(DebugConsole);
        DebugConsole.AddCommand(helpCommand);
        var playAnimationCommand = new PlayAnimationCommand(DebugConsole, animationLibrary);
        DebugConsole.AddCommand(playAnimationCommand);

    }

    public bool TryAddCommand(ICommand<string> command) => DebugConsole.TryAddCommand(command);

    public bool TryUseInput(string input)
    {
        var inputs = input.Split(separators);
        var commandName = inputs[0];
        if (!DebugConsole.IsValidCommand(commandName))
        {
            Debug.Log($"command not found: {commandName}");
            return false;
        }

        var args = inputs[1..];

        DebugConsole.ExecuteCommand(commandName, args);
        return true;
    }

    public void AddCommand(ICommand<string> command) => DebugConsole.AddCommand(command);

    public bool IsValidCommand(string name) => DebugConsole.IsValidCommand(name);

    public void ExecuteCommand(string name, params string[] args) => DebugConsole.ExecuteCommand(name, args);
}
