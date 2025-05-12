using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp : MonoBehaviour
{
    [SerializeField] public GameObject skillCardPrefab;
    [SerializeField] public Transform skillCardParent;

    [SerializeField] private string str;

    //public bool isCheat = false;

    public void SkillCard()
    {
        GameObject SkillPrefabs = Instantiate(skillCardPrefab, skillCardParent);
    }

    //private void Update()
    //{
    //    if (str != "WASDWSAD")
    //    {
    //        if (Input.GetKeyDown(KeyCode.W)) str += "W";
    //        if (Input.GetKeyDown(KeyCode.A)) str += "A";
    //        if (Input.GetKeyDown(KeyCode.S)) str += "S";
    //        if (Input.GetKeyDown(KeyCode.D)) str += "D";
    //    }
    //    else
    //    {
    //        isCheat = true;
    //    }

    //    if (isCheat)
    //    {
    //    }
    //}
}