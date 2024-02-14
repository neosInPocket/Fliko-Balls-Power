using System.Data.SqlTypes;
using UnityEngine;

public class Ground : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private float yOffsetValue;
	[SerializeField] private PlayerSpring playerSpring;

	private void Start()
	{
		var tools = new InternalGameTools();


		var height = 2 * tools.ScreenSize.y * yOffsetValue;
		var xSize = 2 * tools.ScreenSize.x;
		spriteRenderer.size = new Vector2(xSize, height);
		transform.position = new Vector2(0, -tools.ScreenSize.y + height / 2);

		playerSpring.ChangePositionFromBottomPivot(new Vector2(0, -tools.ScreenSize.y + height + playerSpring.Radius));
	}
}

public class InternalGameTools
{
	public Vector2 ScreenSize { get; private set; }

	public InternalGameTools()
	{
		ScreenSize = GetWorldPoint(new Vector3(Screen.width, Screen.height));
	}

	public Vector3 GetWorldPoint(Vector2 screenPosition)
	{
		var screenPointToRay = Camera.main.ScreenPointToRay(screenPosition);

		var direc = screenPointToRay.direction;
		var orig = screenPointToRay.origin;

		Vector3 normalVector2 = new Vector3(0, 0, 1);
		Vector3 pointVector2 = new Vector3(0, 0, 0);

		float cross = Vector3.Dot(direc, normalVector2);

		float distance = Vector3.Dot(pointVector2 - orig, normalVector2) / cross;

		Vector3 rslt = orig + distance * direc;
		return rslt;
	}
}
