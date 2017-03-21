Shader "PlumeShader"
{
	Properties
	{
		_Color ("Main Color", Color) = (1, 1, 1, 1)
		_ShadowColor ("Shadow Color", Color) = (0.8, 0.8, 1, 1)
		_EdgeThickness ("Outline Thickness", Float) = 1
		
		_MainTex ("Diffuse", 2D) = "white" {}
		_FalloffSampler ("Falloff Control", 2D) = "white" {}
		_RimLightSampler ("RimLight Control", 2D) = "white" {}
		
		
		_Color2 ("Color", Color) = (1,1,1,1)
        _Glow ("Intensity", Range(0, 3)) = 0

	}

	SubShader
	{
		Tags
		{
			"RenderType"="Opaque"
			"Queue"="Geometry"
			"LightMode"="ForwardBase"
		}

		Pass
		{
			Cull Back
			ZTest LEqual
			
			CGPROGRAM
				#pragma multi_compile_fwdbase
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				#include "AutoLight.cginc"
				#include "CharaSkin.cg"
			ENDCG			
		}
		
		Pass
		{
			Cull Front
			ZTest Less

			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				#include "CharaOutline.cg"
			ENDCG
		}
		
		Pass 
		{
			Blend One One
			Cull Back
			ZTest LEqual
			
			CGPROGRAM
				#pragma multi_compile_fwdbase
                #pragma vertex vert
                #pragma fragment frag

				
                sampler2D _MainTex;
                half4 _MainTex_ST;
                fixed4 _Color2;
                half _Glow;

                struct vertIn {
                    float4 pos : POSITION;
                    half2 tex : TEXCOORD0;
                };

                struct v2f {
                    float4 pos : SV_POSITION;
                    half2 tex : TEXCOORD0;
                };

                v2f vert (vertIn v) {
                    v2f o;
                    o.pos = mul(UNITY_MATRIX_MVP, v.pos);
                    o.tex = v.tex * _MainTex_ST.xy + _MainTex_ST.zw;
                    return o;
                }

                fixed4 frag (v2f f) : SV_Target {
                    fixed4 col = tex2D(_MainTex, f.tex);
					col *= _Color2;
                    col *= _Glow;
                    return col;
                }
				
            ENDCG
        }
	}

	FallBack "Transparent/Cutout/Diffuse"
}
