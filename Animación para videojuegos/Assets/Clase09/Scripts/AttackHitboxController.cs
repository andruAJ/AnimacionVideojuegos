using UnityEngine;

public class AttackHitboxController : MonoBehaviour
{
    [SerializeField] private GameObject[] hitBoxes;
    public void ToggleHitBoxes()
    {
        for (int hitBoxID = 0; hitBoxID < hitBoxes.Length; hitBoxID++)
        {
            GameObject hitBox = hitBoxes[hitBoxID];
            hitBox.SetActive(!hitBox.activeSelf);
        }
    }
    public void CleanUpHitboxes()
    { 
        foreach (GameObject hitBox in hitBoxes)
        {
            hitBox.SetActive(false);
        }
    }
}
