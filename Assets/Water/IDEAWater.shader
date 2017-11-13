Shader "IDEA Lab/IDEAWater" {
	Properties{
		_WaveScale("Wave scale", Range(0.02,0.15)) = 0.063
		[NoScaleOffset] _BumpMap("Normalmap ", 2D) = "bump" {}
		WaveSpeed("Wave speed (map1 x,y; map2 x,y)", Vector) = (19,9,-16,-7)
		[NoScaleOffset] _ReflectiveColor("Reflective color (RGB) fresnel (A) ", 2D) = "" {}
		_HorizonColor("Simple water horizon color", COLOR) = (.172, .463, .435, 1)

		_FadeStart("Fade Starting", Range(0.001, 10)) = 3
		_FadeDist("Fade Distance", Range(0.001, 100)) = 10
		_ColorMap("Color Map", 2D) = "white" {}
		_ColorCorrection("Color Correction Value", Range(0, 1)) = 1
		
	}


	// -----------------------------------------------------------
	// Fragment program cards


	Subshader{
		Tags {
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"ForceNoShadow" = "True"
			"PreviewType" = "Plane"
		}

		LOD 100
		ZWrite Off Blend SrcAlpha OneMinusSrcAlpha

	Pass {
		CGPROGRAM
		// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members worldPos)
		//#pragma exclude_renderers d3d11
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		uniform float4 _WaveScale4;
		uniform float4 _WaveOffset;

		struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 pos : SV_POSITION;
			float2 bumpuv0 : TEXCOORD0;
			float2 bumpuv1 : TEXCOORD1;
			float3 viewDir : TEXCOORD2;

			float4 worldPos : TEXCOORD3;
			float2 texcoord : TEXCOORD4;

		};

		v2f vert(appdata v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);

			// scroll bump waves
			float4 temp;
			float4 wpos = mul(unity_ObjectToWorld, v.vertex);
			temp.xyzw = wpos.xzxz * _WaveScale4 + _WaveOffset;
			o.bumpuv0 = temp.xy;
			o.bumpuv1 = temp.wz;

			// object space view direction (will normalize per pixel)
			o.viewDir.xzy = WorldSpaceViewDir(v.vertex);

			o.worldPos = wpos;
			o.texcoord = v.texcoord;

			return o;
		}

		sampler2D _ReflectiveColor;
		uniform float4 _HorizonColor;
		sampler2D _BumpMap;

		float _FadeDist;
		float _FadeStart;
		sampler2D _ColorMap;
		float _ColorCorrection;

		half4 frag(v2f i) : SV_Target {
			i.viewDir = normalize(i.viewDir);

			// combine two scrolling bumpmaps into one
			half3 bump1 = UnpackNormal(tex2D(_BumpMap, i.bumpuv0)).rgb;
			half3 bump2 = UnpackNormal(tex2D(_BumpMap, i.bumpuv1)).rgb;
			half3 bump = (bump1 + bump2) * 0.5;
			
			// fresnel factor
			half fresnelFac = dot(i.viewDir, bump);

			// final color is between refracted and reflected based on fresnel
			half4 color;

			half4 water = tex2D(_ReflectiveColor, float2(fresnelFac,fresnelFac));
			color.rgb = lerp(water.rgb, _HorizonColor.rgb, water.a);
			color.a = _HorizonColor.a;

			// Apply color map over the top
			half4 colorMap = (tex2D(_ColorMap, i.texcoord) * _ColorCorrection);
			color += colorMap;

			// Fade alpha based on distance from WorldOrigin
			float len = length(i.worldPos.xyz);
			
			if (len > _FadeStart) {
				float dist = (len - _FadeStart) / _FadeDist;
				color.a = lerp(1, 0, dist);
				//color.r = 1;			// Debug
			}

			else
				color.a = _HorizonColor.a;

			return color;

		}

		ENDCG

		}
	}

}
