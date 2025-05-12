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

            else Service.Log($"{effects[i]}라는 같은 파일이 이미 존재함");
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
    /// 특정 위치에 원하는 임펙트를 재생해줌
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
            Service.Log($"{_effectName}은 Effect파일에 없는 Effect입니다.");
        }
    }

    /// <summary>
    /// 특정 바닥 위치에 피 자국을 만들어줌
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

        //꺼져있을 경우
        if (!effect[typeName][floorBloodCount[bloodType]].activeSelf)
        {
            effect[typeName][floorBloodCount[bloodType]].SetActive(true);
        }

        //위치 설정
        effect[typeName][floorBloodCount[bloodType]].transform.position = _spawnPos;
    }

    /// <summary>
    /// 특정 객체 포지션에 데미지 임펙트를 출력해줌
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

        else Service.Log($"{typeName}은 없는 타입입니다.");
    }
}
