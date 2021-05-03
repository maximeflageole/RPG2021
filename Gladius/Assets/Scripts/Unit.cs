using UnityEngine;

public class Unit : MonoBehaviour
{
    protected Tile m_currentTile;

    [SerializeField] private Vector2Int m_initialPos;

    private void Start()
    {
        if (!Map.Instance.TryGetTileAtPos(m_initialPos, ref m_currentTile))
        {
            Debug.LogError("Position of " + this.name + " unit is set to an unexisting position at " + m_initialPos);
            return;
        }
        transform.position = m_currentTile.transform.position;
    }
}
