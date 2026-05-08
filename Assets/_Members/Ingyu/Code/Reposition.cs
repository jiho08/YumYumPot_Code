using Code.Players;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private PlayerMovement _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<PlayerMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        if (_player == null)
            return;

        Vector3 playerPos = _player.transform.position;
        Vector3 myPos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector2 playerDir = _player.Velocity.normalized;

        float dirX = playerDir.x >= 0 ? 1f : -1f;
        float dirY = playerDir.y >= 0 ? 1f : -1f;

        if (diffX > diffY)
        {
            transform.Translate(Vector3.right * dirX * 40f);
        }
        else
        {
            transform.Translate(Vector3.up * dirY * 40f);
        }
    }
}
