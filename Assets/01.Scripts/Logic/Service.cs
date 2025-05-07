using UnityEngine;

public delegate void Func();
public interface IHit { public void OnHit(int _dmg); }

public class Service
{
    public static Transform FindChild(Transform _parent, string _childName)
    {
        var child = TryFindChild(_parent, _childName);
        if (child == null) Debug.Log($"{_parent.name}에 {_childName}라는 하위 오브젝트는 존재하지 않음");

        return child;
    }

    private static Transform TryFindChild(Transform _parent, string _childName)
    {
        //호출하는 메서드 아님
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
        //홍대병 걸려버린 Distance구현
        var distance = _targetPos - _startPos;
        var resoult = (distance.x * distance.x) + (distance.y * distance.y);

        return resoult < 0 ? resoult * -1 : resoult;
    }

    public static GameObject FindResource(string _fileName, string _resourceName)
    {
        var findObject = Resources.Load<GameObject>($"{_fileName}/{_resourceName}").gameObject;
        if (findObject == null) Debug.Log($"{_resourceName}은 존재하지 않는 리소스");

        return findObject;
    }
}
