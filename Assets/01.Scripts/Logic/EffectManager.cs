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
        var effects = Resources.LoadAll<MonoBehaviour>("Effect");

        for (int i = 0; i < effects.Length; i++)
        {
            //데미지
            if (effects[i].name.Contains("Damage"))
            {
                var pooling = new DamageComponent[poolingCount];

                for (int I = 0; I < poolingCount; I++)
                {
                    var poolingEffect = MonoBehaviour.Instantiate(effects[i].gameObject);
                    pooling[I] = poolingEffect.GetComponent<DamageComponent>();
                    pooling[I].SetComponent();
                }

                damage.Add(effects[i].name, pooling);
            }

            //임펙트
            else if (!effect.ContainsKey(effects[i].name))
            {
                var pooling = new GameObject[poolingCount];

                for (int I = 0; I < poolingCount; I++)
                {
                    var poolingEffect = MonoBehaviour.Instantiate(effects[i].gameObject);
                    pooling[I] = poolingEffect;
                    poolingEffect.SetActive(false);
                }

                effect.Add(effects[i].name, pooling);
            }
        }
    }

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
            Debug.Log($"{_effectName}은 Effect파일에 없는 Effect입니다.");
        }
    }

    public void FloorBlood(Vector3 _spawnPos)
    {
        var bloodType = Random.Range(0, 3);
        var typeName = $"FloorBlood{bloodType}";

        floorBloodCount[bloodType]++;
        var index = floorBloodCount[bloodType];

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

    public void Damage(Vector3 _spawnPos, int _damageValue, DmgTypeCode dmgType = DmgTypeCode.Damage)
    {
        damageCount++;
        var typeName = dmgType.ToString();

        if (damage.ContainsKey(typeName))
        {
            if (damageCount >= damage[typeName].Length) damageCount = 0;
            damage[typeName][damageCount].Show(_spawnPos, _damageValue);
        }

        else Debug.Log($"{typeName}은 없는 타입입니다.");
    }
}
