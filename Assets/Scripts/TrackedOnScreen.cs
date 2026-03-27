using UnityEngine;

public class TrackedOnScreen : MonoBehaviour
{
	private void OnDestroy()
	{
		OnScreenObjectsTracker.UnTrack(gameObject);
	}
}
