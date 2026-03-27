using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpawnBox: MonoBehaviour
{
    public event Action OnEnable;
    public event Action OnDisable;
    public static event Action OnSave;

	private void Start()
	{
		var spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		OnEnable.Invoke();
		OnSave.Invoke();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		OnDisable.Invoke();
	}

}
