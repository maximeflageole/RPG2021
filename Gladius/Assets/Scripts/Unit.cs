using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer m_selectionSpriteRenderer;
    [SerializeField] protected Vector2Int m_initialPos;
    [SerializeField] protected int m_range = 2;

    protected bool m_isSelected;
    protected Tile m_currentTile;


    protected void Start()
    {
        if (!Map.Instance.TryGetTileAtPos(m_initialPos, ref m_currentTile))
        {
            Debug.LogError("Position of " + this.name + " unit is set to an unexisting position at " + m_initialPos);
            return;
        }
        MoveAtTile(m_currentTile);
    }

    public void OnSelected(bool isSelected = true)
    {
        m_isSelected = isSelected;
        if (m_selectionSpriteRenderer != null)
        {
            m_selectionSpriteRenderer.enabled = isSelected;
        }
        if (isSelected) Map.Instance.DisplayRange(m_currentTile, m_range);
    }

    protected void MoveAtTile(Tile tile)
    {
        m_currentTile?.AssignUnit(null);
        m_currentTile = tile;
        transform.position = m_currentTile.transform.position;
        m_currentTile.AssignUnit(this);
    }
}
