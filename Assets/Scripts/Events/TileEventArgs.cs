using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event argument containing a <see cref="Tile>"/>
/// </summary>
public class TileEventArgs : EventArgs
{
    public readonly Tile Tile;

    public TileEventArgs(Tile tile)
    {
        Tile = tile;
    }
}
