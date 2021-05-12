using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int Pos { get; protected set; }
    public bool IsObstructed { get; private set; }
    public bool IsOccupied { get; private set; }
    public int TravelCost { get; protected set; } = 1;
    [SerializeField] private SpriteRenderer m_defaultTile;
    [SerializeField] private SpriteRenderer m_obstructedTile;
    [SerializeField] private SpriteRenderer m_traversableTile;

    public void Init(bool obstructed, Vector2Int pos)
    {
        Pos = pos;
        IsObstructed = obstructed;
        m_defaultTile.enabled = !obstructed;
        m_obstructedTile.enabled = obstructed;
    }

    public void AssignUnit(Unit unit = null)
    {
        var occupied = unit != null;
        IsOccupied = occupied;
        m_defaultTile.enabled = !occupied;
        m_obstructedTile.enabled = occupied;
    }

    public void DisplayTraversable(bool isTraversable)
    {
        if (IsObstructed || IsOccupied)
            return;
        m_defaultTile.enabled = !isTraversable;
        m_traversableTile.enabled = isTraversable;
    }
}
