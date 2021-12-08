    //=============================================BaseColor======================================================================//
    // layer0
    fixed4 baseMap = tex2D(_BaseMap, i.uv);
    fixed alpha = GetAlpha(baseMap.a, _BaseColor, _Cutoff);
    float3 shadeColorLayer0  = baseMap.rgb * _BaseColor.rgb;
    shadeColorLayer0 = shadeColorLayer0 * lightColor;
    // layer1
    fixed4 _1st_shadeMap = tex2D(_1st_ShadeMap, i.uv);
    float3 shadeColorLayer1 =  _1st_shadeMap.rgb * _1st_ShadeColor.rgb;
    shadeColorLayer1 = shadeColorLayer1 * lightColor;
    // layer2
    float4 _2nd_shadeMap = tex2D(_2nd_ShadeMap, i.uv);
    float3 shadeColorLayer2 = _2nd_shadeMap.rgb * _2nd_ShadeColor.rgb;
    shadeColorLayer2 = shadeColorLayer2 * lightColor;

    // gradeMap
    #if defined (EnableGradeMap)
        float4 shadeGradeMap = tex2D(_ShadingGradeMap, i.uv);
        float shadeGradeMapLevel = shadeGradeMap.r + _Tweak_ShadingGradeMapLevel;
        float shadingGrade = shadeGradeMapLevel * lerp(halfLambert, halfLambertWithShadow, _SetSystemShadowsToBase);

        // blend layer 0-1-2
        float shadowMask = saturate(1.0 + (_1st_ShadeColor_Step - _1st_ShadeColor_Feather - shadingGrade ) / _1st_ShadeColor_Feather); 
        float3 blendLayer_01 = lerp(shadeColorLayer0, shadeColorLayer1, shadowMask);
        
        float blendFactor_12 = saturate(1.0 + (_2nd_ShadeColor_Step - _2nd_ShadeColor_Feather - shadingGrade) / _2nd_ShadeColor_Feather); 
        float3 blendLayer_12 = lerp(shadeColorLayer1, shadeColorLayer2, blendFactor_12);
        
        float3 color  = lerp(blendLayer_01, blendLayer_12, shadowMask); 
        color  = lerp(color , blendLayer_01,  _Is_1st_ShadeColorOnly);
    #else
        float _2ndShadePosition = tex2D(_Set_2nd_ShadePosition, i.uv).r;
        float _1stShadePosition = tex2D(_Set_1st_ShadePosition, i.uv).r;
        float shadowMask = calFeatherMask(halfLambertWithShadow, _BaseColor_Step, _BaseShade_Feather, _1stShadePosition);
        float _1st2nd_Mask = calFeatherMask(halfLambert, _ShadeColor_Step, _1st2nd_Shades_Feather, _2ndShadePosition);
        float3 shadeColorLayer12 = lerp(shadeColorLayer1, shadeColorLayer2, _1st2nd_Mask);
        float3 color = lerp(shadeColorLayer0, shadeColorLayer12, shadowMask);
    #endif