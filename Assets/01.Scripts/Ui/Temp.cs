using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp : MonoBehaviour
{
    [SerializeField] public GameObject skillCardPrefab;
    [SerializeField] public Transform skillCardParent;

    public void SkillCard()
    {
        GameObject SkillPrefabs = Instantiate(skillCardPrefab, skillCardParent);
    }

    public void LoadingScene()
    {
        SceneManager.LoadScene("Loading");
    }
}