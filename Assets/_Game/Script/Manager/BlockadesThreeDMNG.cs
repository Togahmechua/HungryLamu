using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockadesThreeDMNG : MonoBehaviour
{
    private static BlockadesThreeDMNG ins;
    public static BlockadesThreeDMNG Ins => ins;

    [SerializeField] private GameObject dogBlockade;
    [SerializeField] private GameObject campSiteBlockade;
    [SerializeField] private GameObject friendBlockade;
    [SerializeField] private GameObject forestBlockade;
    [SerializeField] private GameObject caveBlockade;

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
    }

    public void DisableDogBlockade()
    {
        dogBlockade.SetActive(false);
    }

    public void DisableCampSiteBlockade()
    {
        campSiteBlockade.SetActive(false);
    }

    public void DisableFriendBlockade()
    {
        friendBlockade.SetActive(false);
    }

    public void DisableForestBlockade()
    {
        forestBlockade.SetActive(false);
    }

    public void DisableCaveBlockade()
    {
        caveBlockade.SetActive(false);
    }

    public void EnableDogBlockade()
    {
        dogBlockade.SetActive(true);
    }

    public void EnableCampSiteBlockade()
    {
        campSiteBlockade.SetActive(true);
    }

    public void EnableFriendBlockade()
    {
        friendBlockade.SetActive(true);
    }

    public void EnableForestBlockade()
    {
        forestBlockade.SetActive(true);
    }

    public void EnableCaveBlockade()
    {
        caveBlockade.SetActive(true);
    }
}
