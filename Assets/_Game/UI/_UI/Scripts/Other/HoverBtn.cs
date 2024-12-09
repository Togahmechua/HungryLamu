using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{ 
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Text text;

    [SerializeField] private float[] pre;
    [SerializeField] private float[] after;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Chia giá trị RGB và Alpha cho 255f để chuẩn hóa
        text.color = new Color(pre[0] / 255f, pre[1] / 255f, pre[2] / 255f, pre[3] / 255f);
        SoundFXManager.Ins.PlaySFX(audioClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Chia giá trị RGB và Alpha cho 255f để chuẩn hóa
        text.color = new Color(after[0] / 255f, after[1] / 255f, after[2] / 255f, after[3] / 255f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        text.color = new Color(after[0] / 255f, after[1] / 255f, after[2] / 255f, after[3] / 255f);
    }
}
