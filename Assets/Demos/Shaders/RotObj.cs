using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotObj : MonoBehaviour
{
	private new Transform transform;
	private float rot;

	private void Start()
	{
		transform = GetComponent<Transform>();
	}

	private void Update()
	{
		var pos = transform.position;
		pos.x = Mathf.Cos(rot);
		pos.z = Mathf.Sin(rot);
		pos.y = Mathf.Sin(rot * 2) * .5f;
		transform.position = pos;

		rot += Time.deltaTime * 1;
	}
}
