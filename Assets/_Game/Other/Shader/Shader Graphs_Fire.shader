Shader "Shader Graphs/Fire" {
	Properties {
		Vector2_60f130decf3b407db0e9db43c1910d02 ("NoiseScale", Vector) = (1,1,0,0)
		Vector1_881c86889db84594a032c6902935d30e ("DistortionStrength", Float) = 1
		Vector1_9d5f6b94e119402381bd7d848635532e ("FireBottomOffset", Float) = 1.3
		Vector1_01f5a180acbf40c18752c71c896e13e6 ("YStretch", Float) = 1
		[HDR] Color_e13fce6595364c49b070d9e4015e47c1 ("Color", Vector) = (0.5,0.09803922,0,0)
		Vector1_d970887c8c54403897571e9372b2bc7b ("Alpha", Float) = -0.88
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
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
	Fallback "Hidden/Shader Graph/FallbackError"
}