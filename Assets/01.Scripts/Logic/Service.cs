using UnityEngine;


public delegate void Func();
public interface IEnd { public void OnEnd(); }
public interface IHit { public void OnHit(int _dmg); }
public interface IItemEnter { public void OnItem(); }

public class Service
{
    public static readonly WaitForSeconds wait = new WaitForSeconds(1f);

    /// <summary>
    /// _parent 하위 오브젝트 중 특정 이름의 오브젝트 정보를 가져오는 메서드
    /// </summary>
    /// <param name="_parent"></param>
    /// <param name="_childName"></param>
    /// <returns></returns>
    public static Transform FindChild(Transform _parent, string _childName)
    {
        var child = TryFindChild(_parent, _childName);
        if (child == null) Log($"{_parent.name}에 {_childName}라는 하위 오브젝트는 존재하지 않음");

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

    /// <summary>
    /// 홍대병 걸려버린 Distance구현
    /// <returns></returns>
    public static float Distance(Vector2 _targetPos, Vector2 _startPos)
    {
        var distance = _targetPos - _startPos;
        var resoult = (distance.x * distance.x) + (distance.y * distance.y);

        return resoult < 0 ? resoult * -1 : resoult;
    }

    /// <summary>
    /// 디버깅 가능하게 Resource 데이터 불러오기
    /// <returns></returns>
    public static GameObject FindResource(string _fileName, string _resourceName)
    {
        var findObject = Resources.Load<GameObject>($"{_fileName}/{_resourceName}").gameObject;
        if (findObject == null) Log($"{_resourceName}은 존재하지 않는 리소스");

        return findObject;
    }

    public static int SpawnMonster(Transform _mapPos, string[] _monsterNameArray, int _maxSpawnCount)
    {
        if (_maxSpawnCount < 3) Log($"{_maxSpawnCount} 스폰 최대 수치가 3보다 이하일 수는 없습니다.");

        //스폰할 몬스터 정보
        GameObject[] monster = new GameObject[_monsterNameArray.Length];

        for (int i = 0; i < _monsterNameArray.Length; i++)
        {
            monster[i] = Service.FindResource("Enemy", _monsterNameArray[i]);
        }

        //몬스터 배열만큼 랜덤 스폰 및 지정
        var ranSpawnCount = Random.Range(3, _maxSpawnCount);
        var ranMonsterType = Random.Range(0, _monsterNameArray.Length);
        var isRandom = ranMonsterType == 0 ? false : true;

        for (int i = 0; i < ranSpawnCount; i++)
        {
            //스폰 타입 랜덤
            if (isRandom) ranMonsterType = Random.Range(0, _monsterNameArray.Length);
            else ranMonsterType = 0;

            var monsterPos = _mapPos.transform.position;
            monsterPos.x = Random.Range(0, 5);
            monsterPos.y = Random.Range(0, 5);

            var spawnMonster = MonoBehaviour.Instantiate(monster[ranMonsterType]);
            spawnMonster.name = $"{monster[ranMonsterType].name}_{i}";
            spawnMonster.GetComponent<MonsterBase>().SetMonster();

            spawnMonster.transform.position = _mapPos.transform.position + monsterPos;
            spawnMonster.gameObject.SetActive(true);
        }

        return ranSpawnCount;
    }


#if UNITY_EDITOR
    /// <summary>
    /// 에디터 디버깅 전용 로그
    /// </summary>
    /// <param name="_info"></param>
    public static void Log(string _info)
    {
        Debug.Log(_info);
    }
#endif
}
