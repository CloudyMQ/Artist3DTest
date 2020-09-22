Shader "Custom/SimpleVertexExtrusionShader"
{
Properties
{
_Color ("Color", Color) = (1,1,1,1)
_Amount ("Extrusion Amount", Range(0,1)) = 0.5
}
SubShader
{
Tags { "RenderType"="Opaque" }
LOD 200

CGPROGRAM

#pragma surface surf Standard fullforwardshadows vertex:vert

#pragma target 3.0

struct Input
{
float4 color : COLOR;
};

fixed4 _Color;
float _Amount;

void vert (inout appdata_full v)
{
v.vertex.xyz += v.normal * _Amount;
}
void surf (Input IN, inout SurfaceOutputStandard o)
{
fixed4 c = _Color;
o.Albedo = c.rgb;
}
ENDCG
}
FallBack "Diffuse"
}