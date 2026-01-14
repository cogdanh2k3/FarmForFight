using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private Grid m_squareGrid;
    [SerializeField] private GameObject m_blockLandPrefab;
    [SerializeField] private LayerMask m_groundMask;

    [SerializeField] private GameObject m_blockPreview;

    private void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // check if hit layer ground mask
            if ((m_groundMask.value & (1 << hit.collider.gameObject.layer)) == 0)
            {
                return;
            }

            Vector3Int cellPosition = m_squareGrid.WorldToCell(hit.point);
            Vector3 cellPositionWorld = m_squareGrid.GetCellCenterWorld(cellPosition);

            m_blockPreview.transform.position = cellPositionWorld;

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Instantiate(m_blockLandPrefab, cellPositionWorld, Quaternion.identity);
            }
        }
    }
}
