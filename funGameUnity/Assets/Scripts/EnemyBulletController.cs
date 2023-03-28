using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private bool Fire = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.tag == "Player")
		{
			
		}
	}

	public void OffFire()
    {
        Fire = false;
    }

    public void OnFire()
    {
        Fire = true;
    }
}
