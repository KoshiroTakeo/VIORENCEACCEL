//============================================================
// �p������V���O���g��
//======================================================================
// �J������
// 20220813:https://www.youtube.com/watch?v=0LC5BgwPKOc �Q�l
//======================================================================
using UnityEngine;

public class Generic_Singleton<T> : MonoBehaviour where T : Generic_Singleton<T> // where ���̃N���X�݂̂��w�肷��
{
    protected virtual bool DestroyTargetGameManager => false;

    public static T Instance { get; private set; } = null;

    /// <summary>
    /// Singleton���L����
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
    /// �h���N���X�p��Awake
    /// </summary>>
    private void Init()
    {
        Debug.Log("instance");
    }

    private void OnDestroy()
    {
        // �������g�ł��邩�m�F����
        if (Instance == this)
        {
            Instance = null;
        }
        OnRelease();
    }

    /// <summary>
    /// �h���N���X�p��OnDestroy
    /// </summary>>
    protected virtual void OnRelease()
    {

    }
}
