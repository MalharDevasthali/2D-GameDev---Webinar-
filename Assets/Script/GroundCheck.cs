using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
            isGrounded = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
            isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
            isGrounded = false;
    }
}
