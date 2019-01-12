using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject _Cookie;
    public int _heading = 0;
    public float attackDelay = 0;

    private float _attackTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > _attackTime + attackDelay)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                cookieAttack();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("non");
            }
        }
	}

    void cookieAttack()
    {
        GameObject obj = (GameObject)Instantiate(_Cookie, transform.position + Quaternion.AngleAxis(_heading * 90, Vector3.forward) *  new Vector3(8, 0), Quaternion.identity);
        obj.GetComponent<CookieBehaviour>().setLifeTime(1.1f * attackDelay);
        _attackTime = Time.time;
    }

}
