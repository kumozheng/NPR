using System;
using UnityEditor;
using UnityEngine;

namespace WeChat {
    class WXToonParser : WXMaterialParser {
        enum RenderMode {
            Opaque = 0,
            Cutout = 1,
            Transparent = 2,
            Custom = 3
        }
        public override void onParse (WXMaterial wxbb_material) {

            Material material = this.m_material;

            SetEffect ("@system/Toon");

            AddShaderParam ("_Cutoff", material.GetFloat ("_Cutoff"));
            if (material.GetTexture ("_NormalMap") != null) {
                AddTexture ("_NormalMap", "_NormalMap");
                AddShaderDefination ("USE_NORMALMAP", true);
            }
            AddTexture ("_BaseMap", "_BaseMap");
            AddShaderParam ("_BaseColor", material.GetColor ("_BaseColor"), true);
            AddTexture ("_1st_ShadeMap", "_1st_ShadeMap");
            AddShaderParam ("_1st_ShadeColor", material.GetColor ("_1st_ShadeColor"), true);
            AddTexture ("_2nd_ShadeMap", "_2nd_ShadeMap");
            AddShaderParam ("_2nd_ShadeColor", material.GetColor ("_2nd_ShadeColor"), true);
            // System Shadow
            AddShaderParam ("_SetSystemShadowsToBase", material.GetFloat ("_SetSystemShadowsToBase"));
            AddShaderParam ("_TweakSystemShadowsLevel", material.GetFloat ("_TweakSystemShadowsLevel"));
            // Feather
            bool enableGradeMap = (double)material.GetFloat ("_FeatherMode") == 0.0;
            if(enableGradeMap){
                AddShaderDefination ("EnableGradeMap", true);
                AddTexture ("_ShadingGradeMap", "_ShadingGradeMap");
                AddShaderParam ("_Tweak_ShadingGradeMapLevel", material.GetFloat ("_Tweak_ShadingGradeMapLevel"));
                AddShaderParam ("_1st_ShadeColor_Step", material.GetFloat ("_1st_ShadeColor_Step"));
                AddShaderParam ("_1st_ShadeColor_Feather", material.GetFloat ("_1st_ShadeColor_Feather"));
                AddShaderParam ("_Is_1st_ShadeColorOnly", material.GetFloat ("_Is_1st_ShadeColorOnly"));
                AddShaderParam ("_2nd_ShadeColor_Step", material.GetFloat ("_2nd_ShadeColor_Step"));
                AddShaderParam ("_2nd_ShadeColor_Feather", material.GetFloat ("_2nd_ShadeColor_Feather"));
            }else{
                AddTexture ("_Set_1st_ShadePosition", "_Set_1st_ShadePosition");
                AddTexture ("_Set_2nd_ShadePosition", "_Set_2nd_ShadePosition");
                AddShaderParam ("_BaseColor_Step", material.GetFloat ("_BaseColor_Step"));
                AddShaderParam ("_BaseShade_Feather", material.GetFloat ("_BaseShade_Feather"));
                AddShaderParam ("_ShadeColor_Step", material.GetFloat ("_ShadeColor_Step"));
                AddShaderParam ("_1st2nd_Shades_Feather", material.GetFloat ("_1st2nd_Shades_Feather"));
            }
            // HighColor
            bool enableHighLight = (double)material.GetFloat ("_HighLight") == 1.0;
            if(enableHighLight){
                AddShaderDefination ("EnableHighLight", true);
                AddTexture ("_HighColor_Tex", "_HighColor_Tex");
                AddShaderParam ("_HighColor", material.GetColor ("_HighColor"), true);
                AddShaderParam ("_HighColor_Power", material.GetFloat ("_HighColor_Power"));
                AddTexture ("_HighColorMask", "_HighColorMask");
                AddShaderParam ("_Is_SpecularToHighColor", material.GetFloat ("_Is_SpecularToHighColor"));
                AddShaderParam ("_Is_SpecularToHighColor", material.GetFloat ("_Is_SpecularToHighColor"));
                AddShaderParam ("_Tweak_HighColorMaskLevel", material.GetFloat ("_Tweak_HighColorMaskLevel"));
                AddShaderParam ("_TweakHighColorOnShadow", material.GetFloat ("_TweakHighColorOnShadow"));
            }
            //RimLight
            bool enableRimLight = (double)material.GetFloat ("_RimLight") == 1.0;
            if(enableRimLight){
                AddShaderDefination ("EnableRimLight", true);
                AddShaderParam ("_RimLightColor", material.GetColor ("_RimLightColor"), true);
                AddShaderParam ("_RimLight_Power", material.GetFloat ("_RimLight_Power"));
                AddShaderParam ("_RimLight_InsideMask", material.GetFloat ("_RimLight_InsideMask"));
                AddShaderParam ("_LightDirection_MaskOn", material.GetFloat ("_LightDirection_MaskOn"));
                AddShaderParam ("_IsLightRimLight", material.GetFloat ("_IsLightRimLight"));
                AddShaderParam ("_Tweak_LightDirection_MaskLevel", material.GetFloat ("_Tweak_LightDirection_MaskLevel"));
                AddTexture ("_Set_RimLightMask", "_Set_RimLightMask");
                AddShaderParam ("_Ap_RimLightColor", material.GetColor ("_Ap_RimLightColor"), true);
                AddShaderParam ("_Ap_RimLight_Power", material.GetFloat ("_Ap_RimLight_Power"));
                AddShaderParam ("_Tweak_RimLightMaskLevel", material.GetFloat ("_Tweak_RimLightMaskLevel"));
            }
            // MatCap
            bool enableMatcap = (double)material.GetFloat ("_MatCap") == 1.0;
            if(enableMatcap){
                AddShaderDefination ("EnableMatCap", true);
                AddTexture ("_MatCapTex", "_MatCapTex");
                AddShaderParam ("_MatCapColor", material.GetColor ("_MatCapColor"), true);
                // AddShaderParam ("_Is_BlendAddToMatCap", material.GetFloat ("_Is_BlendAddToMatCap"));
                AddShaderParam ("_Rotate_MatCapUV", material.GetFloat ("_Rotate_MatCapUV"));
                AddShaderParam ("_Tweak_MatCapUV", material.GetFloat ("_Tweak_MatCapUV"));
                AddShaderParam ("_TweakMatCapOnShadow", material.GetFloat ("_TweakMatCapOnShadow"));
                AddShaderParam ("_Is_UseTweakMatCapOnShadow", material.GetFloat ("_Is_UseTweakMatCapOnShadow"));
                AddTexture ("_MatCapMask", "_MatCapMask");
                AddShaderParam ("_Tweak_MatcapMaskLevel", material.GetFloat ("_Tweak_MatcapMaskLevel"));

                // AddShaderParam ("_Is_NormalMapForMatCap", material.GetFloat ("_Is_NormalMapForMatCap"));
                // AddTexture ("_NormalMapForMatCap", "_NormalMapForMatCap");
                // AddShaderParam ("_Rotate_NormalMapForMatCapUV", material.GetFloat ("_Rotate_NormalMapForMatCapUV"));
            }
            // AngleRing
            bool enableAngleRing = (double)material.GetFloat ("_AngelRing") == 1.0;
            if(enableAngleRing){
                AddShaderDefination ("EnableAngleRing", true);
                AddTexture ("_AngelRingTex", "_AngelRingTex");
                AddShaderParam ("_AngelRingColor", material.GetColor ("_AngelRingColor"), true);
                AddShaderParam ("_AR_OffsetU", material.GetFloat ("_AR_OffsetU"));
                AddShaderParam ("_AR_OffsetV", material.GetFloat ("_AR_OffsetV"));
                AddShaderParam ("_ARTex_AlphaOn", material.GetFloat ("_ARTex_AlphaOn"));
            }
            

            // Emission & GI
            if (material.GetTexture ("_EmissiveTex") != null) {
                AddTexture ("_EmissiveTex", "_EmissiveTex");
                AddShaderParam ("_EmissiveColor", material.GetColor ("_EmissiveColor"), true);
            }
            AddShaderParam ("_GI_Intensity", material.GetFloat ("_GI_Intensity"));
            // Outline
            if((double) material.GetFloat("_OUTLINE") == 1.0){
                AddShaderDefination ("USE_LIGHTING", true);
            }
            AddShaderParam ("_Outline_Color", material.GetColor ("_Outline_Color"), true);
            AddShaderParam ("_Outline_Width", material.GetFloat ("_Outline_Width"));
            AddShaderParam ("_Nearest_Distance", material.GetFloat ("_Nearest_Distance"));
            AddShaderParam ("_Farthest_Distance", material.GetFloat ("_Farthest_Distance"));
            AddShaderParam ("_Offset_Z", material.GetFloat ("_Offset_Z"));

            
            // laya里面，这个shader属性是写反了的
            AddShaderDefination ("USE_FOG", (double) material.GetFloat ("_Fog") == 1.0 ? false : true);

            // alpha test
            if (material.IsKeywordEnabled ("_ALPHATEST_ON")) {
                AddShaderDefination ("USE_ALPHA_TEST", true);
            }

            // alpha blend
            if (material.IsKeywordEnabled ("_ALPHABLEND_ON")) {
                SetBlendOn (true);
                SetBlendFactor (ConvertBlendFactor (material.GetInt ("_SrcBlend")), ConvertBlendFactor (material.GetInt ("_DstBlend")));
            } else {
                SetBlendOn (false);
            }
            // depth write
            SetDepthWrite (material.GetInt ("_ZWrite") == 1 ? true : false);
            // depth test
            SetDepthTest (ConvertCompareFunc (material.GetInt ("_ZTest")));
            // cull
            SetCullMode (ConvertCullMode (material.GetInt ("_Cull")));

            // Render Mode
            RenderMode mode = (RenderMode) material.GetFloat ("_Mode");
            switch (mode) {
                case RenderMode.Opaque:
                    break;
                case RenderMode.Cutout:
                    AddShaderDefination ("USE_ALPHA_TEST", true);
                    break;
                case RenderMode.Transparent:
                    break;
                case RenderMode.Custom:
                    if (material.IsKeywordEnabled ("_ALPHABLEND_ON")) {
                        AddShaderDefination ("USE_ALPHA_TEST", true);
                    }
                    break;
                default:
                    break;
            }

        }

        protected override void SetEffect (String effect) {
            m_mainJson.SetField ("effect", effect);
        }
    }
}