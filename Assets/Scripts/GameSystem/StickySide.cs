using UnityEngine;

public class StickySide : MonoBehaviour
{
	[SerializeField] private PlayerSpring playerSpring;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<ScreenBarrier>(out ScreenBarrier barrier))
		{
			playerSpring.Destroy();
			return;
		}

		if (collider.TryGetComponent<ROck>(out ROck rock))
		{
			if (rock.Achieved)
			{
				if (!playerSpring.isenabled) return;
				playerSpring.Ground();
				return;
			}

			rock.Achieved = true;
			if (!playerSpring.isenabled) return;
			playerSpring.JumpSuccess();
			playerSpring.Ground();
		}

		if (collider.TryGetComponent<ScreenBarrier>(out ScreenBarrier screenBarrier))
		{
			if (!playerSpring.isenabled) return;
			playerSpring.Ground();
		}
	}
}
