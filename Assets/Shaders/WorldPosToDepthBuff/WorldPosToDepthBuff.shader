Shader "Unlit/WorldPosToDepthBuff"
{
	Properties
	{
		_Falloff("Falloff", Range(0,1)) = 0.5
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float depth : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				//COMPUTE_EYEDEPTH(o.depth);// << Unity built in method
				o.depth = -mul(UNITY_MATRIX_V, mul(unity_ObjectToWorld, v.vertex)).z;

				return o;
			}

			float _Falloff;
			
			float4 frag (v2f i) : SV_Target
			{
				float4 color = i.depth * _Falloff;
				return color;
			}
			ENDCG
		}
	}
}
