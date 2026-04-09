using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Vfx : MonoBehaviour
{
	private ParticleSystem vfx;

	private void Start()
	{
		vfx = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if(vfx.isStopped) {
			Destroy(gameObject);
		}
	}
}
