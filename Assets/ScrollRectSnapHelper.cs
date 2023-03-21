using UnityEngine;
using System.Collections;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollRectSnapHelper : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public float snapSpeed = 10f;
    public float snapThreshold = 0.1f;

    public RectTransform[] children;
    private int childIndex = -1;
    private bool isSnapping = false;
    private Vector2 targetPosition;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.25f);
        children = new RectTransform[content.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = content.GetChild(i).GetComponent<RectTransform>();
        }



        SnapToElement(UIManagerScav.instance.SV_IconList[1].GetComponent<RectTransform>());
    }

    void Update()
    {
        if (isSnapping)
        {
            float step = snapSpeed * Time.deltaTime;
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, step);
            if (Vector2.Distance(content.anchoredPosition, targetPosition) < snapThreshold)
            {
                content.anchoredPosition = targetPosition;
                isSnapping = false;
            }
        }
    }

    public void SnapToElement(RectTransform element)
    {

        Debug.Log(" element wasn't visible"+ children.Length);
        if (isSnapping) return;
        childIndex = -1;
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] == element)
            {
                childIndex = i;
                break;
            }
        }
        if (childIndex < 0) return;
        Debug.Log(" element wasn't visible");
        // Get the visible area of the viewport
        RectTransform viewport = scrollRect.viewport;
        Vector2 viewportTopLeft = (Vector2)viewport.position - (viewport.rect.size * 0.5f);
        Vector2 viewportBottomRight = (Vector2)viewport.position + (viewport.rect.size * 0.5f);

        // Get the visible area of the element in the content space
        Vector2 elementTopLeft = (Vector2)element.position - (element.rect.size * 0.5f);
        Vector2 elementBottomRight = (Vector2)element.position + (element.rect.size * 0.5f);
        elementTopLeft = content.InverseTransformPoint(elementTopLeft);
        elementBottomRight = content.InverseTransformPoint(elementBottomRight);

        // Check if the element is within the visible area of the viewport
        bool isElementVisible = (elementTopLeft.x <= viewportBottomRight.x && elementBottomRight.x >= viewportTopLeft.x);

        // Calculate the target position for the content to snap to
        float targetX = content.anchoredPosition.x;
        if (!isElementVisible)
        {


            Debug.Log(" element wasn't visible");
            if (elementBottomRight.x > viewportBottomRight.x)
            {
                targetX -= (elementBottomRight.x - viewportBottomRight.x);
            }
            else if (elementTopLeft.x < viewportTopLeft.x)
            {
                targetX += (viewportTopLeft.x - elementTopLeft.x);
            }
            targetPosition = new Vector2(targetX, content.anchoredPosition.y);
            isSnapping = true;
        }
    }
}
