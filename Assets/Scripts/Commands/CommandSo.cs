﻿using System;
using System.Collections.Generic;

using UnityEngine;

public abstract class CommandSo : ScriptableObject, ICommand<string>
{
    public abstract void Execute(Action<string> writeToConsole, params string[] args);
    public abstract string Name { get; }
    public abstract IEnumerable<string> Aliases { get; }
    public abstract string Description { get; }
}
