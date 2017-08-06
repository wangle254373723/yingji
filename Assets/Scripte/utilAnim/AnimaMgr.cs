using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 影集动画
     */
public class AnimaMgr : Sington<AnimaMgr>
{
    public void ShowPanelStyle(GameObject go, OpenPanelStyle style)
    {
        switch (style)
        {
            case OpenPanelStyle.None:
                break;
            case OpenPanelStyle.LeftToCenter:
                ShowHorizontal(go);
                break;
            case OpenPanelStyle.RightToCenter:
                ShowHorizontal(go,false);
                break;
            case OpenPanelStyle.TopToCenter:
                ShowVertical(go);
                break;
            case OpenPanelStyle.BottomToCenter:
                ShowVertical(go,false);
                break;
            case OpenPanelStyle.MiddleToBig:
                ShowMiddleToBig(go);
                break;
            case OpenPanelStyle.CenterToLeft:
                CenterToEdge(go,true);
                break;
            case OpenPanelStyle.CenterToRight:
                CenterToEdge(go,false);
                break;
        }
    }


    public void LabelStyle(UILabel label)
    {

        TweenAlpha ts = label.gameObject.GetComponent<TweenAlpha>();
        if (ts == null)
        {
            ts = label.gameObject.AddComponent<TweenAlpha>();
        }
        ts.@from = 1;
        ts.to = 0;
        ts.enabled = true;
        ts.duration = 1f;
    }


    public void ShowMiddleToBig(GameObject go)
    {
        TweenScale ts = go.GetComponent<TweenScale>();
        if (ts == null)
        {
            ts = go.gameObject.AddComponent<TweenScale>();
        }
        ts.@from = Vector3.zero;
        ts.to = Vector3.one;
        ts.duration = 1f;
    }

    public void ShowVertical(GameObject go, bool isTop = true)
    {
        go.gameObject.SetActive(true);
        TweenPosition tp = go.GetComponent<TweenPosition>();
        if (tp == null)
        {
            tp = go.gameObject.AddComponent<TweenPosition>();
        }

        tp.from = isTop ? new Vector3(0, 600, 0) : new Vector3(0, -600, 0);
        tp.to=Vector3.zero;
        tp.duration = 0.1f;
    }

    public void ShowHorizontal(GameObject go, bool isLeft = true)
    {
        go.gameObject.SetActive(true);
        TweenPosition tp = go.GetComponent<TweenPosition>();
        if (tp == null)
        {
            tp = go.gameObject.AddComponent<TweenPosition>();
        }
        tp.@from = isLeft ? new Vector3(600, 0, 0) : new Vector3(-600, 0, 0);
        tp.to=Vector3.zero;
        tp.duration = 0.1f;
    }


    public void  CenterToEdge(GameObject go,bool isLeft)
    {
        go.gameObject.SetActive(true);
        TweenPosition tp = go.GetComponent<TweenPosition>();
        if (tp == null)
        {
            tp = go.gameObject.AddComponent<TweenPosition>();
        }
        tp.@from = isLeft ? new Vector3(-2.04f, 0, 0) : new Vector3(2.04f, 0, 0);
        tp.to = isLeft ? new Vector3(-7, 0, 0) : new Vector3(7, 0, 0);
        tp.duration =1;
    }
}
