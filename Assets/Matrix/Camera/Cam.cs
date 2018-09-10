using UnityEngine;
using System.Collections;
using UnityMathReference;

public class Cam : MonoBehaviour
{
	internal new Camera camera;
	internal new Transform transform;
	public Camera unityCamera;
	public bool isProj = true;
	public Material cameraMatrixMaterial;

	void Start()
	{
		this.camera = GetComponent<Camera>();
		this.transform = GetComponent<Transform>();
	}

	void Update()
	{
		if (Input.GetMouseButton(0)) isProj = !isProj;

		float width = Screen.width;
		float height = Screen.height;
		float near = camera.nearClipPlane;
		float far = camera.farClipPlane;
		float fov = camera.fieldOfView;
		
		if (isProj)
		{
			unityCamera.orthographic = false;
			camera.projectionMatrix = CreateProjection(near, far, fov, width * .5f, height);
		}
		else
		{
			unityCamera.orthographic = true;
			camera.projectionMatrix = ProjectionOrthographicCentered(near, far, width * .5f, height);
		}

		camera.worldToCameraMatrix = CreateView_LeftHanded(transform.position, transform.forward, transform.up);
		cameraMatrixMaterial.SetMatrix("camera", GL.GetGPUProjectionMatrix(camera.projectionMatrix, true) * camera.worldToCameraMatrix);
	}

	public static Matrix4x4 ProjectionOrthographicCentered(float near, float far, float width, float height)
	{
		return OrthographicCentered(0, width, 0, height, near, far);
	}

	public static Matrix4x4 OrthographicCentered(float left, float right, float bottom, float top, float near, float far)
	{
		float width = right - left;
		float height = top - bottom;
		float depth = far - near;

		Matrix4x4 result;
		result.m00 = (2/width);
		result.m01 = 0;
		result.m02 = 0;
		result.m03 = 0;

		result.m10 = 0;
		result.m11 = (2/height);
		result.m12 = 0;
		result.m13 = 0;

		result.m20 = 0;
		result.m21 = 0;
		result.m22 = (-2)/depth;
		result.m23 = -((far+near)/depth);

		result.m30 = 0;
		result.m31 = 0;
		result.m32 = 0;
		result.m33 = 1;
		return result;
	}

	Matrix4x4 CreateProjection(float near, float far, float fov, float width, float height)
	{
		float depth = far - near;
		float top = 1.0f / Mathf.Tan(MathUtilities.DegToRad(fov) * .5f);
		float aspect = (width / height);
		float right = top / aspect;

		var result = new Matrix4x4();
		result.m00 = right;
		result.m01 = 0;
		result.m02 = 0;
		result.m03 = 0;

		result.m10 = 0;
		result.m11 = top;
		result.m12 = 0;
		result.m13 = 0;

		result.m20 = 0;
		result.m21 = 0;
		result.m22 = -(far+near) / depth;
		result.m23 = -(2*near*far) / depth;

		result.m30 = 0;
		result.m31 = 0;
		result.m32 = -1;
		result.m33 = 0;
		return result;
	}

	Matrix4x4 CreateView_LeftHanded(Vec3 position, Vec3 forward, Vec3 up)
	{
		var right = forward.Cross(up);
		up = right.Cross(forward);

		var result = new Matrix4x4();
		result.m00 = -right.x;
		result.m01 = -right.y;
		result.m02 = -right.z;
		result.m03 = position.Dot(right);

		result.m10 = up.x;
		result.m11 = up.y;
		result.m12 = up.z;
		result.m13 = position.Dot(-up);

		result.m20 = -forward.x;
		result.m21 = -forward.y;
		result.m22 = -forward.z;
		result.m23 = position.Dot(forward);

		result.m30 = 0;
		result.m31 = 0;
		result.m32 = 0;
		result.m33 = 1;
		return result;
	}

	Matrix4x4 CreateView_RightHanded(Vec3 position, Vec3 forward, Vec3 up)
	{
		var right = forward.Cross(up);
		up = right.Cross(forward);

		var result = new Matrix4x4();
		result.m00 = right.x;
		result.m01 = right.y;
		result.m02 = right.z;
		result.m03 = position.Dot(-right);

		result.m10 = up.x;
		result.m11 = up.y;
		result.m12 = up.z;
		result.m13 = position.Dot(-up);

		result.m20 = forward.x;
		result.m21 = forward.y;
		result.m22 = forward.z;
		result.m23 = position.Dot(forward);

		result.m30 = 0;
		result.m31 = 0;
		result.m32 = 0;
		result.m33 = 1;
		return result;
	}
}
