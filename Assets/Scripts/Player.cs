using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * La classe du gentil joueur
 */
public class Player : Entity {

    /**
     * Les attaques
     */
    public GameObject CookiePrefab;
    public GameObject BeamPrefab;
    private GameObject _cookie;
    private GameObject _beam;
    private Vector2 fire_direction;

    /**
     * NPC dialogues
     */
    public DialogManager _dialogDisplayer;
    private Dialog _closestNPCDialog;

    /**
     * Gestion écran de mort
     */
    public AudioClip _deathSound;
    public GameObject _deathScreen;
    public float _deathTimeOut;

    /**
     * Status
     */
    private bool _attacking;
    private bool _dead;
    private float _deathTime;

    /**
     * Fonction appelée à l'instenciation
     */
    protected override void Start()
    {
        base.Start();
        speed = walk_speed;
    }

    private void Update()
    {
        // Exit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Mort
        if (_dead)
        {
            //if (Time.time - _deathTime > _deathSound.length + _deathTimeOut)
            if (Time.time - _deathTime > _deathTimeOut)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        // En dialogue
        if (_dialogDisplayer.IsOnScreen())
            return;

        // Attaque
        if (!_cookie && !_beam)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                CookieAttack();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                BeamAttack();
            }
        }
        else if (_cookie)
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                Destroy(_cookie);
            }
        }

        // Parler
        if (Input.GetKeyDown(KeyCode.Space) && _closestNPCDialog)
        {
            _dialogDisplayer.SetDialog(_closestNPCDialog.GetDialog());
        }
    }

    /**
     * Fonction à chaque frame
     */
    protected override void FixedUpdate()
    {
        if (_dead || _dialogDisplayer.IsOnScreen())
            return;

        if (!_cookie && !_beam)
        {
            // Change direction, change sprite and move
            base.FixedUpdate();
        }
    }

    /**
     * Fonction calculant la nouvelle valeur de m_direction
     */
    protected override void ChangeDirection()
    {
        if (!_cookie && !_beam)
        {
            m_direction.x = Input.GetAxis("Horizontal");
            m_direction.y = Input.GetAxis("Vertical");
        }
        else
        {
            m_direction = Vector2.zero;
        }
       

        if (m_direction.x != 0 || m_direction.y != 0)
        {
            fire_direction = m_direction;
            fire_direction.Normalize();
        }
    }


    /**
     * Gestion des zones triggers et collisions
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            _closestNPCDialog = null;
        }
        else if (collision.tag == "InstantDialog")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mob")
        {
            collision.GetComponent<Mob>().TriggerAggro(this.gameObject);
        }
        else if (collision.tag == "NPC")
        {
            _closestNPCDialog = collision.GetComponent<Dialog>();
        }
        else if (collision.tag == "InstantDialog")
        {
            Dialog instantDialog = collision.GetComponent<Dialog>();
            if (instantDialog != null)
            {
                _dialogDisplayer.SetDialog(instantDialog.GetDialog());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Mob")
        {
            _dead = true;
            _deathTime = Time.time;
            //AudioManager.instance.PlaySound(_deathSound);
            _deathScreen.SetActive(true);
        }
    }

    /**
     * Creation des objets d'attaque
     */
    private void CookieAttack()
    {
        // _cookie = (GameObject)Instantiate(CookiePrefab, transform.position + Quaternion.AngleAxis((float)_heading * 90f, Vector3.forward) * new Vector3(8, 0), Quaternion.identity);
        _cookie = (GameObject)Instantiate(CookiePrefab, transform.position + (Vector3) fire_direction * 8, Quaternion.identity);
    }

    private void BeamAttack()
    {
        _beam = (GameObject)Instantiate(BeamPrefab, transform.position + (Vector3) fire_direction * 8, Quaternion.identity);
        _beam.GetComponent<BeamBehaviour>().setBeamParameters(fire_direction, 50f, 0.5f);
    }
}
