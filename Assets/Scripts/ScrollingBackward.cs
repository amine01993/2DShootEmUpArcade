using UnityEngine;

public class ScrollingBackward : MonoBehaviour
{
	public float scrollingSpeed = 1.0f;

	void Update()
	{
		transform.Translate(0f, -1 * scrollingSpeed * Time.deltaTime, 0f);
	}
}
