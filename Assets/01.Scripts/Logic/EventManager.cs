using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EventManager
{
    private StringBuilder eventName = new StringBuilder(40);

    private event Func end;

    private Dictionary<string, IHit> hit = new Dictionary<string, IHit>();
    private Dictionary<string, IItemEnter> item = new Dictionary<string, IItemEnter>();

    private Dictionary<string, Func> gameEvent = new Dictionary<string, Func>();
    private Dictionary<string, Func> constEvent = new Dictionary<string, Func>();

    public void SetComponent(MonoBehaviour _component)
    {
        if (_component is IHit isHit)
        {
            if (!hit.ContainsKey(_component.name)) hit.Add(_component.name, isHit);
        }

        if (_component is IEnd isEnd)
        {
            end += isEnd.OnEnd;
        }

        if (_component is IItemEnter isItem)
        {
            if (!item.ContainsKey(_component.name)) item.Add(_component.name, isItem);
        }
    }

    public void Reset()
    {
        //이벤트 정리
        hit.Clear();
        gameEvent.Clear();
    }

    public void EndEvent()
    {
        //씬 전환시 이벤트
        end?.Invoke();
        end = null;
    }

    public void Add(Func _function, bool _constEvent = false)
    {
        //오브젝트 이름 + _function 이름
        eventName.Clear();
        var isClass = _function.Target;

        if (isClass is MonoBehaviour unityObject) eventName.Append(unityObject.name);
        else Service.Log($"{eventName}의 클래스는 MonoBehaviour의 상속을 받지 않음");

        eventName.Append(_function.Method.Name);

        if (!_constEvent)
        {
            if (!gameEvent.ContainsKey(eventName.ToString())) gameEvent.Add(eventName.ToString(), _function);
            else Service.Log($"{eventName}는 이미 추가된 \"gameEvent\"");
        }

        else
        {
            if (!constEvent.ContainsKey(eventName.ToString())) constEvent.Add(eventName.ToString(), _function);
            else Service.Log($"{eventName}는 이미 추가된 \"constEvent\"");
        }
    }

    public void Call(string _eventName)
    {
        if (gameEvent.ContainsKey(_eventName)) gameEvent[_eventName]();
        else if (constEvent.ContainsKey(_eventName)) constEvent[_eventName]();
        else Service.Log($"{_eventName}는 추가되지 않은 \"constEvent\"");
    }

    public void Hit(string _hitObjectName, int _hitValue = 0)
    {
        if (hit.ContainsKey(_hitObjectName)) hit[_hitObjectName].OnHit(_hitValue);
        else Service.Log($"{_hitObjectName}는 추가되지 않은 \"Hit Interface\"");
    }

    public void GetItem(string _itemName)
    {
        if (item.ContainsKey(_itemName)) item[_itemName].OnItem();
        else Service.Log($"{_itemName}는 추가되지 않은 \"Item Interface\"");
    }
}
