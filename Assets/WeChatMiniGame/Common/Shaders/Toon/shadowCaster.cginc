
uniform sampler2D _ClippingMask; uniform float4 _ClippingMask_ST;
uniform float _Clipping_Level;
uniform fixed _Inverse_Clipping;
struct VertexInput {
    float4 vertex : POSITION;
};
struct VertexOutput {
    V2F_SHADOW_CASTER;
};
VertexOutput vert (VertexInput v) {
    VertexOutput o = (VertexOutput)0;
    o.pos = UnityObjectToClipPos( v.vertex );
    TRANSFER_SHADOW_CASTER(o)
    return o;
}
float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
    SHADOW_CASTER_FRAGMENT(i)
}
