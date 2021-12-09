//#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
namespace WeChat {
    class ToonGUI : ShaderGUI {

        public override void AssignNewShaderToMaterial (Material material, Shader oldShader, Shader newShader) {
            material.shader = newShader;
            material.EnableKeyword ("EnableFog");
        }
        public enum RenderMode {
            Opaque = 0,
            Cutout = 1,
            Transparent = 2,
            Custom = 3
        }
        public enum SrcBlendMode {
            //Blend factor is (0, 0, 0, 0).
            Zero = 0,
            //Blend factor is (1, 1, 1, 1).
            One = 1,
            //Blend factor is (Rd, Gd, Bd, Ad).
            DstColor = 2,
            //Blend factor is (Rs, Gs, Bs, As).
            SrcColor = 3,
            //Blend factor is (1 - Rd, 1 - Gd, 1 - Bd, 1 - Ad).
            OneMinusDstColor = 4,
            //Blend factor is (As, As, As, As).
            SrcAlpha = 5,
            //Blend factor is (1 - Rs, 1 - Gs, 1 - Bs, 1 - As).
            OneMinusSrcColor = 6,
            //Blend factor is (Ad, Ad, Ad, Ad).
            DstAlpha = 7,
            //Blend factor is (1 - Ad, 1 - Ad, 1 - Ad, 1 - Ad).
            OneMinusDstAlpha = 8,
            //Blend factor is (f, f, f, 1); where f = min(As, 1 - Ad).
            SrcAlphaSaturate = 9,
            //Blend factor is (1 - As, 1 - As, 1 - As, 1 - As).
            OneMinusSrcAlpha = 10
        }
        public enum DstBlendMode {
            //Blend factor is (0, 0, 0, 0).
            Zero = 0,
            //Blend factor is (1, 1, 1, 1).
            One = 1,
            //Blend factor is (Rd, Gd, Bd, Ad).
            DstColor = 2,
            //Blend factor is (Rs, Gs, Bs, As).
            SrcColor = 3,
            //Blend factor is (1 - Rd, 1 - Gd, 1 - Bd, 1 - Ad).
            OneMinusDstColor = 4,
            //Blend factor is (As, As, As, As).
            SrcAlpha = 5,
            //Blend factor is (1 - Rs, 1 - Gs, 1 - Bs, 1 - As).
            OneMinusSrcColor = 6,
            //Blend factor is (Ad, Ad, Ad, Ad).
            DstAlpha = 7,
            //Blend factor is (1 - Ad, 1 - Ad, 1 - Ad, 1 - Ad).
            OneMinusDstAlpha = 8,
            //Blend factor is (f, f, f, 1); where f = min(As, 1 - Ad).
            SrcAlphaSaturate = 9,
            //Blend factor is (1 - As, 1 - As, 1 - As, 1 - As).
            OneMinusSrcAlpha = 10
        }
        public enum CullMode {
            CULL_NONE = 0,
            CULL_FRONT = 1,
            CULL_BACK = 2,
        }

        public enum DepthWrite {
            OFF = 0,
            ON = 1
        }

        public enum DepthTest {
            OFF = 0,
            Never = 1,
            LESS = 2,
            EQUAL = 3,
            LEQUAL = 4,
            GREATER = 5,
            NOTEQUAL = 6,
            GEQUAL = 7,
            ALWAYS = 8
        }
        public enum LightingMode {
            ON = 0,
            OFF = 1,
        }
        public enum OutlineMode {
            NormalDirection,
            PositionScaling
        }

        // RenderState
        MaterialProperty fog = null;
        MaterialProperty cullMode = null;
        MaterialProperty renderMode = null;
        MaterialProperty alphaTest = null;
        MaterialProperty alphaCutoff = null;
        MaterialProperty alphaBlend = null;
        MaterialProperty srcBlendMode = null;
        MaterialProperty dstBlendMode = null;
        MaterialProperty depthWrite = null;
        MaterialProperty depthTest = null;
        // BaseColor Props
        static bool _Basic_Foldout = true;
        MaterialProperty normalMap = null;
        MaterialProperty baseMap = null;
        MaterialProperty baseColor = null;
        MaterialProperty shadeMap1 = null;
        MaterialProperty shadeColor1 = null;
        MaterialProperty shadeMap2 = null;
        MaterialProperty shadeColor2 = null;

        // Shadow 
        MaterialProperty shadingGradeMap = null;
        MaterialProperty tweakShadingGradeMapLevel = null;
        MaterialProperty setSystemShadowsToBase = null;
        MaterialProperty tweakSystemShadowsLevel = null;
        
        // Feather
        // -----ShadingGradeMap
        MaterialProperty is_1st_ShadeColorOnly = null;
        MaterialProperty _1st_ShadeColor_Step = null;
        MaterialProperty _1st_ShadeColor_Feather = null;
        MaterialProperty _2nd_ShadeColor_Step = null;
        MaterialProperty _2nd_ShadeColor_Feather = null;
        // ----DoubleShade
        MaterialProperty featherMode = null;
        MaterialProperty _1st_ShadePosition = null;
        MaterialProperty _2nd_ShadePosition = null;
        MaterialProperty baseColorStep = null;
        MaterialProperty baseColorFeather = null;
        MaterialProperty shadeColorStep = null;
        MaterialProperty shadeFeather = null;
        
        // HighLight
        static bool _HighLight_Foldout = true;
        MaterialProperty highLight = null;
        MaterialProperty highColor = null;
        MaterialProperty highColorMap = null;
        MaterialProperty highColorPower = null;
        MaterialProperty highColorMask = null;
        MaterialProperty isSpecularToHighColor = null;
        MaterialProperty tweakHighColorMaskLevel = null;
        MaterialProperty tweakHighColorOnShadow = null;

        // RimLight
        static bool _RimLight_Foldout = true;
        MaterialProperty rimLight = null;
        MaterialProperty rimLightColor = null;
        MaterialProperty rimLightPower = null;
        MaterialProperty rimLightInsideMask = null;
        MaterialProperty lightDirection_MaskOn = null;
        MaterialProperty isLightRimLight = null;
        MaterialProperty tweakLightDirectionMaskLevel = null;
        MaterialProperty rimLightColor_AP = null;
        MaterialProperty rimLightPower_AP = null;
        MaterialProperty rimLightMask = null;
        MaterialProperty tweakRimLightMaskLevel = null;

        // AngleRing
        static bool _AngleRing_Foldout = true;
        MaterialProperty angleRing = null;
        MaterialProperty angleRingTex = null;
        MaterialProperty angleRingColor = null;
        MaterialProperty angleRingOffsetU = null;
        MaterialProperty angleRingOffsetV = null;
        MaterialProperty arTexAlphaOn = null;

        // MatCap
        static bool _MatCap_Foldout = true;
        MaterialProperty matcap = null;
        MaterialProperty matcapTex = null;
        MaterialProperty matcapColor = null;
        // MaterialProperty isBlendAddToMatcap = null;
        MaterialProperty rotateMatcapUV = null;
        MaterialProperty tweakMatcapUV = null;
        // MaterialProperty is_NormalMapForMatCap = null;
        // MaterialProperty normalMapForMatcap = null;
        // MaterialProperty rotateNormalMapForMatcapUV = null;
        MaterialProperty isUseTweakMatCapOnShadow = null;
        MaterialProperty tweakMatcapOnShadow = null;
        MaterialProperty matCapMask = null;
        MaterialProperty tweakMatcapMaskLevel = null;


        // Emission
        static bool _Emission_GI_Foldout = true;
        MaterialProperty emissiveTex = null;
        MaterialProperty emissiveColor = null;
        MaterialProperty GI_Intensity = null;

        // OutLine
        static bool _OutLine_Foldout = true;
        MaterialProperty outLineLightMode = null;
        MaterialProperty outLineMode = null;
        MaterialProperty outline_Width = null;
        MaterialProperty outline_Color = null;
        MaterialProperty farthest_Distance = null;
        MaterialProperty nearest_Distance = null;
        MaterialProperty offset_Z = null;
    

        MaterialEditor m_MaterialEditor;
        bool m_FirstTimeApply = true;
        static bool _RenderState_Foldout = true;

        public void FindProperties (MaterialProperty[] props, Material material) {

            fog = FindProperty ("_Fog", props);
            renderMode = FindProperty ("_Mode", props);
            cullMode = FindProperty ("_Cull", props);

            alphaTest = FindProperty ("_AlphaTest", props, false);
            alphaCutoff = FindProperty ("_Cutoff", props, false);

            alphaBlend = FindProperty ("_AlphaBlend", props, false);
            srcBlendMode = FindProperty ("_SrcBlend", props);
            dstBlendMode = FindProperty ("_DstBlend", props);

            depthWrite = FindProperty ("_ZWrite", props);
            depthTest = FindProperty ("_ZTest", props);

            // BaseColor Props 
            normalMap = FindProperty ("_NormalMap", props, false);
            baseMap = FindProperty ("_BaseMap", props, false);
            baseColor = FindProperty ("_BaseColor", props, false);
            shadeMap1 = FindProperty ("_1st_ShadeMap", props, false);
            shadeColor1 = FindProperty ("_1st_ShadeColor", props, false); 
            shadeMap2 = FindProperty ("_2nd_ShadeMap", props, false);
            shadeColor2 = FindProperty ("_2nd_ShadeColor", props, false);

            // Shadow
            shadingGradeMap = FindProperty ("_ShadingGradeMap", props, false);
            tweakShadingGradeMapLevel = FindProperty ("_Tweak_ShadingGradeMapLevel", props, false);
            setSystemShadowsToBase = FindProperty ("_SetSystemShadowsToBase", props, false);
            tweakSystemShadowsLevel = FindProperty ("_TweakSystemShadowsLevel", props, false);

            // Feather
            is_1st_ShadeColorOnly = FindProperty ("_Is_1st_ShadeColorOnly", props, false);
            _1st_ShadeColor_Step = FindProperty ("_1st_ShadeColor_Step", props, false);
            _1st_ShadeColor_Feather = FindProperty ("_1st_ShadeColor_Feather", props, false);
            _2nd_ShadeColor_Step = FindProperty ("_2nd_ShadeColor_Step", props, false);
            _2nd_ShadeColor_Feather = FindProperty ("_2nd_ShadeColor_Feather", props, false);

            featherMode = FindProperty ("_FeatherMode", props, false);
            _1st_ShadePosition = FindProperty ("_Set_1st_ShadePosition", props, false);
            _2nd_ShadePosition = FindProperty ("_Set_2nd_ShadePosition", props, false);
            baseColorStep = FindProperty ("_BaseColor_Step", props, false);
            baseColorFeather = FindProperty ("_BaseShade_Feather", props, false);
            shadeColorStep = FindProperty ("_ShadeColor_Step", props, false);
            shadeFeather = FindProperty ("_1st2nd_Shades_Feather", props, false);
        
            // High Light
            highLight = FindProperty ("_HighLight", props, false);
            highColor = FindProperty ("_HighColor", props, false);
            highColorMap = FindProperty ("_HighColor_Tex", props, false);
            highColorPower = FindProperty ("_HighColor_Power", props, false);
            highColorMask = FindProperty ("_HighColorMask", props, false);
            isSpecularToHighColor = FindProperty ("_Is_SpecularToHighColor", props, false);
            tweakHighColorMaskLevel = FindProperty ("_Tweak_HighColorMaskLevel", props, false);
            tweakHighColorOnShadow = FindProperty ("_TweakHighColorOnShadow", props, false);

            //rimLight
            rimLight = FindProperty ("_RimLight", props, false);
            rimLightColor = FindProperty ("_RimLightColor", props, false);
            rimLightPower = FindProperty ("_RimLight_Power", props, false);
            rimLightInsideMask = FindProperty ("_RimLight_InsideMask", props, false);
            lightDirection_MaskOn = FindProperty ("_LightDirection_MaskOn", props, false);
            isLightRimLight = FindProperty ("_IsLightRimLight", props, false);
            tweakLightDirectionMaskLevel = FindProperty ("_Tweak_LightDirection_MaskLevel", props, false);
            rimLightColor_AP = FindProperty ("_Ap_RimLightColor", props, false);
            rimLightPower_AP = FindProperty ("_Ap_RimLight_Power", props, false);
            rimLightMask = FindProperty ("_Set_RimLightMask", props, false);
            tweakRimLightMaskLevel = FindProperty ("_Tweak_RimLightMaskLevel", props, false);

            // AngleRing
            angleRing = FindProperty ("_AngelRing", props, false);
            angleRingTex = FindProperty ("_AngelRingTex", props, false);
            angleRingColor = FindProperty ("_AngelRingColor", props, false);
            angleRingOffsetU = FindProperty ("_AR_OffsetU", props, false);
            angleRingOffsetV = FindProperty ("_AR_OffsetV", props, false);
            arTexAlphaOn = FindProperty ("_ARTex_AlphaOn", props, false);

            // Matcap
            matcap = FindProperty ("_MatCap", props, false);
            matcapTex = FindProperty ("_MatCapTex", props, false);
            matcapColor = FindProperty ("_MatCapColor", props, false);
            // isBlendAddToMatcap = FindProperty ("_Is_BlendAddToMatCap", props, false);
            rotateMatcapUV = FindProperty ("_Rotate_MatCapUV", props, false);
            tweakMatcapUV = FindProperty ("_Tweak_MatCapUV", props, false);
            // is_NormalMapForMatCap = FindProperty ("_Is_NormalMapForMatCap", props, false);
            // normalMapForMatcap = FindProperty ("_NormalMapForMatCap", props, false);
            // rotateNormalMapForMatcapUV = FindProperty ("_Rotate_NormalMapForMatCapUV", props, false);
            isUseTweakMatCapOnShadow = FindProperty ("_Is_UseTweakMatCapOnShadow", props, false);
            tweakMatcapOnShadow = FindProperty ("_TweakMatCapOnShadow", props, false);
            matCapMask = FindProperty ("_MatCapMask", props, false);
            tweakMatcapMaskLevel = FindProperty ("_Tweak_MatcapMaskLevel", props, false);

            // Emissive & GI
            emissiveTex = FindProperty ("_EmissiveTex", props, false);
            emissiveColor = FindProperty ("_EmissiveColor", props, false);
            GI_Intensity = FindProperty ("_GI_Intensity", props, false);

            // Outline
            if(material.HasProperty("_OUTLINE")){
                outLineLightMode = FindProperty ("_OUTLINE", props, false);
                outLineMode = FindProperty ("_OUTLINE_NORMAL_ATTRIBUTE", props, false);
                outline_Width = FindProperty ("_Outline_Width", props, false);
                outline_Color = FindProperty ("_Outline_Color", props, false);
                farthest_Distance = FindProperty ("_Farthest_Distance", props, false);
                nearest_Distance = FindProperty ("_Nearest_Distance", props, false);
                offset_Z = FindProperty ("_Offset_Z", props, false);
            }
            
        }

        public override void OnGUI (MaterialEditor materialEditor, MaterialProperty[] props) {
            // render the default gui
            m_MaterialEditor = materialEditor;
            Material material = m_MaterialEditor.target as Material;
            FindProperties (props, material);


            if (m_FirstTimeApply) {
                onChangeRender (material, (RenderMode) material.GetFloat ("_Mode"));
                m_FirstTimeApply = false;
            }
            ShaderPropertiesGUI (material);
        }

        public void ShaderPropertiesGUI (Material material) {
            // Use default labelWidth
            EditorGUIUtility.labelWidth = 0f;

            // // Detect any changes to the material
            EditorGUI.BeginChangeCheck (); {

                // Basic
                DoBasic (material);

                // High Light
                DoHighLight(material);

                // Rim Light
                DoRimLight(material);

                // MatCap
                DoMatcap(material);

                // Angle Ring
                DoAngleRing(material);

                // Outline
                if(material.HasProperty("_OUTLINE")){
                    DoOutline(material);
                }

                // Emissive & GI
                DoEmissionGI(material);

                // Render State
                DoRenderStates (material);

                // Set keywords
                SetMaterialKeywords (material);

                if (EditorGUI.EndChangeCheck ()) {
                    onChangeRender (material, (RenderMode) material.GetFloat ("_Mode"));
                }

            }

            
        }

        public void onChangeRender (Material material, RenderMode mode) {

            switch (mode) {
                case RenderMode.Opaque:
                    material.SetInt ("_Mode", 0);
                    material.SetInt ("_AlphaTest", 0);
                    material.SetInt ("_AlphaBlend", 0);
                    material.SetInt ("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
                    material.SetInt ("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt ("_ZWrite", 1);
                    material.SetInt ("_ZTest", 4);
                    material.DisableKeyword ("_ALPHATEST_ON");
                    material.DisableKeyword ("_ALPHABLEND_ON");
                    material.DisableKeyword ("ENABLE_ALPHA_CUTOFF");
                    material.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Geometry;
                    break;
                case RenderMode.Cutout:
                    material.SetInt ("_Mode", 1);
                    material.SetInt ("_AlphaTest", 1);
                    material.SetInt ("_AlphaBlend", 0);
                    material.SetInt ("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
                    material.SetInt ("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt ("_ZWrite", 1);
                    material.SetInt ("_ZTest", 4);
                    material.EnableKeyword ("_ALPHATEST_ON");
                    material.DisableKeyword ("_ALPHABLEND_ON");
                    material.EnableKeyword ("ENABLE_ALPHA_CUTOFF");
                    material.renderQueue = (int) UnityEngine.Rendering.RenderQueue.AlphaTest;
                    break;
                case RenderMode.Transparent:
                    material.SetInt ("_Mode", 2);
                    material.SetInt ("_AlphaTest", 0);
                    material.SetInt ("_AlphaBlend", 1);
                    material.SetInt ("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt ("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt ("_ZWrite", 0);
                    material.SetInt ("_ZTest", 4);
                    material.DisableKeyword ("_ALPHATEST_ON");
                    material.EnableKeyword ("_ALPHABLEND_ON");
                    material.DisableKeyword ("ENABLE_ALPHA_CUTOFF");
                    material.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                case RenderMode.Custom:
                    material.SetInt ("_Mode", 3);
                    break;
                default:
                    material.SetInt ("_Mode", 0);
                    material.SetInt ("_AlphaTest", 0);
                    material.SetInt ("_AlphaBlend", 0);
                    material.SetInt ("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
                    material.SetInt ("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt ("_ZWrite", 1);
                    material.SetInt ("_ZTest", 4);
                    material.DisableKeyword ("_ALPHATEST_ON");
                    material.DisableKeyword ("_ALPHABLEND_ON");
                    material.DisableKeyword ("ENABLE_ALPHA_CUTOFF");
                    material.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Geometry;
                    break;
            }
        }

        public static class Styles {
            public static GUIStyle optionsButton = "PaneOptions";
            public static GUIContent uvSetLabel = new GUIContent ("UV Set");
            public static GUIContent[] uvSetOptions = new GUIContent[] { new GUIContent ("UV channel 0"), new GUIContent ("UV channel 1") };
            public static string emptyTootip = "";
            public static GUIContent alphaTestText = new GUIContent ("AlphaTest", "AlphaTest");
            public static GUIContent alphaCutoffText = new GUIContent ("Alpha Cutoff", "Threshold for alpha cutoff");
            public static GUIContent alphaBlendText = new GUIContent ("AlphaBlend", "AlphaBlend");
            public static GUIContent normalMapText = new GUIContent ("Normal", "Normal");
            public static string depthWriteText = "DepthWrite";
            public static string depthTestText = "DepthTest";
            public static string cullModeText = "CullMode";
            public static string whiteSpaceString = " ";
            public static string AdvancedText = "Advanced Properties";
            public static string renderModeText = "RenderMode";
            public static string fogModeText = "Fog";
            public static readonly string[] srcBlendNames = Enum.GetNames (typeof (SrcBlendMode));
            public static readonly string[] dstBlendNames = Enum.GetNames (typeof (DstBlendMode));
            public static readonly string[] renderModeNames = Enum.GetNames (typeof (RenderMode));
            public static readonly string[] cullModeNames = Enum.GetNames (typeof (CullMode));
            public static readonly string[] depthWriteNames = Enum.GetNames (typeof (DepthWrite));
            public static readonly string[] depthTestNames = Enum.GetNames (typeof (DepthTest));
            public static readonly string[] lightingNames = Enum.GetNames (typeof (LightingMode));
            public static readonly string[] outlineModeNames = Enum.GetNames (typeof (OutlineMode));

        }

        protected void DoPopup (string label, MaterialProperty property, string[] options) {
            if (property == null)
                throw new ArgumentNullException ("property");

            EditorGUI.showMixedValue = property.hasMixedValue;

            var mode = property.floatValue;
            EditorGUI.BeginChangeCheck ();
            mode = EditorGUILayout.Popup (label, (int) mode, options);
            if (EditorGUI.EndChangeCheck ()) {
                m_MaterialEditor.RegisterPropertyChangeUndo (label);
                property.floatValue = (float) mode;
            }

            EditorGUI.showMixedValue = false;
        }

        static bool Foldout (bool display, string title) {
            var style = new GUIStyle ("ShurikenModuleTitle");
            style.font = new GUIStyle (EditorStyles.boldLabel).font;
            style.border = new RectOffset (15, 7, 4, 4);
            style.fixedHeight = 22;
            style.contentOffset = new Vector2 (20f, -2f);

            var rect = GUILayoutUtility.GetRect (16f, 22f, style);
            GUI.Box (rect, title, style);

            var e = Event.current;

            var toggleRect = new Rect (rect.x + 4f, rect.y + 2f, 13f, 13f);
            if (e.type == EventType.Repaint) {
                EditorStyles.foldout.Draw (toggleRect, false, false, display, false);
            }

            if (e.type == EventType.MouseDown && rect.Contains (e.mousePosition)) {
                display = !display;
                e.Use ();
            }

            return display;
        }

        // Do Material Properties
        static void SetKeyword (Material material, string keyword, bool enableValue) {
            if (enableValue) {
                material.EnableKeyword (keyword);
            } else {
                material.DisableKeyword (keyword);
            }
        }

        void SetMaterialKeywords (Material material) {
            m_MaterialEditor.RegisterPropertyChangeUndo ("Rendering Mode");

            if ((RenderMode) material.GetFloat ("_Mode") == RenderMode.Custom) {
                SetKeyword (material, "ENABLE_ALPHA_CUTOFF", alphaTest.floatValue == 1);
                SetKeyword (material, "_ALPHATEST_ON", alphaTest.floatValue == 1);
                SetKeyword (material, "_ALPHABLEND_ON", alphaBlend.floatValue == 1);
            }

            //Fog
            SetKeyword (material, "EnableFog", fog.floatValue == 0);
            SetKeyword (material, "EnableRimLight", rimLight.floatValue == 1);
            SetKeyword (material, "EnableHighLight", highLight.floatValue == 1);
            SetKeyword (material, "EnableAngleRing", angleRing.floatValue == 1);
            SetKeyword (material, "EnableMatCap", matcap.floatValue == 1);
            SetKeyword (material, "EnableGradeMap", featherMode.floatValue == 0);
            if(material.HasProperty("_OUTLINE")){
                SetKeyword (material, "USE_LIGHTING", outLineLightMode.floatValue == 1);
                SetKeyword (material, "OUTLINE_NORMAL_IN_COLOR", outLineMode.floatValue == 1);
            }
        }

        void DoBasic (Material material) {
            _Basic_Foldout = Foldout (_Basic_Foldout, "Basic Setting");
            if (_Basic_Foldout) {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space ();
                DoPopup (Styles.renderModeText, renderMode, Styles.renderModeNames);
                DoPopup (Styles.fogModeText, fog, Styles.lightingNames);
                m_MaterialEditor.TexturePropertySingleLine (new GUIContent("Normal", "Normal"), normalMap);
                m_MaterialEditor.TexturePropertySingleLine (new GUIContent("BaseColor", "BaseColor"), baseMap, baseColor);
                m_MaterialEditor.TexturePropertySingleLine (new GUIContent("ShadeColor1", "ShadeColor1"), shadeMap1, shadeColor1);
                m_MaterialEditor.TexturePropertySingleLine (new GUIContent("ShadeColor2", "ShadeColor2"), shadeMap2, shadeColor2);
                // Feather & Shadow
                m_MaterialEditor.ShaderProperty (featherMode, "Feather Mode");
                if(featherMode.floatValue == 0){
                    DoFeatherWithGradeMap();
                }else{
                    DoFeather();
                }
                
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space ();
        }

        void DoFeather(){
            // Shadow
            GUILayout.Label ("Shadow Control", EditorStyles.boldLabel);
            m_MaterialEditor.ShaderProperty (setSystemShadowsToBase, "Set SystemShadows To Base");
            if(setSystemShadowsToBase.floatValue == 1){
                m_MaterialEditor.ShaderProperty (tweakSystemShadowsLevel, "Tweak System Shadows Level");
            }
            m_MaterialEditor.TexturePropertySingleLine (new GUIContent("Set 1st ShadePosition", "Set 1st ShadePosition"), _1st_ShadePosition);
            m_MaterialEditor.TexturePropertySingleLine (new GUIContent("Set 2nd ShadePosition", "Set 2nd ShadePosition"), _2nd_ShadePosition);

            // Feather
            GUILayout.Label ("Feather Setting", EditorStyles.boldLabel);
            m_MaterialEditor.ShaderProperty (baseColorStep, "BaseColor Step");
            m_MaterialEditor.ShaderProperty (baseColorFeather, "BaseColor Feather");
            m_MaterialEditor.ShaderProperty (shadeColorStep, "Shade Color Step");
            m_MaterialEditor.ShaderProperty (shadeFeather, "1st2nd Shades Feather");
        }

        void DoFeatherWithGradeMap(){
            // Shadow
            GUILayout.Label ("Shadow Control", EditorStyles.boldLabel);
            m_MaterialEditor.TexturePropertySingleLine (new GUIContent("ShadingGradeMap", "ShadingGradeMap"), shadingGradeMap);
            m_MaterialEditor.ShaderProperty (tweakShadingGradeMapLevel, "Tweak ShadingGradeMap Level");
            m_MaterialEditor.ShaderProperty (setSystemShadowsToBase, "Set SystemShadows To Base");
            if(setSystemShadowsToBase.floatValue == 1){
                m_MaterialEditor.ShaderProperty (tweakSystemShadowsLevel, "Tweak System Shadows Level");
            }
            // Feather
            GUILayout.Label ("Feather Setting", EditorStyles.boldLabel);
            m_MaterialEditor.ShaderProperty (_1st_ShadeColor_Step, "1st ShadeColor Step");
            m_MaterialEditor.ShaderProperty (_1st_ShadeColor_Feather, "1st ShadeColor Feather");
            m_MaterialEditor.ShaderProperty (is_1st_ShadeColorOnly, "Only Shade Layer01");
            if(is_1st_ShadeColorOnly.floatValue == 0){
                m_MaterialEditor.ShaderProperty (_2nd_ShadeColor_Step, "2nd ShadeColor Step");
                m_MaterialEditor.ShaderProperty (_2nd_ShadeColor_Feather, "2nd ShadeColor Feather");
            }
        }

        void DoHighLight (Material material) {
            _HighLight_Foldout = Foldout (_HighLight_Foldout, "HightLight Setting");
            if (_HighLight_Foldout) {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space ();
                m_MaterialEditor.ShaderProperty (highLight, "HighLight");
                if(highLight.floatValue == 1){
                    m_MaterialEditor.TexturePropertySingleLine (new GUIContent("HighColor", "HighColor"), highColorMap, highColor);
                    m_MaterialEditor.TexturePropertySingleLine (new GUIContent("HighColorMask", "HighColorMask"), highColorMask, null);
                    m_MaterialEditor.ShaderProperty (highColorPower, "HighColor Power");
                    m_MaterialEditor.ShaderProperty (isSpecularToHighColor, "Is Specular To HighColor");
                    m_MaterialEditor.ShaderProperty (tweakHighColorOnShadow, "Tweak HighColor On Shadow");
                    m_MaterialEditor.ShaderProperty (tweakHighColorMaskLevel, "Tweak HighColor Mask Level");
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space ();
        }

        void DoRimLight (Material material) {
            _RimLight_Foldout = Foldout (_RimLight_Foldout, "RimLight Setting");
            if (_RimLight_Foldout) {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space ();
                m_MaterialEditor.ShaderProperty (rimLight, "RimLight");
                if(rimLight.floatValue == 1){
                    m_MaterialEditor.ShaderProperty (rimLightColor, "RimLight Color");
                    m_MaterialEditor.ShaderProperty (rimLightPower, "RimLight Power");
                    m_MaterialEditor.ShaderProperty (rimLightInsideMask, "RimLight Inside Mask");
                    m_MaterialEditor.ShaderProperty (lightDirection_MaskOn, "Tweak Light Direction Mask On");
                    m_MaterialEditor.ShaderProperty (isLightRimLight, "Is Light RimColor");
                    m_MaterialEditor.ShaderProperty (tweakLightDirectionMaskLevel, "Tweak Light Direction Mask Level");
                    m_MaterialEditor.ShaderProperty (rimLightColor_AP, "AntiPodean RimLight Color");
                    m_MaterialEditor.ShaderProperty (rimLightPower_AP, "AntiPodean RimLight Power");
                    m_MaterialEditor.TexturePropertySingleLine (new GUIContent("RimLight Mask", "RimLight Mask"), rimLightMask, null);
                    m_MaterialEditor.ShaderProperty (tweakRimLightMaskLevel, "Tweak RimLight Mask Level");
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space ();
        }

        void DoAngleRing (Material material) {
            _AngleRing_Foldout = Foldout (_AngleRing_Foldout, "AngleRing Setting");
            if (_AngleRing_Foldout) {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space ();
                m_MaterialEditor.ShaderProperty (angleRing, "AngleRing");
                if(angleRing.floatValue == 1){
                    m_MaterialEditor.TexturePropertySingleLine (new GUIContent("AngleRing Color", "AngleRing Color"), angleRingTex, angleRingColor);
                    m_MaterialEditor.ShaderProperty (angleRingOffsetU, "AngleRing U Offset");
                    m_MaterialEditor.ShaderProperty (angleRingOffsetV, "AngleRing V Offset");
                    m_MaterialEditor.ShaderProperty (arTexAlphaOn, "AngleRing Tex AlphaOn");
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space ();
        }

        void DoMatcap (Material material) {
            _MatCap_Foldout = Foldout (_MatCap_Foldout, "MatCap Setting");
            if (_MatCap_Foldout) {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space ();
                m_MaterialEditor.ShaderProperty (matcap, "MatCap");
                if(matcap.floatValue == 1){
                    m_MaterialEditor.TexturePropertySingleLine (new GUIContent("MatCap Color", "MatCap Color"), matcapTex, matcapColor);
                    // m_MaterialEditor.ShaderProperty (isBlendAddToMatcap, "Is BlendAdd To Matcap");
                    m_MaterialEditor.ShaderProperty (rotateMatcapUV, "Rotate MatCap UV");
                    m_MaterialEditor.ShaderProperty (tweakMatcapUV, "Scale MatCap UV");
                    // m_MaterialEditor.ShaderProperty (is_NormalMapForMatCap, "Is NormalMap For MatCap");
                    // m_MaterialEditor.ShaderProperty (normalMapForMatcap, "NormalMap For MatCap");
                    // m_MaterialEditor.ShaderProperty (rotateNormalMapForMatcapUV, "Rotate NormalMap For MatCap UV");

                    m_MaterialEditor.ShaderProperty (isUseTweakMatCapOnShadow, "Is Use Tweak MatCap On Shadow");
                    m_MaterialEditor.ShaderProperty (tweakMatcapOnShadow, "Tweak MatCap On Shadow");

                     m_MaterialEditor.TexturePropertySingleLine (new GUIContent("MatCap Mask", "MatCap Mask"), matCapMask, null);
                    m_MaterialEditor.ShaderProperty (tweakMatcapMaskLevel, "Tweak MatCap Mask Level");
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space ();
        }

        void DoEmissionGI (Material material) {
            _Emission_GI_Foldout = Foldout (_Emission_GI_Foldout, "Emissive & GI Setting");
            if (_Emission_GI_Foldout) {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space ();
                m_MaterialEditor.TexturePropertySingleLine (new GUIContent("Emissive Color", "Emissive Color"), emissiveTex, emissiveColor);
                m_MaterialEditor.ShaderProperty (GI_Intensity, "GI_Intensity");
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space ();
        }

        void DoOutline (Material material) {
            _OutLine_Foldout = Foldout (_OutLine_Foldout, "Outline Setting");
            if (_OutLine_Foldout) {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space ();
                m_MaterialEditor.ShaderProperty (outLineLightMode, "Outline Light Mode");
                m_MaterialEditor.ShaderProperty (outLineMode, "Outline Normal Attribute");
                m_MaterialEditor.ShaderProperty (outline_Width, "Outline Width");
                m_MaterialEditor.ShaderProperty (outline_Color, "Outline Color");
                m_MaterialEditor.ShaderProperty (nearest_Distance, "Nearest Distance");
                m_MaterialEditor.ShaderProperty (farthest_Distance, "Farthest Distance");
                m_MaterialEditor.ShaderProperty (offset_Z, "Offset Z");
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space ();
        }

        void DoRenderStates (Material material) {
            _RenderState_Foldout = Foldout (_RenderState_Foldout, "RenderState Settings");
            if (_RenderState_Foldout) {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space ();

                //AlphaTest
                m_MaterialEditor.ShaderProperty (alphaTest, Styles.alphaTestText);
                if (alphaTest.floatValue == 1) {
                    m_MaterialEditor.ShaderProperty (alphaCutoff, Styles.alphaCutoffText, MaterialEditor.kMiniTextureFieldLabelIndentLevel + 1);
                }

                //AlphaBlend
                m_MaterialEditor.ShaderProperty (alphaBlend, Styles.alphaBlendText);
                var dstMode = (DstBlendMode) dstBlendMode.floatValue;
                var srcMode = (SrcBlendMode) srcBlendMode.floatValue;
                if (alphaBlend.floatValue == 1) {
                    GUILayout.BeginHorizontal ();
                    GUILayout.Label ("", GUILayout.Width (20));
                    srcMode = (SrcBlendMode) EditorGUILayout.Popup ((int) srcMode, Styles.srcBlendNames);
                    dstMode = (DstBlendMode) EditorGUILayout.Popup ((int) dstMode, Styles.dstBlendNames);
                    GUILayout.EndHorizontal ();
                }
                if ((RenderMode) material.GetFloat ("_Mode") == RenderMode.Custom) {
                    //alphaBlend
                    if (alphaBlend.floatValue == 1) {
                        srcBlendMode.floatValue = (float) srcMode;
                        dstBlendMode.floatValue = (float) dstMode;
                        material.SetInt ("_SrcBlend", (int) srcMode);
                        material.SetInt ("_DstBlend", (int) dstMode);
                        material.SetInt ("_AlphaBlend", 1);
                    } else {
                        material.SetInt ("_AlphaBlend", 0);
                        material.SetInt ("_SrcBlend", (int) 1);
                        material.SetInt ("_DstBlend", (int) 0);
                    }
                }

                //DepthWrite
                DoPopup (Styles.depthWriteText, depthWrite, Styles.depthWriteNames);

                //DepthTest
                DoPopup (Styles.depthTestText, depthTest, Styles.depthTestNames);

                //CullMode
                DoPopup (Styles.cullModeText, cullMode, Styles.cullModeNames);
                
                m_MaterialEditor.RenderQueueField ();

                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space ();

        }

    }
}

//#endif