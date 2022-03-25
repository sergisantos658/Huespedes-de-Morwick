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
		float4x4 _GLOBALMaskTransform[10];
		

		
		float3 _GLOBALMaskPosition[10];
		half _GLOBALMaskRadius[10];
		half _GLOBALMaskLength[10];

		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			float squares = step(0.5, 1);

			float alpha = 0;

			for (int i = 0; i < _LensCount; i++)
			{
				float3 pixInLensIn0 = _GLOBALMaskPosition[i] - IN.worldPos;
				float3 pixLocalPos = mul(_GLOBALMaskTransform[i], pixInLensIn0);
				float2 radialPos = float2(pixLocalPos.x, pixLocalPos.y);

				half radialDist = length(radialPos);
				half dist = -pixLocalPos.z;

				half actualRadius = dist / _GLOBALMaskLength[i] * _GLOBALMaskRadius[i];

				if (dist >= 0 && radialDist <= actualRadius)
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
