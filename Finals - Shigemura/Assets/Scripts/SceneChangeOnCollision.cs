using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnCollision : MonoBehaviour
{
    public int LevelIndex;
    public string requiredTag = "Food"; // Tag to check for
    public int requiredAmount = 4; // Required amount of objects with the tag

    private int objectsWithTag = 0; // Counter for objects with the required tag

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(requiredTag))
        {
            objectsWithTag++; 

            if (objectsWithTag >= requiredAmount) 
            {
                SceneManager.LoadScene(LevelIndex);
            }
        }
    }
}
