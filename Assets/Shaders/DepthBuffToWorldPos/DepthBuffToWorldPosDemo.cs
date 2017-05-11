using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class DepthBuffToWorldPosDemo : PostProcess
{
	private new Camera camera;
	private new Transform transform;

	private void Start()
	{
		camera = GetComponent<Camera>();
		transform = GetComponent<Transform>();
	}

	protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetVector("camForward", transform.forward);
		
		//var clipToWorld = (camera.projectionMatrix * camera.worldToCameraMatrix).inverse;// << Is there a way to make this method work indead?
		// NOTE: code was ported from: https://gamedev.stackexchange.com/questions/131978/shader-reconstructing-position-from-depth-in-vr-through-projection-matrix
		// More clerification of whats going on is needed!
		var p = camera.projectionMatrix;
		p[2, 3] = p[3, 2] = 0.0f;
		p[3, 3] = 1.0f;
		var clipToWorld = Matrix4x4.Inverse(p * camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0, 0, -p[2,2]), Quaternion.identity, Vector3.one);
		material.SetMatrix("clipToWorld", clipToWorld);

		base.OnRenderImage(source, destination);
	}
}
