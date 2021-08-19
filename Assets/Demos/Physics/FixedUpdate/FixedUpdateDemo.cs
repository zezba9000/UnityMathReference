using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedUpdateDemo : MonoBehaviour
{
	private float lastTime;

	private void Update()
	{
		// calculate accurate fixed-update
		float time = lastTime + Time.deltaTime;
		while (time >= Time.fixedDeltaTime)
		{
			FixedUpdate_Custom();
			time -= Time.fixedDeltaTime;
		}
		if (time < 0) time = 0;
		lastTime = time;

		// run normal update methods
		Debug.Log("Update");
	}

	private void FixedUpdate_Custom()
	{
		Debug.Log("FixedUpdate_Custom");
	}

	private void FixedUpdate()
	{
		Debug.Log("FixedUpdate");
	}
}
