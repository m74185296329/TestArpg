using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Inst => (inst);
    private static UIManager inst;
    private void Awake()
    {
        inst = this;
    }

    public T OpenUI<T>(bool forceCreate = false) where T : UIBase
    {
        if (!forceCreate)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var item = transform.GetChild(i);

                if (item.name == typeof(T).Name)
                {
                    item.gameObject.SetActive(true);
                    return item.GetComponent<T>();
                }

            }
        }
        

        //¥[¸ü¤@¤UUI
        var tmp = Resources.Load("UI/" + typeof(T).Name);
        var uiitem = Instantiate(tmp) as GameObject;
        uiitem.name = tmp.name;
        uiitem.transform.parent = transform;
        uiitem.transform.localPosition = Vector3.zero;
        uiitem.transform.localRotation = Quaternion.identity;
        uiitem.transform.localScale = Vector3.one;
        return uiitem.GetComponent<T>();
    }

    public void CloseUI<T>(T t,bool destroy = false) where T : UIBase
    {
        if (destroy)
        {
            Destroy(t.gameObject);
        }
        else
        {
            t.gameObject.SetActive(false);
        }
    }

    public void ClearAllUI()
    {
        while (transform.childCount > 0)
        {
            var item = transform.GetChild(0);
            Destroy(item.gameObject);
        }
    }
}
