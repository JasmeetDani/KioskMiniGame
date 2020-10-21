Shader "Custom/RedGlow"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_Speed ("Speed", Range(1.0, 6.0)) = 3.0
    }
    SubShader
    {
		Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 200
		Cull Front

        CGPROGRAM

		#pragma surface surf Lambert alpha:fade

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;

		uniform float _Speed;

		void surf (Input IN, inout SurfaceOutput o)
        {
			o.Emission = _Color.rgb;
			o.Alpha = abs(sin(_Time.y * _Speed)) / 2;
        }
        ENDCG
    }

    FallBack "Diffuse"
}