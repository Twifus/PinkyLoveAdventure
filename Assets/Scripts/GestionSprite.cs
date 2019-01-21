using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum Direction { Iddle, Down, Right, Up, Left };

public class GestionSprite : MonoBehaviour {

    /**
     * Tableau des sprites
     * 0 : iddle (tableau des sprites pour le non déplacement)
     * 0,0 : sprite par défaut
     * ensuite c'est selon l'énum
     */
    public Sprite[] iddle;
    public Sprite[] down;
    public Sprite[] right;
    public Sprite[] up;
    public Sprite[] left;

    /**
     * Vitesse de l'animation
     * (temps d'attente entre chaque changement)
     */
    public float anim_speed;

    SpriteRenderer m_render;

    private float timer;
    private Sprite[] state;
    private int i;

    private readonly float error_zero = 0.005f;

    /**
     * Fonction d'initilisation
     */
    void Start ()
    {
        timer = 0;
        i = 0;

        m_render = this.GetComponent<SpriteRenderer>();
	}
	
	/**
     * Fonction appelée à chaque frame
     */
	void FixedUpdate ()
    {
        timer += Time.deltaTime;
        if (timer > anim_speed)
        {
            i++;
            if (i >= state.Length)
                i = 0;
            m_render.sprite = state[i];
            timer = 0;
        }
	}

    /**
     * Définit la direction pointée par l'entité
     */
    public void GiveState(Vector2 direction)
    {
        // Debug.Log("x:" + direction.x + " y:" + direction.y);
        if (Mathf.Abs(direction.x) <= error_zero && Mathf.Abs(direction.y) <= error_zero)
            ChangeState(iddle);
        else if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                ChangeState(right);
            else if (direction.x < 0)
                ChangeState(left);
        }
        else
        {
            if (direction.y > 0)
                ChangeState(up);
            else if (direction.y < 0)
                ChangeState(down);
        }
    }

    /**
     * Fait la transistion d'un état à un autre
     * set le timer pour une transition instantanée
     */
    private void ChangeState(Sprite[] new_state)
    {
        if (state != new_state)
        {
            state = new_state;
            timer = anim_speed + 1;
        }
    }
}
// refresh timer si on change de state (pour eviter la fin de l'animation qui slide)
