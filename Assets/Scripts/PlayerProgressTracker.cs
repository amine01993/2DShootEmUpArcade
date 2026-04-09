
using UnityEngine;

public class PlayerProgressTracker : MonoBehaviour
{
	private Checkpoint lastCheckpoint;
	private PlayerShip ship;
	private CanTakeDamage canTakeDamage;
	private Shield shield;
	private LifeCount lifeCount;
	private Score score;

	public GameObject player;
	public GameObject background;
	public GameObject scoreUI;
	public GameObject lifeCountUI;

	private void Awake()
	{
		Time.timeScale = 1;
	}

	private void Start()
	{
		ship = player.GetComponent<PlayerShip>();
		canTakeDamage = player.GetComponent<CanTakeDamage>();
		shield = player.transform.GetChild(0).GetComponent<Shield>();

		score = scoreUI.GetComponent<Score>();
		lifeCount = lifeCountUI.GetComponent<LifeCount>();

		SpawnBox.OnSave += SaveProgress;
	}

	private void OnDestroy()
	{
		SpawnBox.OnSave -= SaveProgress;
	}

	public void SaveProgress()
	{
		lastCheckpoint = new Checkpoint()
		{
			healthPoints = canTakeDamage.healthPoints,
			shieldPoints = shield.canTakeDamage.healthPoints,
			remainingLifes = lifeCount.remainingLifes,
			score = score.score,
			attack = ship.attack,
			position = player.transform.position,
			timeLineYPos = transform.position.y,
			backgroundYPos = background.transform.position.y,
		};
		//Debug.Log(lastCheckpoint);
	}

	public Checkpoint GetLastCheckpoint()
	{
		return lastCheckpoint;
	}
}

