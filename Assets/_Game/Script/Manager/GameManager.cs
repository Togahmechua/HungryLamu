using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    //private static GameState gameState = GameState.MainMenu;

    // Start is called before the first frame update
    public bool isActive;
    public MovingCam movingCam;
    public PlayerController playerController;
    public EDialogueType eDialogueType;
    [HideInInspector] public Transform roamArea;

    protected void Awake()
    {
        DOTween.SetTweensCapacity(500, 50);
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //csv.OnInit();
        //userData?.OnInitData();

        //ChangeState(GameState.MainMenu);
        if (isActive)
        {
            switch (eDialogueType)
            {
                case EDialogueType.Cave:
                    UIManager.Ins.OpenUI<StartSceneDialogueCanvas>();
                    break;
                case EDialogueType.Park:
                    UIManager.Ins.OpenUI<FadeOutCanvas>();
                    break;
                case EDialogueType.ThreeDLamuCave:
                    UIManager.Ins.OpenUI<FadeOutCanvas>();
                    break;
                case EDialogueType.KillingRoad:
                    UIManager.Ins.OpenUI<FadeOutCanvas>();
                    break;
                case EDialogueType.LamuPark3D:
                    UIManager.Ins.OpenUI<FadeOutCanvas>();
                    break;
            }
            
        }
    }

    //public static void ChangeState(GameState state)
    //{
    //    gameState = state;
    //}

    //public static bool IsState(GameState state)
    //{
    //    return gameState == state;
    //}
}