using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("时间控制参数")]
    public float activeTime;//显示时间
    public float activeStart;//开始显示的时间点

    [Header("不透明度控制")]
    private float alpha;
    public float alphaSet;//初始值
    public float alphaMultiplier;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.flipX = playerSprite.flipX;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }

    void Update()
    {
        alpha *= alphaMultiplier;

        color = new Color(0.5f, 0.5f, 1, alpha);//Color(1,1,1,1)代表100%显示各通道颜色，请查看Api手册

        thisSprite.color = color;

        if (Time.time >= activeStart + activeTime)
        {
            //返回对象池
            ObjectPool.instance.SetIn("Shadow",this.gameObject);
        }
    }
}
