using System.Collections.Generic;
using UnityEngine;

public class DetectionAreaCollider : MonoBehaviour
{
    public List<GameObject> PlayerObjects = new List<GameObject>();
    public List<GameObject> EnemyObjects = new List<GameObject>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!PlayerObjects.Contains(other.gameObject))
            {
                PlayerObjects.Add(other.gameObject);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (!EnemyObjects.Contains(other.gameObject))
            {
                EnemyObjects.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerObjects.Contains(other.gameObject))
            {
                PlayerObjects.Remove(other.gameObject);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (EnemyObjects.Contains(other.gameObject))
            {
                EnemyObjects.Remove(other.gameObject);
            }
        }
    }
}
