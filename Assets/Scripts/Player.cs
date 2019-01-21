using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * La classe du gentil joueur
 */
public class Player : Entity {

    /**
     * Fonction appelée à l'instenciation
     */
    protected override void Start()
    {
        base.Start();
        speed = walk_speed;
    }

    /**
     * Fonction calculant la nouvelle valeur de m_direction
     */
    protected override void ChangeDirection()
    {
        m_direction.x = Input.GetAxis("Horizontal");
        m_direction.y = Input.GetAxis("Vertical");
    }


    /**
     * Gestion des zones triggers
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mob")
        {
            collision.GetComponent<Mob>().TriggerAggro(this.gameObject);
        }
    }
}
