using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class buttonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public TMP_Text text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.white;
        button.image.color = Color.black;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.black;
        button.image.color = Color.white;
    }
}
