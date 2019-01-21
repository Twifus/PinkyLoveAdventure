using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * La classe de base d'un truc qui se déplace
 */
public class Entity : MonoBehaviour
{
    Rigidbody2D m_rb2D;
    /**
     * Valeurs de vitesse
     */
    public float walk_speed;
    public float sprint_speed;
    protected float speed;

    /**
     * Direction du déplacement
     */
    protected Vector2 m_direction;

    /**
     * Gestion de l'animation
     */
    public GestionSprite gestionSprite;

    /**
     * Fonction appelée à l'instenciation
     */
    protected virtual void Start()
    {
        speed = 0.0f;
        m_rb2D = gameObject.GetComponent<Rigidbody2D>();
        gestionSprite = gameObject.GetComponent<GestionSprite>();
    }

    /**
     * Fonction appelée une fois par Frame
     */
    protected virtual void FixedUpdate()
    {
        ChangeDirection();
        if (gestionSprite != null)
            gestionSprite.GiveState(m_direction);
        Move();
    }

    /**
     * Fonction calculant la valeur de m_direction
     */
    protected virtual void ChangeDirection()
    {
        return;
    }

    /**
     * Fonction qui réinitialise m_direction à zéro
     */
    protected void Stop()
    {
        m_direction = Vector2.zero;
    }

    /**
     * Fonction qui gère le déplacement
     */
    protected void Move()
    {
        Vector2 newPos;
        newPos.x = transform.position.x + m_direction.x * speed;
        newPos.y = transform.position.y + m_direction.y * speed;
        m_rb2D.MovePosition(newPos);
    }
}
