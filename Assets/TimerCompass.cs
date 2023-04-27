using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerCompass : MonoBehaviour
{
    public GameObject targetObject;
    public Image compassImage;
    public float timerDuration = 30.0f;
    public Image timerFillImage;
    public Text timerProgressText;

    private RectTransform canvasRectTransform;
    private float timerRemaining;
    private bool timerRunning = false;

    private Color Compcolor;

    void Start()
    {
        compassImage.gameObject.SetActive(false);
        Compcolor = compassImage.color;

        canvasRectTransform = compassImage.canvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (timerRunning)
        {

            timerRemaining -= Time.deltaTime;

            if (timerRemaining <= 0.0f)
            {

                timerRunning = false;


                StartCoroutine(FadeOutCompass());
            }
            else
            {

                Vector3 targetPosition = targetObject.transform.position;


                Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, targetPosition);


                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, null, out Vector2 localPosition);


                Vector3 delta = new Vector3(localPosition.x, localPosition.y) - compassImage.rectTransform.localPosition;
                float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90.0f;


                compassImage.rectTransform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));


                timerFillImage.fillAmount = timerRemaining / timerDuration;


                timerProgressText.text = Mathf.FloorToInt(timerRemaining).ToString();
            }
        }
    }

    public void StartTimer()
    {
        if (targetObject == null)
        {
            targetObject = LevelManagerScav.instance.HintButton().gameObject;
        }
        compassImage.gameObject.SetActive(true);
        compassImage.color = Compcolor;



        timerRemaining = timerDuration;
        timerRunning = true;
        timerFillImage.fillAmount = 1.0f;
        timerProgressText.text = Mathf.FloorToInt(timerRemaining).ToString();
    }

    private IEnumerator FadeOutCompass()
    {

        float fadeDuration = 1.0f;
        float t = 0.0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, t / fadeDuration);
            compassImage.color = new Color(compassImage.color.r, compassImage.color.g, compassImage.color.b, alpha);
            yield return null;
        }


        compassImage.gameObject.SetActive(false);
    }

}
