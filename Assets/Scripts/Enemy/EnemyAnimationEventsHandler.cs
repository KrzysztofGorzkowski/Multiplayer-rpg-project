using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// (NOTICE!) This script is a component of Character GameObject, NOT Enemy!
/// This script is used to handle animation events (which can be handled only in script associated with the animated GameObject).
/// </summary>
public class EnemyAnimationEventsHandler : MonoBehaviour
{
    void Die()
    {
        Destroy(transform.gameObject);
    }
}
