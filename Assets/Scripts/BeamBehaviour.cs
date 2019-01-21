using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBehaviour : MonoBehaviour {

    private float _birthDate;
    private float _lifeTime = 0;
    private float _speed = 0;
    private float _heading = 0;
    
	void Start () {
        _birthDate = Time.time;
	}
	
    public void setBeamParameters(float heading, float speed, float time)
    {
        _heading = heading;
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
            Vector2 direction = Quaternion.AngleAxis(_heading, Vector3.forward) * Vector2.right;
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + _speed * Time.deltaTime * direction, _speed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
