Shader "FergusonTV/Shadow"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_Position ("Position", Vector) = (0,0,0,1)
		_LightPos ("Light Position", Vector) = (0,0,0,1)
		_LightAtt ("Light Attenuation", Float) = 20
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

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Shadow vertex:vert
		#pragma multi_compile DUMMY PIXELSNAP_ON

		sampler2D _MainTex;
		fixed4 _Color;
		float4 _Position;
		float4 _LightPos;
		float _LightAtt;

		struct Input
		{
			float2 uv_MainTex;
			fixed4 color;
		};
		
		half4 LightingShadow(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			half NdotL = dot (s.Normal, lightDir);
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten * 2);
			c.a = s.Alpha;
			return c;
		}
		
		void vert (inout appdata_full v, out Input o)
		{
			#if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
			v.vertex = UnityPixelSnap (v.vertex);
			#endif
			
			float4 LightDir = normalize(_LightPos - _Position);
			
			float y;
			if (LightDir.y > 0)
			{
				y = 1;
			}
			else
			{
				y = -1;
			}
			
			// v.vertex = float4(
				// atan2(LightDir.y, LightDir.x),
				// v.vertex.y * (LightDir.y > 0 ? 1 : -1),
				// v.vertex.z,
				// 1
			// );
			
			v.vertex.y *= y * (1 -clamp(LightDir.z, 0, 1));
			
			v.vertex.x = v.vertex.x + v.vertex.y*fmod((atan2(LightDir.y, LightDir.x) - y * 3.1415/2), 3.1415*2);
			// if (LightDir.y >= 0)
			// {
				// v.vertex.x = v.vertex.x + v.vertex.y * fmod((atan2(LightDir.y, LightDir.x) - 3.1415/2), 3.1415*2);// + atan2(LightDir.y, LightDir.x); //* (1 - (v.vertex.y + 1) * 0.5) / v.vertex.w;
			// }
			// else
			// {
			// }
			
			v.normal = float3(0,0,-1);
			
			UNITY_INITIALIZE_OUTPUT(Input, o);
			//o.color = v.color * _Color;
			//o.color = LightDir;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed c = tex2D(_MainTex, IN.uv_MainTex).a;
			o.Albedo = IN.color.rgb;
			o.Alpha = c * (distance(_LightPos.xy, _Position.xy) - _LightAtt)/_LightAtt;
		}
		ENDCG
	}

Fallback "Transparent/VertexLit"
}
