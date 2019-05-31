using UnityEngine;
using System.Collections;

namespace UnityMathReference
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[ImageEffectAllowedInSceneView]
	public class PostProcess : MonoBehaviour
	{
		public Material material;
 
		protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination, material);
		}
	}
}