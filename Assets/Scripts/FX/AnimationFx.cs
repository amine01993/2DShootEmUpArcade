using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationFx : MonoBehaviour
{
    public void Exit() {
		Destroy(gameObject);
    }
}
