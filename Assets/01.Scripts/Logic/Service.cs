using UnityEngine;

public delegate void Func();
public interface IHit { public void OnHit(int _dmg); }

public class Service
{
    public static Transform FindChild(Transform _parent, string _childName)
    {
        var child = TryFindChild(_parent, _childName);
        if (child == null) Debug.Log($"{_parent.name}�� {_childName}��� ���� ������Ʈ�� �������� ����");

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

    public static float Distance(Vector2 _targetPos, Vector2 _startPos)
    {
        //ȫ�뺴 �ɷ����� Distance����
        var distance = _targetPos - _startPos;
        var resoult = (distance.x * distance.x) + (distance.y * distance.y);

        return resoult < 0 ? resoult * -1 : resoult;
    }

    public static GameObject FindResource(string _fileName, string _resourceName)
    {
        var findObject = Resources.Load<GameObject>($"{_fileName}/{_resourceName}").gameObject;
        if (findObject == null) Debug.Log($"{_resourceName}�� �������� �ʴ� ���ҽ�");

        return findObject;
    }
}
