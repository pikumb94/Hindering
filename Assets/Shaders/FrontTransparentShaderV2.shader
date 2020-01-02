// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/FrontTransparentShaderV2" {/*

	Properties{
		_MainTex("Texture", 2D) = "white" {}
	  _ColorTint("Tint", Color) = (0, 0, 0, 1)
	}
		SubShader{
			Tags {  "Queue" = "Geometry+1" "RenderType" = "Opaque" }
			LOD 200

			ZTest Off
			ZWrite Off

			CGPROGRAM
			#pragma surface surf Lambert

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
			};

			fixed4 _ColorTint;
			void mycolor(Input IN, SurfaceOutput o, inout fixed4 color)
			{
				color *= _ColorTint;
			}

			void surf(Input IN, inout SurfaceOutput o) {
				half4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;
				o.Alpha = c.a;

			}
			ENDCG
	}
		FallBack "Diffuse"
*/
	
		Properties
		{
			_MainTex("Texture", 2D) = "white" {}
			_Color0("Color Bottom Left", Color) = (1,1,1,1)
			_Color1("Color Bottom Right", Color) = (1,1,1,1)
			_Color2("Color Top Left", Color) = (1,1,1,1)
			_Color3("Color Top Right", Color) = (1,1,1,1)
		}
			SubShader
			{
				Tags
				{
					"Queue" = "Geometry+1"
					"RenderType" = "Opaque"
					"PreviewType" = "Plane"
				}

				ZTest Off
				ZWrite Off
				Blend SrcAlpha OneMinusSrcAlpha

				Pass
				{
					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma multi_compile_instancing
					#include "UnityCG.cginc"

					struct appdata
					{
						float4 vertex : POSITION;
						float2 uv : TEXCOORD0;
						UNITY_VERTEX_INPUT_INSTANCE_ID
					};

					struct v2f
					{
						float4 vertex : SV_POSITION;
						float4 uv : TEXCOORD0;
						UNITY_VERTEX_INPUT_INSTANCE_ID
					};

					sampler2D _MainTex;

					UNITY_INSTANCING_BUFFER_START(Props)
					UNITY_DEFINE_INSTANCED_PROP(float4, _MainTex_ST)
					UNITY_DEFINE_INSTANCED_PROP(half4, _Color0)
					UNITY_DEFINE_INSTANCED_PROP(half4, _Color1)
					UNITY_DEFINE_INSTANCED_PROP(half4, _Color2)
					UNITY_DEFINE_INSTANCED_PROP(half4, _Color3)
					UNITY_INSTANCING_BUFFER_END(Props)

					v2f vert(appdata v)
					{
						UNITY_SETUP_INSTANCE_ID(v);
						v2f o;
						UNITY_TRANSFER_INSTANCE_ID(v, o);
						o.vertex = UnityObjectToClipPos(v.vertex);
						float4 _uvRect = UNITY_ACCESS_INSTANCED_PROP(Props, _MainTex_ST);
						o.uv.xy = v.uv.xy * _uvRect.xy + _uvRect.zw;
						o.uv.zw = v.uv.xy;
						return o;
					}

					fixed4 frag(v2f i) : SV_Target
					{
						UNITY_SETUP_INSTANCE_ID(i);
						half4 _color = lerp(
							lerp(UNITY_ACCESS_INSTANCED_PROP(Props, _Color0), UNITY_ACCESS_INSTANCED_PROP(Props, _Color1), i.uv.z),
							lerp(UNITY_ACCESS_INSTANCED_PROP(Props, _Color2), UNITY_ACCESS_INSTANCED_PROP(Props, _Color3), i.uv.z),
							i.uv.w);
						return tex2D(_MainTex, i.uv.xy) * _color;
					}
					ENDCG
				}
			}
}