using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


public class DebugConsole<T> : IDebugConsole<T>
{
    private readonly Action<T> log;
    private readonly Dictionary<T, ICommand<T>> commandDictionary;

    public DebugConsole(Action<T> logs, params ICommand<T>[] commands)
    {
        log = logs;
        Commands = commands.ToHashSet();
        commandDictionary = new Dictionary<T, ICommand<T>>();
        foreach (var command in Commands)
            AddToCommandDictionary(command);
    }

    public HashSet<ICommand<T>> Commands { get; set; }

    public void AddCommand(ICommand<T> command)
    {
        if (!Commands.Add(command))
            throw new DuplicateNameException($"Command {command.Name} has already been added");
        AddToCommandDictionary(command);

    }

    public bool TryAddCommand(ICommand<T> command)
    {
        if(Commands.Contains(command) || !TryAddToCommandDictionary(command))
            return false;
        Commands.Add(command);
        return true;

    }

    public bool IsValidCommand(T name) => commandDictionary.ContainsKey(name);

    public void ExecuteCommand(T name, params T[] args)
    {
        if(!IsValidCommand(name)) return;
        commandDictionary[name].Execute(log, args);
    }

    private void AddToCommandDictionary(ICommand<T> command)
    {
        if (!commandDictionary.ContainsKey(command.Name))
            commandDictionary.Add(command.Name, command);
        else
            throw new DuplicateNameException($"Command {command.Name} already exists in commands dictionary");
        foreach (var alias in command.Aliases)
        {
            if (commandDictionary.TryGetValue(alias, out var duplicate))
                throw new DuplicateNameException($"A command with alias: {alias} already exists in command dictionary" + $"\n{alias} -> {duplicate.Name}");
            commandDictionary.Add(alias, command);
        }
    }
    private bool TryAddToCommandDictionary(ICommand<T> command)
    {
        if(!commandDictionary.ContainsKey(command.Name))
        {
            commandDictionary.Add(command.Name, command);
            return true;
        }
        foreach(var alias in command.Aliases)
        {
            if (!commandDictionary.ContainsKey(alias))
            {
                commandDictionary.Add(alias, command);
                return true;
            }
        }
        return false;
    }
}
