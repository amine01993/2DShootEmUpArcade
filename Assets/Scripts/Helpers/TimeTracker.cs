
using UnityEngine;

public class TimeTracker
{
	private float tracker = 0f;
	private readonly float cooldown;
	private readonly bool resets;

	public TimeTracker(float cooldown, bool resets = true)
	{
		this.cooldown = cooldown;
		this.resets = resets;
	}

	public void Update()
	{
		tracker += Time.deltaTime;
		if (IsExpired() && resets)
		{
			Reset();
		}
	}

	public bool IsZero()
	{
		return tracker == 0f;
	}

	public bool IsExpired()
	{
		return tracker >= cooldown;
	}

	public void Reset()
	{
		tracker = 0f;
	}

	public float GetRatio()
	{
		return Mathf.Min(tracker / cooldown, 1f);
	}
}

