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
        //오브젝트 이름 + _function 이름
        var eventName = _function.Method.Name;
        var isClass = _function.Target;

        if (isClass is MonoBehaviour unityObject) eventName = $"{unityObject.name}{eventName}";
        else Debug.Log($"{eventName}의 클래스는 MonoBehaviour의 상속을 받지 않음");

        if (!_constEvent)
        {
            if (!gameEvent.ContainsKey(eventName)) gameEvent.Add(eventName, _function);
            else Debug.Log($"{eventName}는 이미 추가된 \"gameEvent\"");
        }

        else
        {
            if (!constEvent.ContainsKey(eventName)) constEvent.Add(eventName, _function);
            else Debug.Log($"{eventName}는 이미 추가된 \"constEvent\"");
        }
    }

    public void Call(string _eventName)
    {
        if (gameEvent.ContainsKey(_eventName)) gameEvent[_eventName]();
        else if (constEvent.ContainsKey(_eventName)) constEvent[_eventName]();
        else Debug.Log($"{_eventName}는 추가되지 않은 \"constEvent\"");
    }

    public void Hit(string _hitObjectName, int _hitValue = 0)
    {
        if (hit.ContainsKey(_hitObjectName)) hit[_hitObjectName].OnHit(_hitValue);
        else Debug.Log($"{_hitObjectName}는 추가되지 않은 \"Hit Interface\"");
    }
}
