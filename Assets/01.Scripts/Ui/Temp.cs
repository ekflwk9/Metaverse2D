using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] public GameObject skillCardPrefab;
    [SerializeField] public Transform skillCardParent;

    public void SkillCard()
    {
        GameObject skillCard = Instantiate(skillCardPrefab, skillCardParent);
    }
}
