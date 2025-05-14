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
        //�̺�Ʈ ����
        hit.Clear();
        item.Clear();
        gameEvent.Clear();
    }

    public void EndEvent()
    {
        //�� ��ȯ�� �̺�Ʈ
        end?.Invoke();
        end = null;
    }

    /// <summary>
    /// ������Ʈ �̸� + �޼��� �̸�
    /// ���� �Ѿ�� �̺�Ʈ�� �����Ϸ��� true�� �Է�����.
    /// </summary>
    /// <param name="_function"></param>
    /// <param name="_constEvent"></param>
    public void Add(Func _function, bool _constEvent = false)
    {
        eventName.Clear();
        var isClass = _function.Target;

        if (isClass is MonoBehaviour unityObject) eventName.Append(unityObject.name);
        else Service.Log($"{eventName}�� Ŭ������ MonoBehaviour�� ����� ���� ����");

        eventName.Append(_function.Method.Name);

        if (!_constEvent)
        {
            if (!gameEvent.ContainsKey(eventName.ToString())) gameEvent.Add(eventName.ToString(), _function);
            else Service.Log($"{eventName}�� �̹� �߰��� \"gameEvent\"");
        }

        else
        {
            if (!constEvent.ContainsKey(eventName.ToString())) constEvent.Add(eventName.ToString(), _function);
            else Service.Log($"{eventName}�� �̹� �߰��� \"constEvent\"");
        }
    }

    /// <summary>
    /// ������Ʈ �̸� + �޼��� �̸��� Ű���̴�.
    /// </summary>
    /// <param name="_eventName"></param>
    public void Call(string _eventName)
    {
        if (gameEvent.ContainsKey(_eventName)) gameEvent[_eventName]();
        else if (constEvent.ContainsKey(_eventName)) constEvent[_eventName]();
        else Service.Log($"{_eventName}�� �߰����� ���� \"constEvent\"");
    }

    /// <summary>
    /// ü���� ������ ������Ʈ�� �̸� / ���ҽ�ų ��
    /// </summary>
    /// <param name="_hitObjectName"></param>
    /// <param name="_hitValue"></param>
    public void Hit(string _hitObjectName, int _hitValue = 0)
    {
        if (hit.ContainsKey(_hitObjectName)) hit[_hitObjectName].OnHit(_hitValue);
        else Service.Log($"{_hitObjectName}�� �߰����� ���� \"Hit Interface\"");
    }

    /// <summary>
    /// ������ �޼��带 ������ ������Ʈ�� �̸�
    /// </summary>
    /// <param name="_itemName"></param>
    public void GetItem(string _itemName)
    {
        if (item.ContainsKey(_itemName)) item[_itemName].OnItem();
        else Service.Log($"{_itemName}�� �߰����� ���� \"Item Interface\"");
    }
}
