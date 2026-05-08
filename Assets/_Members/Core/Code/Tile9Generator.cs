using UnityEngine;

public class Tile9Generator : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int cols = 3;
    [SerializeField] private int rows = 3;
    [SerializeField] private bool clearChildrenOnStart = true;

    private void Start()
    {
        if (tilePrefab == null)
            return;

        if (clearChildrenOnStart)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
        }

        float tileW = 0f;
        float tileH = 0f;

        var sr = tilePrefab.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            tileW = sr.bounds.size.x;
            tileH = sr.bounds.size.y;
        }

        if (tileW <= 0f || tileH <= 0f)
            return;

        Vector3 origin = transform.position;
        float startX = -(cols - 1) * 0.5f * tileW;
        float startY = -(rows - 1) * 0.5f * tileH;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector3 pos = origin + new Vector3(startX + x * tileW, startY + y * tileH, 0f);
                Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
