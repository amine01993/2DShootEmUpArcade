using UnityEngine;

public class BelongToWave : MonoBehaviour
{
	private int spawnLineLayer;
	private ScrollingBackward scrollingBackward;
	private EnemySpawnManager spawnManager;
	public bool reachedSpawnLine = false;

	public GameObject wave;

	void Start()
    {
		var scrollingBg = GameObject.FindGameObjectWithTag("Background");
		scrollingBackward = scrollingBg.GetComponent<ScrollingBackward>();

		spawnManager = wave.GetComponent<EnemySpawnManager>();

		spawnLineLayer = LayerMask.NameToLayer("SpawnLine");
	}

	void Update()
    {
        if(!spawnManager.isRandom && !reachedSpawnLine) {
			transform.position += new Vector3(0f, -1f * scrollingBackward.scrollingSpeed * Time.deltaTime, 0f);
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == spawnLineLayer)
		{
			reachedSpawnLine = true;
		}
	}
}
