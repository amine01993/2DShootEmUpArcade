using UnityEngine;


public class Checkpoint
{
	public Vector3 position;
	public int healthPoints;
	public int shieldPoints;
	public int remainingLifes;
	public Attacks attack;
	public float score;
	public float backgroundYPos;
	public float timeLineYPos;

	public override string ToString()
	{
		return $@"hp={healthPoints}, shield={shieldPoints}, lifes={remainingLifes}, 
attack={attack}, pos={position}, score={score}, 
bgPos={backgroundYPos}, tLPos={timeLineYPos}";
	}
}
