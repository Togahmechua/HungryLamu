using UnityEngine;

[CreateAssetMenu(menuName = "Action/Eat Fruit")]
public class EatFruit : ActionSO
{
    [SerializeField] private GameObject Vfx_EatApple;

    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);
        InteractBehaviour par = parent.GetComponent<InteractBehaviour>();
        LamuCtrl lamu = par.lamu;
        par.End();
        lamu.PickUp(EItemType.HiddenRock);
        Instantiate(Vfx_EatApple, parent.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        SoundFXManager.Ins.PlaySFX("lamu-eat");
        par.anim.SetTrigger(CacheString.TAG_INTERACT);
        UIManager.Ins.objectiveCanvas.EatFruitFriend();
    }
}
