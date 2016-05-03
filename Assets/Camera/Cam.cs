using UnityEngine;
using System.Collections;
using Reign;

public class Cam : MonoBehaviour
{
	internal new Camera camera;

	void Start()
	{
		this.camera = GetComponent<Camera>();
	}

	void Update()
	{
		float width = Screen.width;
		float height = Screen.height;
		float near = camera.nearClipPlane;
		float far = camera.farClipPlane;
		float depth = far - near;
		float fov = camera.fieldOfView;
		
		float nearClip = near;
		float top = 1.0f / Mathf.Tan(MathUtilities.DegToRad(fov) * .5f);
		float aspect = ((width*.5f) / height);
		float right = top / aspect;

		var mat = new Matrix4x4();
		mat.m00 = right;
		mat.m01 = 0;
		mat.m02 = 0;
		mat.m03 = 0;

		mat.m10 = 0;
		mat.m11 = top;
		mat.m12 = 0;
		mat.m13 = 0;

		mat.m20 = 0;
		mat.m21 = 0;
		mat.m22 = -(far+near) / depth;
		mat.m23 = -(2*near*far) / depth;

		mat.m30 = 0;
		mat.m31 = 0;
		mat.m32 = -1;
		mat.m33 = 0;

		camera.projectionMatrix = mat;
	}
}
