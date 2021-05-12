using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Intance;
    private int m_characterLayer;
    public Unit SelectedUnit { get; private set; }

    private void Awake()
    {
        Intance = this;
        m_characterLayer = LayerMask.GetMask("Characters");
    }

    public void SelectUnit(Unit unit)
    {
        UnselectUnit();
        SelectedUnit = unit;
        SelectedUnit.OnSelected();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100.0F, m_characterLayer);

            if (hit.collider != null)
            {
                var unit = hit.collider.GetComponent<Unit>();
                if (SelectedUnit == unit)
                {
                    UnselectUnit();
                }
                else
                {
                    SelectUnit(unit);
                }
                return;
            }

            UnselectUnit();
        }
    }

    private void UnselectUnit()
    {
        SelectedUnit?.OnSelected(false);
        SelectedUnit = null;
    }
}
