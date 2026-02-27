using UnityEngine;

public class HandleOutOfBounds : MonoBehaviour
{
    private GameObject scrollingBackground;
    private float initialYPosition;

    void Start()
    {
        scrollingBackground = GameObject.FindGameObjectWithTag("Background");
        initialYPosition = scrollingBackground.transform.position.y;
        //initialYPosition = 18f;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player Bullet")
		|| collision.gameObject.CompareTag("Enemy Bullet")
		|| collision.gameObject.CompareTag("Enemy Bullet 2")
		|| collision.gameObject.CompareTag("Enemy Bullet 3")) {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Background FinishLine")) {
            scrollingBackground.transform.position = new Vector3(0f, initialYPosition, 0f);
		}
	}
}
