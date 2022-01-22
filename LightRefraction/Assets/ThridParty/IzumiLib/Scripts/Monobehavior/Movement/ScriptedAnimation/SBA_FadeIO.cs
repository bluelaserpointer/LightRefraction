using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(CanvasGroup))]
public class SBA_FadeIO : MonoBehaviour
{
    public CanvasGroup CanvasGroup { get; private set; }
    public float alphaChangePerSec = 1;
    [SerializeField]
    float targetAlpha;

    bool _reached;
    UnityAction reachAction;
    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }
    private void FixedUpdate()
    {
        if(CanvasGroup.alpha != targetAlpha)
        {
            CanvasGroup.alpha = Mathf.MoveTowards(CanvasGroup.alpha, targetAlpha, alphaChangePerSec * Time.fixedDeltaTime);
        }
        else if(!_reached)
        {
            _reached = true;
            if(reachAction != null)
            {
                reachAction.Invoke();
                reachAction = null;
            }
        }
    }
    public void FadeIn(UnityAction reachAction = null)
    {
        _reached = false;
        this.reachAction = reachAction;
        gameObject.SetActive(true);
        CanvasGroup.interactable = true;
        targetAlpha = 1;
    }
    public void FadeOut(UnityAction reachAction = null)
    {
        _reached = false;
        this.reachAction = reachAction;
        CanvasGroup.interactable = false;
        targetAlpha = 0;
    }
}
