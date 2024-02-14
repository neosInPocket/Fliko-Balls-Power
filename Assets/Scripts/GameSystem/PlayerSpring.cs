using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerSpring : MonoBehaviour
{
	[SerializeField] private SpriteRenderer bouncySideRenderer;
	[SerializeField] private SpriteRenderer stickySideRenderer;
	[SerializeField] private SpriteRenderer connector;
	[SerializeField] private float radius;
	[SerializeField] private Vector2 connectorSize;
	[SerializeField] private Transform pivot;
	[SerializeField] private float springSlideStartDistance;
	[SerializeField] private Rigidbody2D springRigid;
	[SerializeField] private float gravityScale;
	[SerializeField] private GameObject destroy;
	[SerializeField] private float[] jumpSpeeds;
	[SerializeField] private float[] ballSizes;
	public float Radius => radius;

	private InternalGameTools screen;
	private Vector2 firstFingerPosition;
	private Vector3 moveFingerPosition;
	private Vector2 bouncySizeStartPosition;
	private float startFingerDistance;
	private float distanceMagnitude;
	private Vector2 bouncyBottomPosition;
	public bool Restricted { get; set; }
	public Action OnJumpSuccess { get; set; }
	public Action OnDestroyed { get; set; }
	public bool isenabled { get; set; }
	private float jumpSpeed;
	private float ballSize;

	private void Start()
	{
		ballSize = DataSystemController.DataSystemValues.shopUpgradesBought[1] ? ballSizes[1] : ballSizes[0];

		screen = new InternalGameTools();

		connector.transform.localPosition = Vector2.zero;
		connector.size = connectorSize;

		bouncySideRenderer.transform.localPosition = new Vector2(0, connectorSize.y / 2);
		stickySideRenderer.transform.localPosition = new Vector2(0, -connectorSize.y / 2);

		bouncySideRenderer.size = new Vector2(ballSize * 2, ballSize * 2);
		stickySideRenderer.size = new Vector2(ballSize * 2, ballSize * 2);

		transform.localPosition = Vector2.zero;
		transform.localPosition = new Vector2(0, connectorSize.y / 2);
		bouncySizeStartPosition = bouncySideRenderer.transform.localPosition;
		bouncyBottomPosition = new Vector2(0, stickySideRenderer.transform.localPosition.y + 2 * ballSize);

		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		if (DataSystemController.DataSystemValues.shopUpgradesBought[0])
		{
			jumpSpeed = jumpSpeeds[1];
		}
		else
		{
			jumpSpeed = jumpSpeeds[0];
		}
	}

	public void ProvideControls()
	{
		Touch.onFingerDown += OnPlayerTouched;
		isenabled = true;
	}

	public void DisableControls()
	{
		Touch.onFingerDown -= OnPlayerTouched;
		Touch.onFingerMove -= OnPlayerHold;
		Touch.onFingerMove -= OnPlayerMidAirMove;
		Touch.onFingerUp -= OnPlayerUnTouched;
	}

	private void OnPlayerTouched(Finger finger)
	{
		Touch.onFingerDown -= OnPlayerTouched;
		Touch.onFingerMove += OnPlayerHold;
		Touch.onFingerUp += OnPlayerUnTouched;

		firstFingerPosition = screen.GetWorldPoint(finger.screenPosition);
		pivot.transform.up = firstFingerPosition.normalized;
		startFingerDistance = Vector2.Distance(firstFingerPosition, new Vector2(pivot.transform.position.x, pivot.transform.position.y + firstFingerPosition.normalized.y));
		distanceMagnitude = 1;
	}

	private void OnPlayerHold(Finger finger)
	{
		moveFingerPosition = screen.GetWorldPoint(finger.screenPosition);
		pivot.transform.up = moveFingerPosition - pivot.transform.position;

		distanceMagnitude = Vector2.Distance(moveFingerPosition, new Vector2(pivot.transform.position.x, pivot.transform.position.y + springSlideStartDistance)) / startFingerDistance;
		if (distanceMagnitude > 1 || distanceMagnitude < 0)
		{
			if (distanceMagnitude > 1)
			{
				distanceMagnitude = 1;
				return;
			}

			if (distanceMagnitude < 0)
			{
				distanceMagnitude = 0;
				return;
			}
		}

		bouncySideRenderer.transform.localPosition = Vector2.Lerp(bouncyBottomPosition, bouncySizeStartPosition, distanceMagnitude * distanceMagnitude);
		connector.size = new Vector2(
			connector.size.x,
			bouncySideRenderer.transform.localPosition.y - stickySideRenderer.transform.localPosition.y
			);
	}

	private void OnPlayerMidAirMove(Finger finger)
	{
		var fingerWorld = screen.GetWorldPoint(finger.screenPosition);
		pivot.transform.up = moveFingerPosition - pivot.transform.position;
	}

	private void OnPlayerUnTouched(Finger finger)
	{
		Touch.onFingerMove -= OnPlayerHold;
		Touch.onFingerUp -= OnPlayerTouched;

		if (Restricted)
		{
			connector.size = connectorSize;
			bouncySideRenderer.transform.localPosition = bouncySizeStartPosition;
			Touch.onFingerDown += OnPlayerTouched;
			return;
		}

		if (distanceMagnitude == 1)
		{
			Touch.onFingerDown += OnPlayerTouched;
			return;
		}

		if (springRigid != null)
		{
			springRigid.gravityScale = gravityScale;
			springRigid.velocity = (1 - distanceMagnitude * distanceMagnitude) * jumpSpeed * pivot.transform.up;

			connector.size = connectorSize;
			bouncySideRenderer.transform.localPosition = bouncySizeStartPosition;
		}

		Touch.onFingerMove += OnPlayerMidAirMove;
	}

	public void Ground()
	{
		if (!isenabled) return;

		springRigid.gravityScale = 0;
		springRigid.velocity = Vector2.zero;
		Touch.onFingerMove -= OnPlayerMidAirMove;
		Touch.onFingerDown += OnPlayerTouched;
	}

	public void ChangePositionFromBottomPivot(Vector2 position)
	{
		pivot.transform.position = position;
	}

	public void JumpSuccess()
	{
		OnJumpSuccess?.Invoke();
	}

	public void Destroy()
	{
		if (!isenabled) return;
		destroy.SetActive(true);
		bouncySideRenderer.enabled = false;
		stickySideRenderer.enabled = false;
		connector.enabled = false;
		springRigid.constraints = RigidbodyConstraints2D.FreezeAll;

		OnDestroyed?.Invoke();
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnPlayerTouched;
		Touch.onFingerMove -= OnPlayerHold;
		Touch.onFingerMove -= OnPlayerMidAirMove;
		Touch.onFingerUp -= OnPlayerUnTouched;
	}
}
