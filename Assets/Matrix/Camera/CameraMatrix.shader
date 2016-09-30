Shader "Unlit/CameraMatrix"
{
	Properties
	{
		_Color ("Color", Color) = (1, 1, 1, 1)
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
				float4 vertex : SV_POSITION;
			};

			float4 _Color;
			//float4x4 camera;
			float4x4 viewMatrix, projMatrix;
			
			v2f vert (appdata v)
			{
				v2f o;
				//o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				//float4x4 camera = mul(viewMatrix, projMatrix);
				//projMatrix[2][3] = UNITY_MATRIX_P[2][3];
				float4x4 camera = mul(projMatrix, viewMatrix);
				//float4x4 camera = mul(UNITY_MATRIX_V, UNITY_MATRIX_P);
				//float4x4 camera = mul(UNITY_MATRIX_P, UNITY_MATRIX_V);
				o.vertex = mul(camera, v.vertex);
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				const int x = 2;
				const int y = 3;
				float value = projMatrix[x][y] / UNITY_MATRIX_P[x][y];
				if (UNITY_MATRIX_V[x][y] != 0.0) return float4(1, 1, 1, 1) * value;
				else return float4(1, 0, 0, 1);
				return _Color;
			}
			ENDCG
		}
	}
}
