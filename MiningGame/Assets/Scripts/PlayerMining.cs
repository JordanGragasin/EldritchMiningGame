using UnityEngine;

public class PlayerMining : MonoBehaviour
{
    public float mineRange = 1.2f;
    public int mineDamage = 1;
    public float mineCooldown = 0.25f;

    private float lastMineTime;

    public LayerMask mineableLayer;

    void Update()
    {
        if (Input.GetMouseButton(0)) // hold left click
        {
            TryMine();
        }
    }

    void TryMine()
    {
        if (Time.time < lastMineTime + mineCooldown)
            return;

        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            mineRange,
            mineableLayer
        );

        if (hit.collider != null)
        {
            MineableBlock block = hit.collider.GetComponent<MineableBlock>();
            if (block != null)
            {
                block.TakeDamage(mineDamage);
                lastMineTime = Time.time;
            }
        }
    }
}
