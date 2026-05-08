using UnityEngine;
using Code.Players;

public class EnemyReposition : MonoBehaviour
{
    [SerializeField] private float tileSize = 14f;
    [SerializeField] private float distanceMultiplier = 1.5f;

    private PlayerMovement player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
        Vector3 offset = myPos - playerPos;

        float limit = tileSize * distanceMultiplier;
        float moveDistance = tileSize * 3f;

        if (Mathf.Abs(offset.x) > limit)
        {
            transform.position += Vector3.right
                * moveDistance
                * Mathf.Sign(playerPos.x - myPos.x);
        }

        if (Mathf.Abs(offset.y) > limit)
        {
            transform.position += Vector3.up
                * moveDistance
                * Mathf.Sign(playerPos.y - myPos.y);
        }
    }
}
