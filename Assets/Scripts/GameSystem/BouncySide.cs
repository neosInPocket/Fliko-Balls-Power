using UnityEngine;

public class BouncySide : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Color restrictColor;
	[SerializeField] private PlayerSpring playerSpring;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		spriteRenderer.color = restrictColor;
		playerSpring.Restricted = true;
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		spriteRenderer.color = Color.white;
		playerSpring.Restricted = false;
	}
}
