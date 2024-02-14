using UnityEngine;
using Vector2 = UnityEngine.Vector2;


public class ROck : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Vector2 randomSizes;
	[SerializeField] private Color achievedColor;
	public bool Achieved
	{
		get => achieved;
		set
		{
			achieved = value;
			if (value)
			{
				spriteRenderer.color = achievedColor;
			}
		}
	}

	private bool achieved;

	private void Start()
	{
		var size = Random.Range(randomSizes.x, randomSizes.y);
		spriteRenderer.size = new Vector2(size, size);

		var randomZ = Random.Range(0, 360f);

		transform.rotation = Quaternion.Euler(new Vector3(0, 0, randomZ));
	}
}
