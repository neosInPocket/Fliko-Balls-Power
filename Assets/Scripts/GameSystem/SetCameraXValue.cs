using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")]
public class SetCameraXValue : CinemachineExtension
{
	[SerializeField] private PlayerSpring playerSpring;
	public float xLockPosition = 0;
	private float currentYValue;

	protected override void Awake()
	{
		currentYValue = playerSpring.transform.position.y;

		base.Awake();
	}

	protected override void PostPipelineStageCallback(
		CinemachineVirtualCameraBase vcam,
		CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
	{
		if (stage == CinemachineCore.Stage.Body)
		{
			var pos = state.RawPosition;
			pos.x = xLockPosition;
			state.RawPosition = pos;

			if (pos.y < currentYValue)
			{
				pos.y = currentYValue;
				state.RawPosition = pos;
				return;
			}

			currentYValue = pos.y;
		}
	}
}
