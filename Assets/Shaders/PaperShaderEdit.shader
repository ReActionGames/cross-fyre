Shader "Custom/Paper-edit" {

	Properties{
		[HideInInspector]_MainTex("Base (RGB)", 2D) = "white" {}
	_DetailTex("Detail", 2D) = "white" {}

		_Alpha("Alpha", Float) = 1
	}

		SubShader{
		// Configure as a Unity UI sprite
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" }
		ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off

		Pass{
		// Basic shader setup
		CGPROGRAM
#include "UnityCG.cginc"
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest

		// Distortion multi-comple feature
#pragma multi_compile DISTORT_OFF DISTORT_ON

		// Define vertex shader input struct
		struct vertexInput {
		float4 vertex   : POSITION;
		float4 color    : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	// Define fragment shader input struct
	struct fragmentInput {
		half2 texcoord  : TEXCOORD0;
		float4 vertex   : SV_POSITION;
		fixed4 color : COLOR;
		float3 worldPos : TEXCOORD1;
		float3 worldOrigin : TEXCOORD3;
	};

	// Declare variables
	sampler2D _MainTex;
	sampler2D _DetailTex;
	float4 _MainTex_TexelSize;
	fixed _Alpha;

	


	// Vertex shader
	fragmentInput vert(vertexInput IN) {

		// Copy basic data over
		fragmentInput OUT;
		OUT.vertex = UnityObjectToClipPos(IN.vertex);
		OUT.texcoord = IN.texcoord;
		OUT.color = IN.color;

		// Calculate extra location data
		OUT.worldPos = mul(unity_ObjectToWorld, IN.vertex).xyz;
		OUT.worldOrigin = mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;
		
		return OUT;
	}

	// Fragment shader
	float4 frag(fragmentInput i) : COLOR{

		// Find the source color
		fixed2 coord = i.texcoord;


	fixed4 c = tex2D(_MainTex, coord) * i.color;

	// Apply paper detail texture
	fixed2 detailPos = (i.worldPos.xy - i.worldOrigin.xy) * 0.3;
	detailPos.y += i.worldPos.z * 0.3; // Fixes issue with horizontal sprites

	fixed detailShift = tex2D(_DetailTex, detailPos).r - 0.5;
	detailShift *= 1.5; // Scale up paper effect
	c += fixed4(detailShift, detailShift, detailShift, 0);


	return c;
	}
		ENDCG
	}

	}
		Fallback "Sprites/Default"
}