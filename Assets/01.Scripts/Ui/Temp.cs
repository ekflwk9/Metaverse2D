using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] public GameObject skillCardPrefab;

    public void SkillCard()
    { 
        Instantiate(skillCardPrefab);
    }
}
