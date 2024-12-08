using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Text text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Chia giá trị RGB và Alpha cho 255f để chuẩn hóa
        text.color = new Color(255 / 255f, 202 / 255f, 0 / 255f, 60 / 255f);
        SoundFXManager.Ins.PlaySFX(audioClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Chia giá trị RGB và Alpha cho 255f để chuẩn hóa
        text.color = new Color(255 / 255f, 202 / 255f, 0 / 255f, 196 / 255f);
    }

}
