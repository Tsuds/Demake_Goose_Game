using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
	private int targetFrameRate = 60;

	private void Start()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = targetFrameRate;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

}
