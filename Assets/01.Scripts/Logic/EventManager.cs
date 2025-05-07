using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private Dictionary<string, IHit> hit = new Dictionary<string, IHit>();
    private Dictionary<string, Func> gameEvent = new Dictionary<string, Func>();
    private Dictionary<string, Func> constEvent = new Dictionary<string, Func>();

    public void SetComponent(MonoBehaviour _component)
    {
        if (_component is IHit isHit)
        {
            if (!hit.ContainsKey(_component.name)) hit.Add(_component.name, isHit);
        }
    }

    public void Reset()
    {
        hit.Clear();
        gameEvent.Clear();
    }

    public void Add(Func _function, bool _constEvent = false)
    {
        //������Ʈ �̸� + _function �̸�
        var eventName = _function.Method.Name;
        var isClass = _function.Target;

        if (isClass is MonoBehaviour unityObject) eventName = $"{unityObject.name}{eventName}";
        else Debug.Log($"{eventName}�� Ŭ������ MonoBehaviour�� ����� ���� ����");

        if (!_constEvent)
        {
            if (!gameEvent.ContainsKey(eventName)) gameEvent.Add(eventName, _function);
            else Debug.Log($"{eventName}�� �̹� �߰��� \"gameEvent\"");
        }

        else
        {
            if (!constEvent.ContainsKey(eventName)) constEvent.Add(eventName, _function);
            else Debug.Log($"{eventName}�� �̹� �߰��� \"constEvent\"");
        }
    }

    public void Call(string _eventName)
    {
        if (gameEvent.ContainsKey(_eventName)) gameEvent[_eventName]();
        else if (constEvent.ContainsKey(_eventName)) constEvent[_eventName]();
        else Debug.Log($"{_eventName}�� �߰����� ���� \"constEvent\"");
    }

    public void Hit(string _hitObjectName, int _hitValue = 0)
    {
        if (hit.ContainsKey(_hitObjectName)) hit[_hitObjectName].OnHit(_hitValue);
        else Debug.Log($"{_hitObjectName}�� �߰����� ���� \"Hit Interface\"");
    }
}
