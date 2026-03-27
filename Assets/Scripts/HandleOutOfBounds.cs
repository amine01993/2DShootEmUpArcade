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
        if (collision.gameObject.CompareTag("Background FinishLine"))
        {
            scrollingBackground.transform.position = new Vector3(0f, initialYPosition, 0f);
        }
    }
}
