Shader "Custom/Stencil"
{
    Properties
    {
        [IntRange]_StencilID("Stencil ID", Range(0, 255)) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry+2000"    
        }
        
        Pass
        {
            Blend Zero One
            Zwrite Off
            
            Stencil
            {
                Ref [_StencilID]
                Comp Always
                Pass Replace
                Fail Keep
            }
        }
    }
}