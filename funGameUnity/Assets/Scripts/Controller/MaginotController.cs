using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaginotController : MonoBehaviour
{
	private static MaginotController Instance = null;

    private SpriteRenderer Renderer;
    private float Width;
    
	public static MaginotController GetInstance()
	{
		if (Instance == null)
		{
			Instance = new MaginotController();
		}
		return Instance;
	}

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Width = Renderer.sprite.bounds.size.x;
    }

    private void Update()
	{
        if (ControllerManager.GetInstance().PlayerRunHard)
            Climate.GetInstance().Slide(gameObject);
        else if (transform.position.x + Width * transform.localScale.x * 0.4 < 0)
			transform.position += Vector3.right * Time.deltaTime;
        
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			ControllerManager.GetInstance().CommonHit(100*Time.deltaTime);
		}
	}
}
