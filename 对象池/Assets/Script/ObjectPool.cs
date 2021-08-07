using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;   //单例
    public GameObject[] prefabObjects;   //prefab数组
    public Dictionary<string, List<GameObject>> pool = new Dictionary<string, List<GameObject>>();  //对象池字典
    //U3D的单例机制，是第一个挂载脚本的对象，就是该单例，后面再怎么重复挂载是无效的。因为挂载的时候就实例化instance了。
    void Awake()
    {
        instance = this;

    }
    //从池中获取     
    //正式项目最好用一个静态类来保存名字，防止出错
    public GameObject GetOut(string GameObjectName, Vector3 vector3)
    {
        GameObject gameObject;   //返回的gameObject
        //如果池中有
        if (pool.ContainsKey(GameObjectName) && pool[GameObjectName].Count > 0)
        {
            //取池里的用
            gameObject = pool[GameObjectName][0];
            gameObject.SetActive(true);
            gameObject.transform.position = vector3;
            //取完移除
            pool[GameObjectName].RemoveAt(0);
        }
        //如果没有
        else
        {
            GameObject prefabObject = null;
            //要生成的prefabObject   prefab数组中的物体名字要和传入的字符串一致  
            for (int i = 0; i < prefabObjects.Length; i++)
            {
                if (prefabObjects[i].name == GameObjectName)
                {
                    prefabObject = prefabObjects[i];
                }
            }
            //直接创建
            Debug.Log("直接创建 ",prefabObject);
            gameObject = (GameObject)GameObject.Instantiate(prefabObject, vector3, Quaternion.identity);
        }
        return gameObject;
    }
    //存入对象池
    public void SetIn(string GameObjectName, GameObject gameObject)
    {
        //池中没有
        if (!pool.ContainsKey(GameObjectName))
        {   //新建池List
            pool.Add(GameObjectName, new List<GameObject>());
        }
        //存入池
        gameObject.SetActive(false);
        gameObject.transform.SetParent(transform);
        pool[GameObjectName].Add(gameObject);
        Debug.Log("存入列表中的物件名字 ："+GameObjectName+"对应的列表长度"+pool[GameObjectName].Count);
    }
    //销毁对象池
    public void DestroyPool(string GameObjectName)
    {
        if (pool.ContainsKey(GameObjectName))
        {  //删除对象
            for (int i = 0; i < pool[GameObjectName].Count; i++)
            {
                Destroy(pool[GameObjectName][i]);
            }
            //移除列表
            pool.Remove(GameObjectName);
        }
    }
}
