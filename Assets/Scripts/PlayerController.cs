using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions { EAST, NORTH, WEST, SOUTH };

public class PlayerController : MonoBehaviour {

    public GameObject CookiePrefab;
    public GameObject BeamPrefab;

    public Sprite FrontSprite;
    public Sprite BackSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite;

    public float speed;

    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    private GameObject _cookie;
    private GameObject _beam;

    private Directions _heading;

    private bool _attacking;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _heading = Directions.SOUTH;
	}

    // Update is called once per frame
    void Update()
    {
        ChangeSpriteToMatchDirection();

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
        _beam.GetComponent<BeamBehaviour>().setBeamParameters((float)_heading * 90f, 50f, 0.5f);
    }

}
