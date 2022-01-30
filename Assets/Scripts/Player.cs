using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class for handling useful gamestate properties.
/// </summary>
public static class Player
{
    public static bool CanInput { get; set; } = true;
    public static bool IsPaused { get; set; } = false;
}
