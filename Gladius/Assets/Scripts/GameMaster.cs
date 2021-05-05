using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Intance;
    public Unit SelectedUnit { get; private set; }

    private void Awake()
    {
        Intance = this;
    }

    public void SelectUnit(Unit unit)
    {
        if (SelectedUnit != null)
        {
            SelectedUnit.OnSelected(false);
        }
        SelectedUnit = unit;
        SelectedUnit.OnSelected();
    }
}
