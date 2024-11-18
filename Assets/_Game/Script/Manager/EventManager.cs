using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager ins;
    public static EventManager Ins => ins;

    [SerializeField] private GameObject cherryBlockade;
    [SerializeField] private GameObject bananaBlockade;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject orange;
    [SerializeField] private GameObject pear;

    private void Awake()
    {
        EventManager.ins = this;
    }

    public void DeActiveCheeryBlockade()
    {
        cherryBlockade.SetActive(false);
    }

    public void DeActiveBananaBlockade()
    {
        bananaBlockade.SetActive(false);
    }

    public void ActiveItem()
    {
        item.SetActive(true);
    }

    public void ActiveFruit(int index)
    {
        if (index == 0)
        {
            orange.SetActive(true);
        }
        else if (index == 1)
        {
            pear.SetActive(true);
        }
    }

    public void NextObjective()
    {
        UIManager.Ins.OpenUI<ObjectiveCanvas>();
    }

    public void CloseObjective()
    {
        UIManager.Ins.CloseUI<ObjectiveCanvas>();
    }

}
