using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void OnEnable()
    {
        transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.1f);
    }
}
