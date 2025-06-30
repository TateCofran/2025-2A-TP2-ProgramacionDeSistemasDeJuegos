using System;
using System.Collections.Generic;
using System.Linq;


public class HelpCommand : ICommand<string>
{
    private readonly IDebugConsole<string> console;

    public HelpCommand(IDebugConsole<string> console) => this.console = console;

    public string Name => "help";

    public IEnumerable<string> Aliases => new[] { "h", "Help", "HELP"};

    public string Description => "Shows all commands description";

    public void Execute(Action<string> log, params string[] args)
    {
        const string baseOutput = "<color=green>name\t\t|\t\tdescription\n</color>";
        var commandsInfo = console
                           .Commands
                           .Aggregate(baseOutput,
                                      (current, command) => current + $"{command.Name}\t\t|\t\t{command.Description}\n");
        log?.Invoke(commandsInfo);
    }
}
