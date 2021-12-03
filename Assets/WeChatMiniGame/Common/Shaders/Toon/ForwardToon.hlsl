
#ifndef __TOON_LIGHTING_HLSL_
    #define __TOON_LIGHTING_HLSL_
    
    #include "../ForwardLit/math.hlsl"
    #include "./toonUtils.hlsl"
    #ifndef PI 
        #define PI 3.141592654
    #endif

    // BaseColor Props
    half _Cutoff;
    uniform sampler2D _NormalMap;
    uniform sampler2D _BaseMap;
    uniform float4 _BaseColor;
    uniform sampler2D _1st_ShadeMap;
    uniform float4 _1st_ShadeColor;
    uniform sampler2D _2nd_ShadeMap;
    uniform float4 _2nd_ShadeColor;

    // Feather
    #if defined (EnableGradeMap)
        uniform sampler2D _ShadingGradeMap;
        uniform float _Tweak_ShadingGradeMapLevel;

        uniform float _1st_ShadeColor_Step;
        uniform float _1st_ShadeColor_Feather;
        uniform fixed _Is_1st_ShadeColorOnly;
        uniform float _2nd_ShadeColor_Step;
        uniform float _2nd_ShadeColor_Feather;
    #else
        uniform float _BaseColor_Step;
        uniform float _BaseShade_Feather;
        uniform sampler2D _Set_1st_ShadePosition;
        uniform float _ShadeColor_Step;
        uniform float _1st2nd_Shades_Feather;
        uniform sampler2D _Set_2nd_ShadePosition;
    #endif

    // System Shadow
    uniform fixed _SetSystemShadowsToBase;
    uniform float _TweakSystemShadowsLevel;

    // HighColor
#if defined (EnableHighLight)
    uniform float4 _HighColor;
    uniform sampler2D _HighColor_Tex; 
    uniform float _HighColor_Power;
    uniform float _Is_SpecularToHighColor;
    uniform float _TweakHighColorOnShadow;
    uniform sampler2D _HighColorMask;
    uniform float _Tweak_HighColorMaskLevel;
#endif

    // RimLight
#if defined (EnableRimLight)
    uniform float4 _RimLightColor;
    uniform float _RimLight_Power;
    uniform float _RimLight_InsideMask;
    uniform float _LightDirection_MaskOn;
    uniform float _Tweak_LightDirection_MaskLevel;
    uniform float4 _Ap_RimLightColor;
    uniform float _Ap_RimLight_Power;
    uniform sampler2D _Set_RimLightMask; 
    uniform float _Tweak_RimLightMaskLevel;
#endif

    //Matcap
#if defined (EnableMatCap)
    uniform sampler2D _MatCapTex; 
    uniform float4 _MatCapColor;
    uniform float _Tweak_MatCapUV;
    uniform float _Rotate_MatCapUV;
    uniform float _TweakMatCapOnShadow;
    uniform fixed _Is_UseTweakMatCapOnShadow;
    uniform sampler2D _MatCapMask;
    uniform float _Tweak_MatcapMaskLevel;
#endif

    //Emission
    uniform sampler2D _EmissiveTex;
    uniform float4 _EmissiveColor;

    // GI
    uniform float _GI_Intensity;

    // Angle Ring
#if defined (EnableAngleRing)
    uniform sampler2D _AngelRingTex;
    uniform float4 _AngelRingColor;
    uniform float _AR_OffsetU;
    uniform float _AR_OffsetV;
    uniform fixed _ARTex_AlphaOn;
#endif

    struct a2v {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        float4 tangent : TANGENT;
        float4 texcoord : TEXCOORD0;
        float4 texcoord1: TEXCOORD1;
    };

    struct v2f {
        float4 pos : SV_POSITION;
        float2 uv : TEXCOORD0;
        float2 uv1: TEXCOORD1;
        float3 normalWS: TEXCOORD2;
        float3 lightDirWS: TEXCOORD4;
        float3 tangentWS: TEXCOORD5;
        float3 bitangentWS: TEXCOORD6;
        float4 positionWS: TEXCOORD7;
        SHADOW_COORDS(8)
        UNITY_FOG_COORDS(9)
    };

    v2f vert(a2v v) {

        v2f o;

        o.pos = UnityObjectToClipPos(v.vertex);

        o.positionWS = mul(unity_ObjectToWorld, v.vertex);

        o.uv = v.texcoord;
        
        o.uv1 = v.texcoord1;
        
        o.normalWS = UnityObjectToWorldNormal(v.normal);

        o.lightDirWS = normalize(UnityWorldSpaceLightDir(o.positionWS));

        o.tangentWS = UnityObjectToWorldDir(v.tangent.xyz);
        o.bitangentWS = cross(o.normalWS, o.tangentWS) * v.tangent.w;

        TRANSFER_SHADOW(o);
        UNITY_TRANSFER_FOG(o, o.pos);

        return o;
    }

    fixed3 GetNormalWS(v2f input){
        // normal
        float3 normalWS = input.normalWS;
        #if defined(USE_NORMALMAP)
            float3 normalTS = UnpackNormal(tex2D(_NormalMap, input.uv));
            half3x3 TBN = half3x3(input.tangentWS, input.bitangentWS, input.normalWS);
            normalWS = TransformTangentToWorld(normalTS, TBN);
        #endif
        normalWS = normalize(normalWS);
        return normalWS;
    }

    half GetAlpha(half albedoAlpha, half4 color, half cutoff)
    {
        half alpha = albedoAlpha * color.a;
        #if defined(_ALPHATEST_ON)
            clip(alpha - cutoff);
        #endif
        return alpha;
    }
    
    fixed4 frag(v2f i) : SV_Target {

        fixed3 normalWS = GetNormalWS(i);
        float3 viewDirWS = normalize(_WorldSpaceCameraPos.xyz - i.positionWS.xyz);
        float3 lightColor = _LightColor0.xyz;
        half shadowAtten = SHADOW_ATTENUATION(i);

        half NDotL = dot(normalWS, i.lightDirWS);
        half3x3 TBN = half3x3(i.tangentWS, i.bitangentWS, i.normalWS);

        float halfLambert = 0.5 * NDotL + 0.5;
        float halfLambertWithShadow = halfLambert * saturate(shadowAtten * 0.5 + 0.5 + _TweakSystemShadowsLevel);

        #include "./DiffuseToon.hlsl"

        //=============================================HighColor======================================================================//
    #if defined (EnableHighLight)
        #include "./HighLight.hlsl"
    #endif
        
        //=============================================RimColor======================================================================//
    #if defined (EnableRimLight)
        #include "./RimLight.hlsl"
    #endif
        //=============================================Matcap======================================================================//
    #if defined (EnableMatCap)
        #include "./MatCap.hlsl"
    #endif

        //=============================================AngleRing=====================================================================//
    #if defined (EnableAngleRing)
        #include "./AngleRing.hlsl"
    #endif

        //=============================================Emission======================================================================//
        float3 emission = tex2D(_EmissiveTex, i.uv).rgb * _EmissiveColor.rgb;

        //=============================================GI============================================================================//
        float3 bakedGI = ShadeSH9(float4(normalWS, 1));
        float envIntensity = clamp(0.299*bakedGI.r + 0.587*bakedGI.g + 0.114*bakedGI.b, 0, 1);
        
        //=============================================Final Composition=============================================================//
        color = saturate(color) + (bakedGI * envIntensity * _GI_Intensity * smoothstep(1, 0, envIntensity / 2)) + emission;

        fixed4 finalColor = fixed4(color , 1.0);
        
        UNITY_APPLY_FOG(i.fogCoord, finalColor);
       
        return finalColor;

    }

#endif