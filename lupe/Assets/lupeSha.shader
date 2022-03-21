Shader "Custom/lupeSha"
{
    Properties
    {
        _Mask("Mask", 2D) = "white" {}
        _Magnify("Magnify", Float) = 1
        _UVOffset("UVCenterOffset", Vector) = (0,0,0,1)
    }
 
        SubShader
        {
            Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
            Cull Off ZWrite Off ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha

            GrabPass{ "_GrabTexture" }

            Pass
                {

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float4 uv : TEXCOORD0;
                    float4 uv2 : TEXCOORD1;
                };

                struct v2f
                {
                    //our vertex position after projection
                    float4 vertex : SV_POSITION;

                    //our UV coordinate on the GrabTexture
                    float4 uv : TEXCOORD0;
                    float2 uv2 : TEXCOORD1;
                };

                float4 _Mask_ST;
                sampler2D _GrabTexture;
                sampler2D _Mask;
                half _Magnify;
                float4 _UVOffset;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    //the UV coordinate of our object's center on the GrabTexture
                    float4 uv_center = ComputeGrabScreenPos(UnityObjectToClipPos(float4(0, 0, 0, 1)));

                    uv_center += _UVOffset;
                    //the vector from uv_center to our UV coordinate on the GrabTexture
                    float4 uv_diff = ComputeGrabScreenPos(o.vertex) - uv_center;
                    //apply magnification
                    uv_diff /= _Magnify;
                    //save result
                    o.uv = uv_center + uv_diff;
                    o.uv2 = v.uv;//TRANSFORM_TEX(v.uv2, _Mask);
                    return o;
                }

                fixed4 frag(v2f i) : COLOR
                {
                    fixed4 albedo = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uv));
                    fixed4 mask = tex2D(_Mask, i.uv2);
                    return albedo *= mask;
                }
                ENDCG
            }
        }
}
