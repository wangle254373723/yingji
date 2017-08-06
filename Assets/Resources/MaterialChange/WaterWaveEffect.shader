Shader "Custom/WaterWaveEffect" {
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}

		//以下属性其实不用开放出来，通过脚本控制就好
		_timeFactor("Time(时间)", float) = 22
		_waveSpeend("WaveSpend(速度)" ,Range(0.3, 2))=2
		_waveHight("WaveStyle(效果)" ,Range(1, 100)) =100
	}

		CGINCLUDE
#include "UnityCG.cginc"  
	uniform sampler2D _MainTex;
	uniform float _distanceFactor;
	uniform float _timeFactor;
	uniform float _totalFactor;
	uniform float _waveSpeend;
	uniform float _waveHight;
	uniform float _curWaveDis;

	fixed4 frag(v2f_img i) : SV_Target
	{
		//计算uv到中间点的向量(向外扩，反过来就是向里缩)  
		float2 dv = float2(0.5, 0.5) - i.uv;
		//按照屏幕长宽比进行缩放  
		dv = dv * float2(_ScreenParams.x / 2 / _ScreenParams.y / 2, -1);
		//计算像素点距中点的距离  
		float dis = sqrt(dv.x * dv.x + dv.y * dv.y);
		//用sin函数计算出波形的偏移值factor  
		//dis在这里都是小于1的，所以我们需要乘以一个比较大的数，比如60，这样就有多个波峰波谷  
		//sin函数是（-1，1）的值域，我们希望偏移值很小，所以这里我们缩小100倍，据说乘法比较快,so...  
		float sinFactor = sin(dis * abs(_waveHight) + _Time.y * _totalFactor) * 30 * 0.01;
		//距离当前波纹运动点的距离，如果小于waveWidth才予以保留，否则已经出了波纹范围，factor通过clamp设置为0 
		//0.3-波纹宽度  _timeFactor控制什么时候触发
		float discardFactor = clamp(0.3 - abs((_Time.y - _timeFactor) * _waveSpeend - dis), 0, 1);
		//归一化  
		float2 dv1 = normalize(dv);
		//计算每个像素uv的偏移值  
		float2 offset = dv1  * sinFactor * discardFactor;
		//像素采样时偏移offset  
		float2 uv = offset + i.uv;
		return tex2D(_MainTex, uv);
	}

		ENDCG

		SubShader
	{
		Pass
		{
			ZTest Always
			Cull Off
			ZWrite Off
			Fog{ Mode off }

			CGPROGRAM
#pragma vertex vert_img  
#pragma fragment frag  
#pragma fragmentoption ARB_precision_hint_fastest   
			ENDCG
		}
	}
	Fallback off
}
