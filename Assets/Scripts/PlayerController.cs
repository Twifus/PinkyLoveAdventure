using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Directions { EAST, NORTH, WEST, SOUTH };

public class PlayerController : MonoBehaviour {

    public GameObject CookiePrefab;
    public GameObject BeamPrefab;

    public Sprite FrontSprite;
    public Sprite BackSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite;

    public AudioClip _deathSound;
    public GameObject _deathScreen;
    public float _deathTimeOut;

    public DialogManager _dialogDisplayer;

    public float speed;

    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    private GameObject _cookie;
    private GameObject _beam;
    private Dialog _closestNPCDialog;

    private Directions _heading;
    private bool _attacking;
    private bool _dead;
    private float _deathTime;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _heading = Directions.SOUTH;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (_dead)
        {
            //if (Time.time - _deathTime > _deathSound.length + _deathTimeOut)
            if (Time.time - _deathTime > _deathTimeOut)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        if (_dialogDisplayer.IsOnScreen())
             return;

        ChangeSpriteToMatchDirection();

        if (Input.GetKeyDown(KeyCode.Space) && _closestNPCDialog) {
                _dialogDisplayer.SetDialog(_closestNPCDialog.GetDialog());
        }

        if (!_cookie && !_beam)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                cookieAttack();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                beamAttack();
            }
        }
        else if (_cookie)
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                Destroy(_cookie);
            }
        }
	}

    private void FixedUpdate()
    {
        if (_dead || _dialogDisplayer.IsOnScreen())
            return;

        if (!_cookie && !_beam)
        {
            Move();
        }
    }

    private void Move()
    {
        float horizontalOffset = Input.GetAxis("Horizontal");
        float verticalOffset = Input.GetAxis("Vertical");

        // Translates the player to a new position, at a given speed.
        Vector2 newPos = new Vector2(transform.position.x + horizontalOffset,
                                     transform.position.y + verticalOffset)
                                     * speed;
        _rb.MovePosition(newPos);

        // Computes the player main direction (North, Sound, East, West)
        if (Mathf.Abs(horizontalOffset) > Mathf.Abs(verticalOffset))
        {
            if (horizontalOffset > 0)
            {
                _heading = Directions.EAST;
            }
            else
            {
                _heading = Directions.WEST;
            }
        }
        else if (Mathf.Abs(horizontalOffset) < Mathf.Abs(verticalOffset))
        {
            if (verticalOffset > 0)
            {
                _heading = Directions.NORTH;
            }
            else
            {
                _heading = Directions.SOUTH;
            }
        }
    }

    private void ChangeSpriteToMatchDirection()
    {
        if (_heading == Directions.NORTH)
        {
            _renderer.sprite = BackSprite;
        }
        else if (_heading == Directions.SOUTH)
        {
            _renderer.sprite = FrontSprite;
        }
        else if (_heading == Directions.EAST)
        {
            _renderer.sprite = RightSprite;
        }
        else if (_heading == Directions.WEST)
        {
            _renderer.sprite = LeftSprite;
        }
    }

    void cookieAttack()
    {
        _cookie = (GameObject)Instantiate(CookiePrefab, transform.position + Quaternion.AngleAxis((float)_heading * 90f, Vector3.forward) *  new Vector3(8, 0), Quaternion.identity);
    }

    void beamAttack()
    {
        _beam = (GameObject)Instantiate(BeamPrefab, transform.position + Quaternion.AngleAxis((float)_heading * 90f, Vector3.forward) * new Vector3(8, 0), Quaternion.identity);
        _beam.GetComponent<BeamBehaviour>().setBeamParameters(Vector2.down, 50f, 0.5f);
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
}
