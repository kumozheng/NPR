/*
* @Athour: kumozheng
* @Date: 2021-11-04 16:26:51
 * @LastEditors: kumozheng
 * @LastEditTime: 2021-11-24 16:22:17
* @Description: 
 * @FilePath: /NPR/Assets/WeChatMiniGame/Common/Shaders/Toon/toonUtils.hlsl
*/
#ifndef __TOON_UTILS_HLSL_
    #define __TOON_UTILS_HLSL_

    inline half WrapRampNL(half nl, fixed threshold, fixed smoothness)
    {
        nl = nl * 0.5 + 0.5;
        #if USE_RAMPTEX
            nl = tex2D(_RampTex, fixed2(nl, nl)).r;
        #else
            nl = smoothstep(threshold - smoothness*0.5, threshold + smoothness*0.5, nl);
        #endif
        
        return nl;
    }
    
    inline half3 StylizedRim(half nv, half nl, half3 rimColor, fixed rimMin, fixed rimMax, fixed rimStrength)
    {
        half rim = 1-nv;
        rim = smoothstep(rimMin, rimMax, rim) * rimStrength;
        return rim * saturate(nl) * rimColor;
    }

    float2 rotateUV(float2 uv, float2 piv, float angle){
        float cos_Rot = cos(angle);
        float sin_Rot = sin(angle);
        float2 uv_rot = (mul(uv - piv, float2x2( cos_Rot, -sin_Rot, sin_Rot, cos_Rot)) + piv);
        return uv_rot;
    }

    float calFeatherMask(float halfLambert, float stepValue, float featherValue, float pValue){
        float maskInv = halfLambert - (stepValue - featherValue);
        maskInv = -1.0 * maskInv * pValue / featherValue;
        return saturate(1.0 + maskInv);
    }

#endif