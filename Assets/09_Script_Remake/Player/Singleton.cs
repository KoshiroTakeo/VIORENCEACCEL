//=========================================================
// ジェネリックなシングルトン
// 一個しか作れない例
// 参考；https://www.youtube.com/watch?v=0LC5BgwPKOc
//=========================================================
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> // where このクラスのみを指定する
{
    protected virtual bool DestroyTargetGameManager => false;

    public static T I { get; private set; } = null;

    /// <summary>
    /// Singletonが有効か
    /// </summary>>
    public static bool IsValid() => I != null;

    private void Awake()
    {
        if(I == null)
        {
            I = this as T;
            I.Init();
            return;
        }
        if(DestroyTargetGameManager)
        {
            Debug.Log("destroy");
            Destroy(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// 派生クラス用のAwake
    /// </summary>>
    private void Init()
    {
        
    }

    private void OnDestroy()
    {
        if (I == this)
        {
            I = null;
        }
        OnRelease();
    }

    /// <summary>
    /// 派生クラス用のOnDestroy
    /// </summary>>
    protected virtual void OnRelease()
    {

    }
}
