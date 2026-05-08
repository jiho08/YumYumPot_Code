using UnityEngine;
using Code.Players;

public class InfiniteBackgroundReposition : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float tileSize = 14f;

    private const int Cols = 5;
    private const int Rows = 5;

    private float CheckDistance => tileSize * 2.5f;
    private float MoveDistance => tileSize * 5f;

    private void Awake()
    {
        if (transform.childCount == 0)
            CreateTiles();
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 playerPos = player.transform.position;

        foreach (Transform tile in transform)
        {
            Vector3 offset = tile.position - playerPos;

            if (Mathf.Abs(offset.x) > CheckDistance)
            {
                tile.position += Vector3.right
                    * MoveDistance
                    * Mathf.Sign(playerPos.x - tile.position.x);
            }

            if (Mathf.Abs(offset.y) > CheckDistance)
            {
                tile.position += Vector3.up
                    * MoveDistance
                    * Mathf.Sign(playerPos.y - tile.position.y);
            }
        }
    }

    private void CreateTiles()
    {
        if (tilePrefab == null)
            return;

        float startX = -(Cols - 1) * 0.5f * tileSize;
        float startY = -(Rows - 1) * 0.5f * tileSize;

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Cols; x++)
            {
                Vector3 pos = new Vector3(
                    startX + x * tileSize,
                    startY + y * tileSize,
                    0f
                );

                Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
