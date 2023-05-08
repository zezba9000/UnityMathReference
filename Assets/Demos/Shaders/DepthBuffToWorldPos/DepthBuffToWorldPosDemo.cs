using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class DepthBuffToWorldPosDemo : PostProcess
{
	private new Camera camera;

	private void Start()
	{
		camera = GetComponent<Camera>();
	}

	protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		var viewMatrix = camera.worldToCameraMatrix;
		var projectionMatrix = camera.projectionMatrix;
		projectionMatrix = GL.GetGPUProjectionMatrix(projectionMatrix, false);
		var clipToPos = (projectionMatrix * viewMatrix).inverse;
		material.SetMatrix("clipToWorld", clipToPos);

		base.OnRenderImage(source, destination);
	}
}
