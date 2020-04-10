Shader "Custom/Wind"
{
	Properties
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15
		_ClipRect ("Clip Rect", vector) = (-32767, -32767, 32767, 32767)

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		
        // Wind effect params
        _WindFrecuency("Wind Frecuency",Range(0.001,100)) = 1
        _WindStrength("Wind Strength", Range( 0, 2 )) = 0.3
        _WindGustDistance("Distance between gusts",Range(0.001,50)) = .25
        _WindDirection("Wind Direction", vector) = (1,0, 1,0)
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
            #pragma multi_compile_local _ PIXELSNAP_ON

			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			half _WindFrecuency;
            half _WindGustDistance;
            half _WindStrength;
            float3 _WindDirection;
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
			};
			
			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.worldPosition = IN.vertex;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
                float4 localSpaceVertex = OUT.vertex;
                // Takes the mesh's verts and turns it into a point in world space
                // this is the equivalent of Transform.TransformPoint on the scripting side
                float4 worldSpaceVertex = mul( unity_ObjectToWorld, localSpaceVertex );
                 
                // Height of the vertex in the range (0,1)
                float height = 0.2;
                 
                worldSpaceVertex.x += sin( _Time.x * _WindFrecuency   * _WindGustDistance) * height * _WindStrength * _WindDirection.x;
                worldSpaceVertex.y += sin( _Time.x * _WindFrecuency  * _WindGustDistance) * height * _WindStrength * _WindDirection.y;
                 
                // takes the new modified position of the vert in world space and then puts it back in local space
                OUT.vertex = mul( unity_WorldToObject, worldSpaceVertex );
                    
				#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
				#endif
				
                #if defined(PIXELSNAP_ON)
                OUT.vertex = UnityPixelSnap (v.vertex);
                #endif
				
				OUT.color = IN.color * _Color;
				return OUT;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
				
				#if UNITY_UI_CLIP_RECT
					color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				#endif

				#ifdef UNITY_UI_ALPHACLIP
					clip (color.a - 0.001);
				#endif

				return color;
			}
		ENDCG
		}
	}
}