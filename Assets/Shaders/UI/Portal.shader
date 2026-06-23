Shader "MobileGame/UI/Portal"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1, 1, 1, 1)
        _PortalColor ("Portal Color", Color) = (0.25, 0.75, 1, 1)
        _SecondaryColor ("Secondary Color", Color) = (0.55, 0.15, 1, 1)
        _Speed ("Speed", Range(-5, 5)) = 1
        _Swirl ("Swirl", Range(2, 16)) = 7
        _RingSize ("Ring Size", Range(0.1, 0.8)) = 0.28
        _Softness ("Softness", Range(0.001, 0.2)) = 0.06

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
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
            Name "Portal"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _PortalColor;
            fixed4 _SecondaryColor;
            float4 _ClipRect;
            float _Speed;
            float _Swirl;
            float _RingSize;
            float _Softness;

            v2f vert(appdata_t input)
            {
                v2f output;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                output.worldPosition = input.vertex;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.texcoord = input.texcoord;
                output.color = input.color * _Color;
                return output;
            }

            fixed4 frag(v2f input) : SV_Target
            {
                fixed4 sprite = tex2D(_MainTex, input.texcoord);
                float2 point = (input.texcoord - 0.5) * 2.0;
                float radius = length(point);
                float angle = atan2(point.y, point.x);
                float time = _Time.y * _Speed;

                float flowA = sin(angle * _Swirl - radius * 18.0 + time * 3.0);
                float flowB = sin(angle * (_Swirl + 3.0) + radius * 28.0 - time * 4.0);
                float energy = saturate(flowA * 0.35 + flowB * 0.2 + 0.55);

                float inner = smoothstep(_RingSize - _Softness, _RingSize + _Softness, radius);
                float outer = 1.0 - smoothstep(1.0 - _Softness, 1.0, radius);
                float ring = inner * outer;
                float rim = smoothstep(0.72, 0.92, radius) * outer;

                fixed3 portal = lerp(_SecondaryColor.rgb, _PortalColor.rgb, energy);
                fixed4 color;
                color.rgb = (portal * (0.65 + energy * 0.8) + rim * _PortalColor.rgb) * input.color.rgb;
                color.a = saturate(ring * (0.4 + energy * 0.75) + rim) * sprite.a * input.color.a;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(input.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip(color.a - 0.001);
                #endif

                return color;
            }
            ENDCG
        }
    }
}
