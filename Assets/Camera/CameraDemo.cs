using UnityEngine;
using System.Collections;

public class CameraDemo : MonoBehaviour
{
	internal new Transform transform;

	void Start()
	{
		this.transform = GetComponent<Transform>();
	}
	
	void Update()
	{
		transform.Rotate(new Vector3(1, 1, 1).normalized, 1);
	}
}
