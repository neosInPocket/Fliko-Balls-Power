using System;
using UnityEngine;

public class GuideController : MonoBehaviour
{
	public Action GuideCompleted { get; set; }

	public void Guide()
	{
		gameObject.SetActive(true);
	}
}
