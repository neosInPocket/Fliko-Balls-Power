using UnityEngine;

public class RockSpawner : MonoBehaviour
{
	[SerializeField] private ROck rockPrefab;
	[SerializeField] private ROck startRock;
	[SerializeField] private float spawnDistance;
	[SerializeField] private float playerPointerOffset;
	[SerializeField] private PlayerSpring playerSpring;
	[SerializeField] private float xOffset;
	private ROck lastRock;
	private InternalGameTools screen;

	private void Start()
	{
		screen = new InternalGameTools();
		lastRock = startRock;
	}

	private void Update()
	{
		if (playerSpring.transform.position.y + playerPointerOffset > lastRock.transform.position.y)
		{
			var randomX = Random.Range(-screen.ScreenSize.x + xOffset, screen.ScreenSize.x - xOffset);
			var yPosition = lastRock.transform.position.y + spawnDistance;
			var rock = Instantiate(rockPrefab, new Vector2(randomX, yPosition), Quaternion.identity, transform);
			lastRock = rock;
		}
	}
}
