//=========================================================
// �W�F�l���b�N�ȃV���O���g��
// ��������Ȃ���
// �Q�l�Ghttps://www.youtube.com/watch?v=0LC5BgwPKOc
//=========================================================
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> // where ���̃N���X�݂̂��w�肷��
{
    protected virtual bool DestroyTargetGameManager => false;

    public static T I { get; private set; } = null;

    /// <summary>
    /// Singleton���L����
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
    /// �h���N���X�p��Awake
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
    /// �h���N���X�p��OnDestroy
    /// </summary>>
    protected virtual void OnRelease()
    {

    }
}
