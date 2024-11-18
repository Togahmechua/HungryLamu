using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl lamu = Cache.GetCharacter(other);
        if (lamu != null )
        {
            NewLevel();
        }    
    }

    private void NewLevel()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            UIManager.Ins.OpenUI<FadeInCanvas>().DeActive();
        });
        sequence.AppendInterval(1f);
        sequence.AppendCallback(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
        sequence.Play();
    }
}
