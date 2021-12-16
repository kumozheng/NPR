Shader "WXBBShader/Toon" {
    Properties {
        
        
        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
        _NormalMap ("NormalMap", 2D) = "bump" {}

        _BaseMap ("BaseMap", 2D) = "white" {}
        _BaseColor ("BaseColor", Color) = (1, 1, 1, 1)
        _1st_ShadeMap ("1st_ShadeMap", 2D) = "white" {}
        _1st_ShadeColor ("1st_ShadeColor", Color) = (1,1,1,1)
        _2nd_ShadeMap ("2nd_ShadeMap", 2D) = "white" {}
        _2nd_ShadeColor ("2nd_ShadeColor", Color) = (1,1,1,1)

        // System Shadow
        [MaterialToggle] _SetSystemShadowsToBase ("Set_SystemShadowsToBase", Float) = 1
        _TweakSystemShadowsLevel ("Tweak_SystemShadowsLevel", Range(-0.5, 0.5)) = 0

        // Feather
        [KeywordEnum(GradeMap, DoubleShade)] _FeatherMode("Feather Mode", Float) = 0
        _ShadingGradeMap ("ShadingGradeMap", 2D) = "white" {}
        _Tweak_ShadingGradeMapLevel ("Tweak_ShadingGradeMapLevel", Range(-0.5, 0.5)) = 0

        _1st_ShadeColor_Step ("1st_ShadeColor_Step", Range(0, 1)) = 0.5
        _1st_ShadeColor_Feather ("1st_ShadeColor_Feather", Range(0.0001, 1)) = 0.0001
        [MaterialToggle] _Is_1st_ShadeColorOnly ("Is_1st_ShadeColorOnly", Float ) = 1
        _2nd_ShadeColor_Step ("2nd_ShadeColor_Step", Range(0, 1)) = 0.003
        _2nd_ShadeColor_Feather ("2nd_ShadeColor_Feather", Range(0.0001, 1)) = 0.0001

        _Set_1st_ShadePosition ("Set_1st_ShadePosition", 2D) = "white" {}
        _Set_2nd_ShadePosition ("Set_2nd_ShadePosition", 2D) = "white" {}
        _BaseColor_Step ("BaseColor_Step", Range(0, 1)) = 0.5
        _BaseShade_Feather ("Base/Shade_Feather", Range(0.0001, 1)) = 0.0001
        _ShadeColor_Step ("ShadeColor_Step", Range(0, 1)) = 0
        _1st2nd_Shades_Feather ("1st/2nd_Shades_Feather", Range(0.0001, 1)) = 0.0001

        // HighColor
        [MaterialToggle] _HighLight ("HighLight", Float ) = 0
        _HighColor ("HighColor", Color) = (0,0,0,1)
        _HighColor_Tex ("HighColorMap", 2D) = "white" {}
        _HighColor_Power ("HighColor_Power", Range(0, 1)) = 0
        [MaterialToggle] _Is_SpecularToHighColor ("_Is_SpecularToHighColor", Range(0, 1)) = 0
        _HighColorMask ("_HighColorMask", 2D) = "white" {}
        _Tweak_HighColorMaskLevel ("Tweak_HighColorMaskLevel", Range(-1, 1)) = 0
        _TweakHighColorOnShadow ("TweakHighColorOnShadow", Range(0, 1)) = 0

        // Rim
        [MaterialToggle] _RimLight ("RimLight", Float ) = 0
        _RimLightColor ("RimLightColor", Color) = (1,1,1,1)
        _RimLight_Power ("RimLight_Power", Range(0, 1)) = 0.1
        _RimLight_InsideMask ("RimLight_InsideMask", Range(0.0001, 1)) = 0.0001

        [MaterialToggle] _LightDirection_MaskOn ("_LightDirection_MaskOn", Float ) = 0
        [MaterialToggle] _IsLightRimLight ("_IsLightRimLight", Float ) = 0
        _Tweak_LightDirection_MaskLevel ("Tweak_LightDirection_MaskLevel", Range(0, 0.5)) = 0
        _Ap_RimLightColor ("Ap_RimLightColor", Color) = (1,1,1,1)
        _Ap_RimLight_Power ("Ap_RimLight_Power", Range(0, 1)) = 0.1
        _Set_RimLightMask ("Set_RimLightMask", 2D) = "white" {}
        _Tweak_RimLightMaskLevel ("Tweak_RimLightMaskLevel", Range(-1, 1)) = 0  

        // Matcap
        [MaterialToggle] _MatCap ("MatCap", Float ) = 0
        _MatCapTex ("MatCapTex", 2D) = "black" {}
        _MatCapColor ("MatCapColor", Color) = (1,1,1,1)
        // [MaterialToggle] _Is_BlendAddToMatCap ("Is_BlendAddToMatCap", Float ) = 1
        _Rotate_MatCapUV ("Rotate_MatCapUV", Range(-1, 1)) = 0
        _Tweak_MatCapUV ("Tweak_MatCapUV", Range(-0.5, 0.5)) = 0
        // [MaterialToggle] _Is_NormalMapForMatCap ("Is_NormalMapForMatCap", Float ) = 0
        // _NormalMapForMatCap ("NormalMapForMatCap", 2D) = "bump" {}
        // _Rotate_NormalMapForMatCapUV ("Rotate_NormalMapForMatCapUV", Range(-1, 1)) = 0
        _Is_UseTweakMatCapOnShadow ("Is_UseTweakMatCapOnShadow", Range(0, 1)) = 0
        _TweakMatCapOnShadow ("TweakMatCapOnShadow", Range(0, 1)) = 0
        _MatCapMask ("MatCapTex", 2D) = "black" {}
        _Tweak_MatcapMaskLevel ("Tweak_MatcapMaskLevel", Range(-1, 1)) = 0

        // Outline
        [KeywordEnum(UNLIT, LIGHTING)] _OUTLINE("OutlineLightMode", Float) = 0
        [KeywordEnum(NORMAL, COLOR)] _OUTLINE_NORMAL_ATTRIBUTE("OutlineMode", Float) = 0
        _Outline_Width ("Outline_Width", Range(0, 2) ) = 0
        _Outline_Color ("Outline_Color", Color) = (0.5, 0.5, 0.5, 1)
        _Nearest_Distance ("Nearest_Distance", Float ) = 0.5
        _Farthest_Distance ("Farthest_Distance", Float ) = 100
        _Offset_Z ("Offset_Camera_Z", Float) = 0.1

        // AngleRing
        [MaterialToggle] _AngelRing ("AngelRing", Float ) = 0
        _AngelRingTex ("AngelRing Tex", 2D) = "black" {}
        _AngelRingColor ("AngelRing Color", Color) = (1, 1, 1, 1)
        _AR_OffsetU ("AR Offset U", Range(0, 0.5)) = 0
        _AR_OffsetV ("AR Offset V", Range(0, 1)) = 0.3
        [MaterialToggle] _ARTex_AlphaOn ("ARTex AlphaOn", Float ) = 0

        // Emission & GI
        _EmissiveTex ("EmissiveTex", 2D) = "white" {}
        [HDR]_EmissiveColor ("EmissiveColor", Color) = (0,0,0,1)
        _GI_Intensity ("GI_Intensity", Range(0, 1)) = 0
        
        // RenderStates
        [ToggleOff] _AlphaTest("AlphaTest", Float) = 0.0
        [ToggleOff] _AlphaBlend("AlphaBlend", Float) = 0.0
        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
        [HideInInspector] _Fog("__Fog", Float) = 0.0
        [HideInInspector] _Cull("__cull", Float) = 2.0
        [HideInInspector] _Mode("__mode", Float) = 0.0
        [HideInInspector] _SrcBlend("__src", Float) = 1.0
        [HideInInspector] _DstBlend("__dst", Float) = 0.0
        [HideInInspector] _ZWrite("__zw", Float) = 1.0
        [HideInInspector] _ZTest("__zt", Float) = 4.0
        [HideInInspector] _RenderQueue("__rq", Float) = 2000.0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma target 3.0
            #pragma shader_feature USE_LIGHTING
            #pragma shader_feature OUTLINE_NORMAL_IN_COLOR

            #include "./Toon/outline.hlsl"

            ENDCG
        }
        Pass{
            Name "Forward"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend[_SrcBlend][_DstBlend]
            ZWrite[_ZWrite]
            ZTest[_ZTest]
            Cull[_Cull]

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma shader_feature EnableRimLight
            #pragma shader_feature EnableHighLight
            #pragma shader_feature EnableAngleRing
            #pragma shader_feature EnableMatCap
            #pragma shader_feature EnableGradeMap
            #pragma shader_feature USE_NORMALMAP
            #include "./Toon/ForwardToon.hlsl"            

            ENDCG
        }
    }
    CustomEditor "WeChat.ToonGUI"
    FallBack "Standard"
}