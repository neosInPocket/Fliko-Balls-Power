using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystemController : MonoBehaviour
{
	[SerializeField] private string menuName;

	public void GetLoadGameScene()
	{
		SceneManager.LoadScene(menuName);
	}
}
