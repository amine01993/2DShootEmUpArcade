using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
	public float scrollingSpeed = 1.0f;

	void Start()
	{

	}

	void Update()
	{
		transform.Translate(0f, -1 * scrollingSpeed * Time.deltaTime, 0f);
	}
}
