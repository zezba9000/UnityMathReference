Shader "Hidden/DepthBuffToWorldPos"
{
	Properties
	{
		
	}

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 5.0
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;

				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float3 worldDirection : TEXCOORD1;

				float4 vertex : SV_POSITION;
			};

			float4x4 clipToWorld;

			v2f vert (appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				float4 clip = float4(o.vertex.xy, 0.0, 1.0);
				o.worldDirection = mul(clipToWorld, clip) - _WorldSpaceCameraPos;

				return o;
			}
			
			sampler2D_float _CameraDepthTexture;
			float4 _CameraDepthTexture_ST;

			float4 frag (v2f i) : SV_Target
			{
				float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv.xy);
				depth = LinearEyeDepth(depth);
				float3 worldspace = i.worldDirection * depth + _WorldSpaceCameraPos;

				float4 color = float4(worldspace, 1.0);
				return color;
			}
			ENDCG
		}
	}
}
