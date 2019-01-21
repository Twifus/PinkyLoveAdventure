using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * La classe des monstres m�chants
 */
public class Mob : Entity
{
    /**
    * Tableau de points pour la patrouille de l'ennemi
    * walk_counter repr�sente le point cibl� actuellement
    */
    public Vector2[] walk;
    private int walk_counter;

    /**
     * D�finit l'�tat d'aggressivit� du mob
     */
    private bool trigger_aggro;
    /**
     * Cible du mob (souvent le Player)
     */
    private Transform target;
    /**
     * La distance de maintient d'aggro maximal par rapport au point de d�part
     */
    public float chase_distance;
    /**
     * Le temps avant de pouvoir reprendre l'aggro (sec)
     * et un compteur pour v�rifier ce temps
     */
    public float wait_for_aggro;
    private float desaggro_time;



    /**
     * la distance sur x et y min � partir de laquelle le mob
     * d�cide d'aller au prochain point
     */
    private readonly float min_distance = 0.05f;

    /**
     * Fonction appel�e � l'instenciation
     */
    protected override void Start()
    {
        base.Start();
        speed = walk_speed;
        trigger_aggro = false;
        desaggro_time = Time.time;
        walk_counter = 0;
    }

    /**
     * Fonction calculant la nouvelle valeur de m_direction
     * appel�e par FixedUpdate
     */
    protected override void ChangeDirection()
    {
        if (trigger_aggro)
        {
            PointDirection((Vector2) target.position);
            if (IsTooFar())
            {
                StopAggro();
            }
        }
        else
        {
            NextWalk();
            PointDirection(walk[walk_counter]);
        }
    }

    /**
     * Calcul la direction � prendre pour se rendre au point
     */
    void PointDirection(Vector2 point)
    {
        m_direction = point - (Vector2) transform.position;
        m_direction.Normalize();
    }

    /**
    * Calcul le bon indice du point de marche en cours
    */
    void NextWalk()
    {
        if (Vector2.Distance(transform.position, walk[walk_counter]) < min_distance)
        {
            walk_counter++;
            if (walk_counter >= walk.Length)
            {
                walk_counter = 0;
            }
        }
    }

    private bool IsTooFar()
    {
        float distance = Vector2.Distance(transform.position, walk[walk_counter]);
        //Debug.Log("distance au d�part: " + distance);
        return (distance > chase_distance);
    }


    /**
     * Gestion de l'aggro
     */
    public void TriggerAggro(GameObject gameObject)
    {
        if (!IsTooFar() && Time.time > desaggro_time + wait_for_aggro)
        {
            trigger_aggro = true;
            target = gameObject.transform;
            speed = sprint_speed;
        }
    }

    public void StopAggro()
    {
        desaggro_time = Time.time;
        trigger_aggro = false;
        speed = walk_speed;
    }

    /**
     * Collisions
     *
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "theobjectToIgnore")
        {
            Physics.IgnoreCollision(theobjectToIgnore.collider, collider);
        }
    }
    */
}