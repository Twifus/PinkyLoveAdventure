using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieBehaviour : MonoBehaviour {

    private float _birthTime = 0;
    private float _lifeTime = 0;
    
    public void setLifeTime(float t)
    {
        _lifeTime = t;
    }

	void Start () {
        _birthTime = Time.time;	
	}
	
	void Update () {
		if (Time.time > _birthTime + _lifeTime)
        {
            Destroy(gameObject);
            return;
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
