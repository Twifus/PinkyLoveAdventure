using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieBehaviour : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Mob")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
