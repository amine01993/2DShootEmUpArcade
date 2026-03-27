
[System.Serializable]
public class EnemySpawnProbs
{
	public float[] probs = new float[3];

	public EnemySpawnProbs(float[] vals)
	{
		for (int i = 0; i < probs.Length && i < vals.Length; ++i)
		{
			probs[i] = vals[i];
		}
	}

	public float this[int i]
	{
		get { return probs[i]; }
		set { probs[i] = value; }
	}
}

