using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [Header("Director")]
    [SerializeField] private PlayableDirector director;
    [SerializeField] private BoxCollider2D box;

    private LamuCtrl lamu;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null)
        {
            lamu = player;
            StartCoroutine(PlayCutscene());
            box.enabled = false;
        }
    }

    private IEnumerator PlayCutscene()
    {
        UIManager.Ins.dialogueCanvas.inCutSence = true;
        GameManager.Ins.movingCam.FocusOnFruitFriends();
        yield return new WaitForSeconds(1f);
        director.Play();
        yield return new WaitUntil(() => director.state != PlayState.Playing);
        director.gameObject.SetActive(value: false);
        yield return new WaitForSeconds(1f);
        GameManager.Ins.movingCam.FocusOnLamu();
        GameManager.Ins.eDialogueType = EDialogueType.Timeline;
        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
    }
}
