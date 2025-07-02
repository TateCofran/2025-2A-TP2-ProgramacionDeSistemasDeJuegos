using System;
using UnityEngine;
using UnityEngine.UI;

public interface IButtonFactory<T> : ISetup<Transform>
{
    Button CreateButton(T entry, Action<T> onClick);
}

