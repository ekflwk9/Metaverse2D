using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private float theNumberOfGhostImageWhichPlayerWillGetTurnsIntoDelayPerSecondsToCountWithTimeDeltaTimeToShowPlayerWithFantasticExperienceOfUnityCodeSystem;
    public float ghostDelayIsForCountingTheNumberOfGhostImageWhichPlayerWillGetTurnsIntoDelayPerSecondsToCountWithTimeDeltaTimeToShowPlayerWithFantasticExperienceOfUnityCodeSystem;
    public bool makeGhost = false;

    public GameObject thisGameObjectIsToCreateTheGhostOfPlayerUsingGhostPrefabInFlieFolderInPrefabsFolder;

    private void Start()
    {
        theNumberOfGhostImageWhichPlayerWillGetTurnsIntoDelayPerSecondsToCountWithTimeDeltaTimeToShowPlayerWithFantasticExperienceOfUnityCodeSystem = ghostDelayIsForCountingTheNumberOfGhostImageWhichPlayerWillGetTurnsIntoDelayPerSecondsToCountWithTimeDeltaTimeToShowPlayerWithFantasticExperienceOfUnityCodeSystem;
    }

    private void Update()
    {
        if (makeGhost)
        {
            if (theNumberOfGhostImageWhichPlayerWillGetTurnsIntoDelayPerSecondsToCountWithTimeDeltaTimeToShowPlayerWithFantasticExperienceOfUnityCodeSystem > 0)
            {
                theNumberOfGhostImageWhichPlayerWillGetTurnsIntoDelayPerSecondsToCountWithTimeDeltaTimeToShowPlayerWithFantasticExperienceOfUnityCodeSystem -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(thisGameObjectIsToCreateTheGhostOfPlayerUsingGhostPrefabInFlieFolderInPrefabsFolder, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;

                currentGhost.transform.localScale = this.transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;

                theNumberOfGhostImageWhichPlayerWillGetTurnsIntoDelayPerSecondsToCountWithTimeDeltaTimeToShowPlayerWithFantasticExperienceOfUnityCodeSystem = ghostDelayIsForCountingTheNumberOfGhostImageWhichPlayerWillGetTurnsIntoDelayPerSecondsToCountWithTimeDeltaTimeToShowPlayerWithFantasticExperienceOfUnityCodeSystem;

                Destroy(currentGhost, 1f);
            }
        }
    }
}
