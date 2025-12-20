using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    public static CrystalManager instance;
    public int currentCrystals;

    private void Awake()
    {
        instance = this;
    }

    public void AddCrystals(int amount)
    {
        currentCrystals += amount;
        Debug.Log("Crystals: " + currentCrystals);
    }
}
