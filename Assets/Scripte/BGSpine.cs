using UnityEngine;
using System.Collections;
/*
 大好河山
     */
public class BGSpine : MonoBehaviour {
    public enum AnimStyle
    {
        TOP,
        Left,
        Right,
    }
    public UILabel mText;                                   //文字
    public SpriteRenderer mLeftOpenDoor;                    //开场left
    public SpriteRenderer mRightOpenDoor;                   //开场right
    public SpriteRenderer mCenterMidToBig;                  //开场2
    public SkinnedMeshRenderer mTextureMaterial;            //影集贴图
    public Texture[] mTexArry = new Texture[10];            //根据每个相册顺序放入数组


    private void Start()
    {
        mTextureMaterial.material.mainTexture = mTexArry[0];
        InitAnim();
    }


    void InitAnim()
    {
        mText.text = "大好河山";
        vp_Timer.In(3, LabelToAlpha);
        vp_Timer.In(4, OpenDoor);
    }


    /// <summary>
    /// 字体动画
    /// </summary>
    void LabelToAlpha()
    {
        AnimaMgr.instance.LabelStyle(mText);
    }


    /// <summary>
    /// 开门
    /// </summary>
    void OpenDoor()
    {
        AnimaMgr.instance.ShowPanelStyle(mLeftOpenDoor.gameObject,OpenPanelStyle.CenterToLeft);
        AnimaMgr.instance.ShowPanelStyle(mRightOpenDoor.gameObject, OpenPanelStyle.CenterToRight);
        CenterMidToBig();
    }


    /// <summary>
    /// 中间由小变大
    /// </summary>
    void CenterMidToBig()
    {
        mCenterMidToBig.gameObject.SetActive(true);
        AnimaMgr.instance.ShowPanelStyle(mCenterMidToBig.gameObject, OpenPanelStyle.MiddleToBig);
        AnimaMgr.instance.LabelStyle(mText);
        vp_Timer.In(2, LabelToAlpha);
    }

    void InstanceLabel()
    {
       GameObject go= Instantiate(Resources.Load<GameObject>("Label"),Vector2.zero,Quaternion.identity) as GameObject;
        go.transform.SetParent(GameObject.Find("UI Root").transform);
    }
}
