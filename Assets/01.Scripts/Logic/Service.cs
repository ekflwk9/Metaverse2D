using UnityEngine;


public delegate void Func();
public interface IEnd { public void OnEnd(); }
public interface IHit { public void OnHit(int _dmg); }
public interface IItemEnter { public void OnItem(); }

public class Service
{
    public static readonly WaitForSeconds wait = new WaitForSeconds(1f);

    /// <summary>
    /// _parent ���� ������Ʈ �� Ư�� �̸��� ������Ʈ ������ �������� �޼���
    /// </summary>
    /// <param name="_parent"></param>
    /// <param name="_childName"></param>
    /// <returns></returns>
    public static Transform FindChild(Transform _parent, string _childName)
    {
        var child = TryFindChild(_parent, _childName);
        if (child == null) Log($"{_parent.name}�� {_childName}��� ���� ������Ʈ�� �������� ����");

        return child;
    }

    private static Transform TryFindChild(Transform _parent, string _childName)
    {
        //ȣ���ϴ� �޼��� �ƴ�
        Transform findChild = null;

        for (int i = 0; i < _parent.childCount; i++)
        {
            var child = _parent.GetChild(i);
            findChild = child.name == _childName ? child : TryFindChild(child, _childName);
            if (findChild != null) break;
        }

        return findChild;
    }

    /// <summary>
    /// ȫ�뺴 �ɷ����� Distance����
    /// <returns></returns>
    public static float Distance(Vector2 _targetPos, Vector2 _startPos)
    {
        var distance = _targetPos - _startPos;
        var resoult = (distance.x * distance.x) + (distance.y * distance.y);

        return resoult < 0 ? resoult * -1 : resoult;
    }

    /// <summary>
    /// ����� �����ϰ� Resource ������ �ҷ�����
    /// <returns></returns>
    public static GameObject FindResource(string _fileName, string _resourceName)
    {
        var findObject = Resources.Load<GameObject>($"{_fileName}/{_resourceName}").gameObject;
        if (findObject == null) Log($"{_resourceName}�� �������� �ʴ� ���ҽ�");

        return findObject;
    }

#if UNITY_EDITOR
    /// <summary>
    /// ������ ����� ���� �α�
    /// </summary>
    /// <param name="_info"></param>
    public static void Log(string _info)
    {
        Debug.Log(_info);
    }
#endif
}
