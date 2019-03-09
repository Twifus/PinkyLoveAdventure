using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBehaviour : MonoBehaviour {

    private float _birthDate;
    private float _lifeTime = 0;
    private float _speed = 0;
    private Vector2 _direction;
    
	void Start () {
        _birthDate = Time.time;
	}
	
    public void setBeamParameters(Vector2 direction, float speed, float time)
    {
        _direction = direction;
        _speed = speed;
        _lifeTime = time;
    }

	void Update () {
        if (Time.time > _birthDate + _lifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + _speed * Time.deltaTime * _direction, _speed * Time.deltaTime);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.collider.tag == "Mob")
        {
            Destroy(collision.gameObject);
        }
    }

}
