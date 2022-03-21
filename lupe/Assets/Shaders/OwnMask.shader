Shader "Custom/OwnMask"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader
	{
		Tags {"Queue"="Geometry" "RenderType"="Opaque"}
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input
		{
            float2 uv_MainTex;
            float3 worldPos;
        };

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		int _LensCount = 1;
		float4 _GLOBALMaskDirection[10];
		float3 _GLOBALMaskPosition[10];
		half _GLOBALMaskRadius[10];
		half _GLOBALMaskLength[10];



		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
		
		float magnitude(float2 vec)
		{
			if (vec.x < 0 || vec.y < 0)
			{
				return -length(vec);
			}
			return length(vec);
		}
		float4 Quat(float4 q1, float4 q2)
		{
			return float4(
				q2.xyz * q1.w + q1.xyz * q2.w + cross(q1.xyz, q2.xyz),
				q1.w * q2.w - dot(q1.xyz, q2.xyz)
				);
		}

		
		float3 RotateVector(float3 vec, float4 rot)
		{
			/*float3 TempVec3;
			float2 dirXZ = float2(dir.x,dir.z);
			float2 dirYZ = float2(dir.y, dir.z);
			TempVec3.x = vec.x * (length(dirYZ) * (dir.z / abs(dir.z))) + vec.z * -dir.x;
			TempVec3.y = vec.y * (length(dirXZ) * (dir.z / abs(dir.z))) + vec.z * -dir.y;
			TempVec3.z = vec.z * dir.z + vec.x * dir.x + vec.y * dir.y;
			
			float3 Rot;*/
	
			float4 Originalq = rot * float4(-1, -1, -1, -1);
			return Quat(rot, Quat(float4(vec, 0), Originalq)).xyz;
			

			
			/*float4 q;
			float3 forwardDir = float3(0,0,1);
			float3 a = float3(forwardDir.y * dir.z - forwardDir.z * dir.y, forwardDir.z * dir.x - forwardDir.x * dir.z, forwardDir.x * dir.y - forwardDir.y * dir.x);
			q.xyz = a;
			q.w = sqrt(pow(length(forwardDir), 2) * pow(length(dir), 2)) + (forwardDir.x * dir.x + forwardDir.y * dir.y + forwardDir.z * dir.z);

			return  q * vec;*/
		}



		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			

			float alpha = 0;

			for (int i = 0; i < _LensCount; i++)
			{
				float3 rotPos = RotateVector(IN.worldPos - _GLOBALMaskPosition[i], _GLOBALMaskDirection[i]);
				float2 pixelPos = rotPos;
				float2 objPos = _GLOBALMaskPosition[i];

				half radialDist = length(pixelPos);
				//half dist = rotPos.z - _GLOBALMaskPosition[i].z;

				half actualRadius = rotPos.z / _GLOBALMaskLength[i] *  _GLOBALMaskRadius[i];

				if (rotPos.z >= 0  && radialDist <= actualRadius)
				{
					alpha = 1;

					//c.r = 1;
				}
			}

			clip(alpha - 0.1); // esto hace la verdadera magia

			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
}
