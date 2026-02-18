using UnityEngine;

public class HandleOutOfBounds : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        //Debug.Log($"Outer bounds OnTriggerEnter2D {collision.gameObject.tag}");
		if(collision.gameObject.CompareTag("Player Bullet")
		|| collision.gameObject.CompareTag("Enemy Bullet")
		|| collision.gameObject.CompareTag("Enemy Bullet 2")
		|| collision.gameObject.CompareTag("Enemy Bullet 3")) {
            Destroy(collision.gameObject);
        }
	}
}
