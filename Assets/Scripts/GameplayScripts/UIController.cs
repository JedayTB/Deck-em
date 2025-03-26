using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Health Cards Info")]
    [SerializeField] private Image[] HealthCards;
    [SerializeField] private RectTransform[] StartTransforms;
    [SerializeField] private RectTransform[] EndTransforms;
    [SerializeField] private float LerpTime = 0.35f;

    public int cardIndex = 0;
    void Start()
    {
        StartCoroutine(GainedHealth());
    }

    public void plGainedHealth()
    {
        StartCoroutine(GainedHealth());
    }

    public void plLostHealth()
    {
        StartCoroutine(LostHealth());
    }

    IEnumerator GainedHealth()
    {
        float count = 0f;
        float progress = 0f;
        Vector3 startpos = Vector3.zero;
        Vector3 endpos = Vector3.zero;

        Quaternion startRot = Quaternion.identity;
        Quaternion endrot = Quaternion.identity;

        startpos = StartTransforms[cardIndex].position;
        endpos = EndTransforms[cardIndex].position;

        startRot = StartTransforms[cardIndex].rotation;
        endrot = EndTransforms[cardIndex].rotation;

        while (count < LerpTime)
        {
            count += Time.deltaTime;
            progress = count / LerpTime;
            progress = LerpAndEasings.easeOutQuart(progress);

            HealthCards[cardIndex].transform.SetPositionAndRotation(Vector3.Lerp(startpos, endpos, progress),
             Quaternion.Slerp(startRot, endrot, progress));
            yield return null;
        }

        if (cardIndex != GameStateManager.Instance.player.health)
        {
            cardIndex++;
            cardIndex = Mathf.Clamp(cardIndex, 0, 2);
            plGainedHealth();
        }
    }
    IEnumerator LostHealth()
    {
        float count = 0f;
        float progress = 0f;
        Vector3 startpos = Vector3.zero;
        Vector3 endpos = Vector3.zero;

        Quaternion startRot = Quaternion.identity;
        Quaternion endrot = Quaternion.identity;


        startpos = EndTransforms[cardIndex].position;
        endpos = StartTransforms[cardIndex].position;

        startRot = EndTransforms[cardIndex].rotation;
        endrot = StartTransforms[cardIndex].rotation;


        while (count < LerpTime)
        {
            count += Time.deltaTime;
            progress = count / LerpTime;
            progress = LerpAndEasings.easeOutQuart(progress);

            HealthCards[cardIndex].transform.SetPositionAndRotation(Vector3.Lerp(startpos, endpos, progress),
             Quaternion.Slerp(startRot, endrot, progress));
            yield return null;
        }

        if (cardIndex != GameStateManager.Instance.player.health)
        {
            cardIndex--;
            cardIndex = Mathf.Clamp(cardIndex, 0, 2);
            plLostHealth();
        }
    }

}
