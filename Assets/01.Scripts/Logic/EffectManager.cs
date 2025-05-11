using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    private int[] floorBloodCount = { 0, 0, 0 };
    public Dictionary<string, GameObject[]> effect = new Dictionary<string, GameObject[]>();

    public void Load()
    {
        var poolingCount = 50;
        var effects = Resources.LoadAll<GameObject>("Effect");

        for (int i = 0; i < effects.Length; i++)
        {
            if (!effect.ContainsKey(effects[i].name))
            {
                var pooling = new GameObject[poolingCount];

                for (int I = 0; I < poolingCount; I++)
                {
                    var poolingEffect = MonoBehaviour.Instantiate(effects[i]);
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
            Debug.Log($"{_effectName}�� Effect���Ͽ� ���� Effect�Դϴ�.");
        }
    }

    public void FloorBlood(Vector3 _spawnPos)
    {
        var bloodType = Random.Range(0, 3);
        floorBloodCount[bloodType]++;

        if (floorBloodCount[bloodType] >= effect[$"FloorBlood{bloodType}"].Length)
        {
            floorBloodCount[bloodType] = 0;
        }

        //�������� ���
        if (!effect[$"FloorBlood{bloodType}"][floorBloodCount[bloodType]].activeSelf)
        {
            effect[$"FloorBlood{bloodType}"][floorBloodCount[bloodType]].SetActive(true);
        }

        //��ġ ����
        effect[$"FloorBlood{bloodType}"][floorBloodCount[bloodType]].transform.position = _spawnPos;
    }
}
