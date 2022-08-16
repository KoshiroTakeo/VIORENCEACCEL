//============================================================
// 可用性あるシングルトン
//======================================================================
// 開発履歴
// 20220813:https://www.youtube.com/watch?v=0LC5BgwPKOc 参考
//======================================================================
using UnityEngine;

public class Generic_Singleton<T> : MonoBehaviour where T : Generic_Singleton<T> // where このクラスのみを指定する
{
    protected virtual bool DestroyTargetGameManager => false;

    public static T Instance { get; private set; } = null;

    /// <summary>
    /// Singletonが有効か
    /// </summary>>
    public static bool IsValid() => Instance != null;

    private void Awake()
    {
        Debug.Log("aaaaaa");
        if (Instance == null)
        {
            Instance = this as T;
            Instance.Init();
            return;
        }
        if (DestroyTargetGameManager)
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
        Debug.Log("instance");
    }

    private void OnDestroy()
    {
        // 自分自身であるか確認する
        if (Instance == this)
        {
            Instance = null;
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
