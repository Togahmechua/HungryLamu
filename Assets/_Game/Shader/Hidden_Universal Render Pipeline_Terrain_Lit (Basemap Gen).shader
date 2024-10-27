Shader "Hidden/Universal Render Pipeline/Terrain/Lit (Basemap Gen)" {
	Properties {
		[HideInInspector] [PerRendererData] _NumLayersCount ("Total Layer Count", Float) = 1
		[HideInInspector] _Control ("AlphaMap", 2D) = "" {}
		[HideInInspector] _Splat0 ("Layer 0 (R)", 2D) = "white" {}
		[HideInInspector] _Splat1 ("Layer 1 (G)", 2D) = "white" {}
		[HideInInspector] _Splat2 ("Layer 2 (B)", 2D) = "white" {}
		[HideInInspector] _Splat3 ("Layer 3 (A)", 2D) = "white" {}
		[HideInInspector] _Mask3 ("Mask 3 (A)", 2D) = "grey" {}
		[HideInInspector] _Mask2 ("Mask 2 (B)", 2D) = "grey" {}
		[HideInInspector] _Mask1 ("Mask 1 (G)", 2D) = "grey" {}
		[HideInInspector] _Mask0 ("Mask 0 (R)", 2D) = "grey" {}
		[HideInInspector] [Gamma] _Metallic0 ("Metallic 0", Range(0, 1)) = 0
		[HideInInspector] [Gamma] _Metallic1 ("Metallic 1", Range(0, 1)) = 0
		[HideInInspector] [Gamma] _Metallic2 ("Metallic 2", Range(0, 1)) = 0
		[HideInInspector] [Gamma] _Metallic3 ("Metallic 3", Range(0, 1)) = 0
		[HideInInspector] _Smoothness0 ("Smoothness 0", Range(0, 1)) = 1
		[HideInInspector] _Smoothness1 ("Smoothness 1", Range(0, 1)) = 1
		[HideInInspector] _Smoothness2 ("Smoothness 2", Range(0, 1)) = 1
		[HideInInspector] _Smoothness3 ("Smoothness 3", Range(0, 1)) = 1
		[HideInInspector] _DstBlend ("DstBlend", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}