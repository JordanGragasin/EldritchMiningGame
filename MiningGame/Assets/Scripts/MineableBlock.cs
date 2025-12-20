using UnityEngine;

public class MineableBlock : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public GameObject dropPrefab; // crystal prefab

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            BreakBlock();
        }
    }

    void BreakBlock()
    {
        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
