﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/FlouHorizontal" {
Properties {
	_MainTex ("", 2D) = "" {}

}
SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off Fog { Mode off } //Parametrage du shader pour éviter de lire, écrire dans le zbuffer, désactiver le culling et le brouillard sur le polygone

			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			sampler2D _MainTex;

			struct Prog2Vertex {
	            float4 vertex : POSITION; 	//Les "registres" précisés après chaque variable servent
	            float4 tangent : TANGENT; 	//A savoir ce qu'on est censé attendre de la carte graphique.
	            float3 normal : NORMAL;		//(ce n'est pas basé sur le nom des variables).
	            float4 texcoord : TEXCOORD0;  
	            float4 texcoord1 : TEXCOORD1; 
	            fixed4 color : COLOR; 
        	 };
			 
			//Structure servant a transporter des données du vertex shader au pixel shader.
			//C'est au vertex shader de remplir a la main les informations de cette structure.
			struct Vertex2Pixel
			 {
           	 float4 pos : SV_POSITION;
           	 float4 uv : TEXCOORD0;

			 };  	 

			Vertex2Pixel vert (Prog2Vertex i)
			{
				Vertex2Pixel o;
		        o.pos = UnityObjectToClipPos (i.vertex); //Projection du modèle 3D, cette ligne est obligatoire
		        o.uv=i.texcoord; //UV de la texture
		      	
		      	return o;
			}

            float4 frag(Vertex2Pixel i) : COLOR 
            {
			float4 color;
			int cpt = 0;
			for(int j = -10; j < 10; j++)
			{
				color += tex2D(_MainTex, i.uv.xy + float2(j * 0.0015, 0.0));
				cpt++;
			}
                return color / cpt; //On renvoit la couleur reçue
            }
ENDCG 
	}
}

Fallback off

}