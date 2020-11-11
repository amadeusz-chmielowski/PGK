using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LeaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            //Debug.LogError("New lvl trigger: " + other.gameObject.tag);
            LevelGenerator.instance.AddPiece();
            LevelGenerator.instance.RemoveOldestPiece();
        }
    }
}
