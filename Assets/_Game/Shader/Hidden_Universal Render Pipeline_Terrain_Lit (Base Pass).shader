Shader "Hidden/Universal Render Pipeline/Terrain/Lit (Base Pass)" {
	Properties {
		_BaseColor ("Color", Vector) = (1,1,1,1)
		_MainTex ("Albedo(RGB), Smoothness(A)", 2D) = "white" {}
		_MetallicTex ("Metallic (R)", 2D) = "black" {}
		[HideInInspector] _TerrainHolesTexture ("Holes Map (RGB)", 2D) = "white" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Hidden/Universal Render Pipeline/FallbackError"
}