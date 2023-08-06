using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class BossSettings : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false; // keeps track of whether the boss is currently flipped or not.

    /// <summary>
    /// Makes the boss face towards the player by flipping its scale and rotating it.
    /// </summary>
    public void LookTowardsPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}