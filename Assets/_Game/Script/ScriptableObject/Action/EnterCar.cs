using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Action/Enter Car")]
public class EnterCar : ActionSO
{
    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);

        parent.GetComponent<MonoBehaviour>().StartCoroutine(Wait(parent));
    }

    private IEnumerator Wait(GameObject parent)
    {
        UIManager.Ins.OpenUI<FadeInCanvas>();
        SoundFXManager.Ins.PlaySFX("car-enter");

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(4);
    }
}
