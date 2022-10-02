using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Bullet Head/Tile Data", order = 0)]
public class TileWithData : ScriptableObject
{
    public TileBase[] tileList;

    public TileType type;
}
