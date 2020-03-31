Shader "Custom/Paper" {

	Properties{
		[HideInInspector]_MainTex("Base (RGB)", 2D) = "white" {}
	_DetailTex("Detail", 2D) = "white" {}

	_Tint("Colorise Tint", Color) = (1, 1, 1, 0)
		_Alpha("Alpha", Float) = 1
		_Silhouette("Silhouette", Range(0, 1)) = 0

		_Hue("Hue Shift", Float) = 0
		_Sat("Saturation", Range(0, 1)) = 1
		_Val("Value", Range(0, 1)) = 1

		_WindTime("Wind Time", Float) = 0
		_WindScale("Wind Scale", Float) = 0

		_DistortScale("Distort Scale", Float) = 1
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
	float _Hue;
	float _Sat;
	float _Val;
	float4 _Tint;
	fixed _Alpha;
	float _Silhouette;

	float _WindTime;
	float _WindScale;

	float _DistortScale;

	// Colorisation helper method
	float3 colorise(float3 RGB, float4 target) {
		float grayscale = (RGB.r + RGB.g + RGB.b) / 3;
		float3 colorised = float3(target.r * grayscale, target.g * grayscale, target.b * grayscale);
		return lerp(RGB, colorised, target.a);
	}

	// Linear interpolation helper method
	float3 lerp(float3 input, float3 target, float amount) {
		float3 lerped;
		lerped.r = (input.r + (target.r - input.r) * amount);
		lerped.g = (input.g + (target.g - input.g) * amount);
		lerped.b = (input.b + (target.b - input.b) * amount);
		return lerped;
	}

	// HSV shift helper method
	float3 shift_col(float3 RGB, float3 shift) {
		float3 RESULT = float3(RGB);
		float a1 = shift.z*shift.y;
		float a2 = shift.x*3.14159265 / 180;
		float VSU = a1*cos(a2);
		float VSW = a1*sin(a2);

		RESULT.x = (.299*shift.z + .701*VSU + .168*VSW)*RGB.x
			+ (.587*shift.z - .587*VSU + .330*VSW)*RGB.y
			+ (.114*shift.z - .114*VSU - .497*VSW)*RGB.z;

		RESULT.y = (.299*shift.z - .299*VSU - .328*VSW)*RGB.x
			+ (.587*shift.z + .413*VSU + .035*VSW)*RGB.y
			+ (.114*shift.z - .114*VSU + .292*VSW)*RGB.z;

		RESULT.z = (.299*shift.z - .3*VSU + 1.25*VSW)*RGB.x
			+ (.587*shift.z - .588*VSU - 1.05*VSW)*RGB.y
			+ (.114*shift.z + .886*VSU - .203*VSW)*RGB.z;

		return (RESULT);
	}

	// Silhouette helper method
	float3 silhouette(float3 RGB, float3 amount) {
		float3 RESULT = float3(RGB);
		RESULT.x = RESULT.x + amount;
		RESULT.y = RESULT.y + amount;
		RESULT.z = RESULT.z + amount;
		return RESULT;
	}

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

		// Apply wind
		fixed yVal = (OUT.worldPos.y - 0.2) * .005;
		fixed sinOffset = OUT.worldPos.x * 0.5 + OUT.worldPos.z * 0.5;
		OUT.vertex.x += _WindScale * (sin(_WindTime + sinOffset) * yVal);

		return OUT;
	}

	// Fragment shader
	float4 frag(fragmentInput i) : COLOR{

		// Find the source color
		fixed2 coord = i.texcoord;

#if DISTORT_ON
	// Apply distortion
	float groundSmoothing = min(i.worldPos.y, 1);
	float distortOffset = i.worldPos.y * 3 + _Time * 20 * _DistortScale;
	coord.x += groundSmoothing * sin(distortOffset) * 0.03;
#endif

	fixed4 c = tex2D(_MainTex, coord) * i.color;

	// Apply paper detail texture
	fixed2 detailPos = (i.worldPos.xy - i.worldOrigin.xy) * 0.3;
	detailPos.y += i.worldPos.z * 0.3; // Fixes issue with horizontal sprites

	fixed detailShift = tex2D(_DetailTex, detailPos).r - 0.5;
	detailShift *= 1.5; // Scale up paper effect
	c += fixed4(detailShift, detailShift, detailShift, 0);

	// Apply colorisation
	c.rgb = colorise(c, _Tint);

	// Apply HSV shift
	float3 shift = float3(_Hue, _Sat, _Val);
	c.rgb = shift_col(c, shift);
	c.rgb = silhouette(c, _Silhouette);

	// Don't render any pixels below the floor plane
	fixed above0 = i.worldPos.y > 0;
	c.a = c.a*_Alpha*above0;

	return c;
	}
		ENDCG
	}

	}
		Fallback "Sprites/Default"
}