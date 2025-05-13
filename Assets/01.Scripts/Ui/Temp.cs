using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp : MonoBehaviour
{
    [SerializeField] public GameObject skillCardPrefab;
    [SerializeField] public Transform skillCardParent;

    [SerializeField] private string str;

    public void SkillCard()
    {
        GameObject SkillPrefabs = Instantiate(skillCardPrefab, skillCardParent);
    }
}