using System.Collections.Generic;
using UnityEngine;

public class ScreenBarriers : MonoBehaviour
{
	[SerializeField] private List<ScreenBarrier> barriers;
	[SerializeField] private float borderWidth;


	private void Start()
	{
		var screen = new InternalGameTools();
		var screenSize = screen.ScreenSize;

		barriers[0].Renderer.size = new Vector2(2 * screenSize.x, borderWidth);
		barriers[1].Renderer.size = new Vector2(2 * screenSize.x, borderWidth);
		barriers[2].Renderer.size = new Vector2(borderWidth, 2 * screenSize.y);
		barriers[3].Renderer.size = new Vector2(borderWidth, 2 * screenSize.y);

		barriers[0].transform.position = new Vector2(0, -screenSize.y - borderWidth / 2);
		barriers[1].transform.position = new Vector2(0, screenSize.y + borderWidth / 2);
		barriers[2].transform.position = new Vector2(screenSize.x + borderWidth / 2, 0);
		barriers[3].transform.position = new Vector2(-screenSize.x - borderWidth / 2, 0);
	}
}
