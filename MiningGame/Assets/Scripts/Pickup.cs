using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CrystalManager.instance.AddCrystals(value);
            Destroy(gameObject);
        }
    }
}

