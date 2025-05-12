using System.Collections.Generic;
using UnityEngine;

public enum DmgTypeCode
{
    Damage = 0,
    CriticalDamage = 1,
}

public class EffectManager
{
    private int damageCount;
    private int[] floorBloodCount = { 0, 0, 0 };

    public Dictionary<string, GameObject[]> effect = new Dictionary<string, GameObject[]>();
    public Dictionary<string, DamageComponent[]> damage = new Dictionary<string, DamageComponent[]>();

    public void Load()
    {
        var poolingCount = 50;
        var effects = Resources.LoadAll<GameObject>("Effect");

        for (int i = 0; i < effects.Length; i++)
        {
            if (!effect.ContainsKey(effects[i].name))
            {
                var component = effects[i].GetComponent<DamageComponent>();

                if (component != null) LoadDamage(component, poolingCount);
                else LoadEffect(effects[i], poolingCount);
            }

            else Service.Log($"{effects[i]}��� ���� ������ �̹� ������");
        }
    }

    private void LoadEffect(GameObject _resource, int _poolingCount)
    {
        var pooling = new GameObject[_poolingCount];

        for (int i = 0; i < _poolingCount; i++)
        {
            var poolingEffect = MonoBehaviour.Instantiate(_resource);
            pooling[i] = poolingEffect;
            poolingEffect.SetActive(false);
        }

        effect.Add(_resource.name, pooling);
    }

    private void LoadDamage(DamageComponent _resource, int _poolingCount)
    {
        var pooling = new DamageComponent[_poolingCount];

        for (int i = 0; i < _poolingCount; i++)
        {
            var poolingEffect = MonoBehaviour.Instantiate(_resource);
            pooling[i] = poolingEffect;
            pooling[i].SetComponent();
        }

        damage.Add(_resource.name, pooling);
    }

    /// <summary>
    /// Ư�� ��ġ�� ���ϴ� ����Ʈ�� �������
    /// </summary>
    /// <param name="_spawnPos"></param>
    /// <param name="_effectName"></param>
    public void Show(Vector3 _spawnPos, string _effectName)
    {
        if (effect.ContainsKey(_effectName))
        {
            for (int i = 0; i < effect[_effectName].Length; i++)
            {
                if (!effect[_effectName][i].activeSelf)
                {
                    effect[_effectName][i].SetActive(true);
                    effect[_effectName][i].transform.position = _spawnPos;
                    break;
                }
            }
        }

        else
        {
            Service.Log($"{_effectName}�� Effect���Ͽ� ���� Effect�Դϴ�.");
        }
    }

    /// <summary>
    /// Ư�� �ٴ� ��ġ�� �� �ڱ��� �������
    /// </summary>
    /// <param name="_spawnPos"></param>
    public void FloorBlood(Vector3 _spawnPos)
    {
        var bloodType = Random.Range(0, 3);
        var typeName = $"FloorBlood{bloodType}";

        floorBloodCount[bloodType]++;

        if (floorBloodCount[bloodType] >= effect[typeName].Length)
        {
            floorBloodCount[bloodType] = 0;
        }

        //�������� ���
        if (!effect[typeName][floorBloodCount[bloodType]].activeSelf)
        {
            effect[typeName][floorBloodCount[bloodType]].SetActive(true);
        }

        //��ġ ����
        effect[typeName][floorBloodCount[bloodType]].transform.position = _spawnPos;
    }

    /// <summary>
    /// Ư�� ��ü �����ǿ� ������ ����Ʈ�� �������
    /// </summary>
    /// <param name="_spawnPos"></param>
    /// <param name="_damageValue"></param>
    /// <param name="dmgType"></param>
    public void Damage(Vector3 _spawnPos, int _damageValue, DmgTypeCode dmgType = DmgTypeCode.Damage)
    {
        damageCount++;
        var typeName = dmgType.ToString();

        if (damage.ContainsKey(typeName))
        {
            if (damageCount >= damage[typeName].Length) damageCount = 0;
            damage[typeName][damageCount].Show(_spawnPos, _damageValue);
        }

        else Service.Log($"{typeName}�� ���� Ÿ���Դϴ�.");
    }
}
