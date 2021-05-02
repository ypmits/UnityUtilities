/**
 * Visualizer
 * © 2021 - Dave Lenz / Ypmits
 * ypmits.nl
 * 
 * Description:
 * ---
 * Visualizer takes care of the transformations of a Transformm.
 * It's a generic helper-class that is used to 'Show/Hide' a transform.
 * It automatically takes care of the visibility of the the GameObject,
 * so when you use 'Show' it will automatically do 'gameObject.SetVisible(true)' at the start.
 * When you use 'Hide' it will automatically do 'gameObject.setVisible(false)' when it's done hiding.
 *
 * Dependency:
 * ---
 * DOTween (DG.Tweening)
 */

using UnityEngine;
using UnityEngine.Events;
// using DG.Tweening;

namespace nl.ypmits.gametools.tools
{
    public class Visualizer : MonoBehaviour
    {
    //     // POSITION:
    //     public bool usePositionUI = false;
    //     public Vector3 showPosition2D = new Vector3(0f,0f,0f);
    //     public Vector3 hidePosition2D = new Vector3(0f,0f,0f);
    //     public bool usePosition = false;
    //     public Vector3 showPosition = new Vector3(0f,0f,0f);
    //     public Vector3 hidePosition = new Vector3(0f,0f,0f);
    //     public float showPositionDuration = .25f;
    //     public float showPositionDelay = 0f;
    //     public Ease showPositionEase = Ease.OutCubic;
    //     public UnityEvent showPositionCompleteAction = null;
    //     public float hidePositionDuration = .15f;
    //     public float hidePositionDelay = 0f;
    //     public Ease hidePositionEase = Ease.InOutCubic;
    //     public UnityEvent hidePositionCompleteAction = null;

    //     // ROTATION:
    //     public bool useRotation = false;
    //     public Vector3 showRotation = new Vector3(0f,0f,0f);
    //     public Vector3 hideRotation = new Vector3(0f,0f,0f);
    //     public float showRotationDuration = .25f;
    //     public float showRotationDelay = 0f;
    //     public Ease showRotationEase = Ease.OutCubic;
    //     public UnityEvent showRotationCompleteAction = null;
    //     public float hideRotationDuration = .15f;
    //     public float hideRotationDelay = 0f;
    //     public Ease hideRotationEase = Ease.InOutCubic;
    //     public UnityEvent hideRotationCompleteAction = null;

    //     // SCALE:
    //     public bool useScale = false;
    //     public Vector3 showScale = new Vector3(1f,1f,1f);
    //     public Vector3 hideScale = new Vector3(0f,0f,0f);
    //     public float showScaleDuration = 1f;
    //     public float showScaleDelay = 0f;
    //     public Ease showScaleEase = Ease.OutCubic;
    //     public UnityEvent showScaleCompleteAction = null;
    //     public float hideScaleDuration = .35f;
    //     public float hideScaleDelay = 0f;
    //     public Ease hideScaleEase = Ease.InOutCubic;
    //     public UnityEvent hideScaleCompleteAction = null;
        
    //     // TRANSPARENCY:
    //     public bool useTransparency = false;
    //     public float showFadeAmount = 1f;
    //     public float hideFadeAmount = 0f;
    //     public float showTransparencyDuration = .25f;
    //     public float showTransparencyDelay = 0f;
    //     public Ease showTransparencyEase = Ease.OutCubic;
    //     public UnityEvent showTransparencyCompleteAction = null;
    //     public float hideTransparencyDuration = .15f;
    //     public float hideTransparencyDelay = 0f;
    //     public Ease hideTransparencyEase = Ease.InOutCubic;
    //     public UnityEvent hideTransparencyCompleteAction = null;

    //     public bool useCurrentAsShowAmount = true;
    //     /// <summary>
    //     /// Do you want the 'Show-function' to be called in Start?
    //     /// </summary>
    //     public bool autoShow;	
    //     /// <summary>
    //     /// Do you want to init the object as 'hidden'
    //     /// </summary>
    //     public bool autoHide = true;
    //     /// <summary>
    //     /// Does a .SetActive(true/false) on the image.gameObject when hiding and showing
    //     /// </summary>
    //     public bool disableWhenHiding = true;
    //     public bool debugOutput = false;
    //     public RectTransform rectTransform => transform as RectTransform;
        

    //     [HideInInspector] public int selectedPositionType = 1;


    // #region Privates
    //     void Awake()
    //     {
    //         if (useCurrentAsShowAmount ) showPosition = transform.localPosition;
    //         if (autoHide) HideImmediately();
    //         if (disableWhenHiding) gameObject.SetActive(false);
    //     }

    //     void Start()
    //     {
    //         if (autoShow) Show();		
    //     }
    // #endregion


    //     // /// <summary>
    //     // /// Callback to draw gizmos that are pickable and always drawn.
    //     // /// </summary>
    //     // void OnDrawGizmos()
    //     // {
    //     // 	// Draw Position:
    //     // 	bool hasRectTransform = GetComponent<RectTransform>() != null;
    //     // 	Gizmos.color = Color.green;
    //     // 	Vector3 size = Vector3.one*10;
    //     // 	if (!hasRectTransform)
    //     // 	{
    //     // 		// 3D:
    //     // 		Vector3 hp = transform.TransformPoint(hidePosition);
    //     // 		Vector3 sp = transform.TransformPoint(showPosition);
    //     // 		Gizmos.color = Color.green;
    //     // 		Gizmos.DrawWireCube(hp, size);
    //     // 		Gizmos.color = Color.red;
    //     // 		Gizmos.DrawWireCube(sp, size);
    //     // 		Gizmos.color = Color.blue;
    //     // 		Gizmos.DrawLine(hp, sp);
    //     // 	} else
    //     // 	{
    //     // 		// 2D:
    //     // 		Vector3 hp2_a = Camera.main.WorldToViewportPoint(transform.TransformPoint(hidePosition2D));// hidePosition2D
    //     // 		// Vector3 hp2 = transform.TransformPoint(hidePosition2D);
    //     // 		Vector3 sp2 = transform.TransformPoint(showPosition2D);
    //     // 		Gizmos.color = Color.green;
    //     // 		Gizmos.DrawWireCube(hp2_a, size);
    //     // 		Gizmos.color = Color.red;
    //     // 		Gizmos.DrawWireCube(sp2, size);
    //     // 		Gizmos.color = Color.blue;
    //     // 		Gizmos.DrawLine(hp2_a, sp2);
    //     // 	}
    //     // }


    //     public void Show(UnityAction action = null, float delay = -1f, Ease ease = Ease.Unset)
    //     {
    //         if( debugOutput ) Debug.Log("Show "+gameObject.name+" usePosition:"+usePosition);		
    //         gameObject.SetActive(true);
    //         if (usePosition)
    //         {
    //             Ease easePos = (ease != Ease.Unset) ? ease : showPositionEase;
    //             float delayPos = (delay != -1f) ? delay : showPositionDelay;
    //             bool hasRectTransform = (GetComponent<RectTransform>() != null);
    //             transform.DOKill();
    //             if(hasRectTransform)
    //             {
    //                 ((RectTransform)transform).DOAnchorPos(showPosition2D, showPositionDuration).SetDelay(delayPos).SetEase(easePos).OnStart( ()=>{} ).OnComplete(() =>
    //                 {
    //                     if (showPositionCompleteAction != null) showPositionCompleteAction.Invoke();
    //                     if (action != null) action.Invoke();
    //                 });
    //             } else {
    //                 transform.DOLocalMove(showPosition, showPositionDuration).SetDelay(delayPos).SetEase(easePos).OnStart( ()=>{} ).OnComplete(() =>
    //                 {
    //                     if (showPositionCompleteAction != null) showPositionCompleteAction.Invoke();
    //                     if (action != null) action.Invoke();
    //                 });
    //             }
    //         }
    //         if (usePositionUI)
    //         {
    //             Debug.Log("[Visualizer.Show() for UI] Not implemented yet!");
    //         }
    //         if (useScale)
    //         {
    //             Ease easeScale = (ease != Ease.Unset) ? ease : showScaleEase;
    //             float delayScale = (delay != -1f) ? delay : showScaleDelay;
    //             transform.DOKill();
    //             transform.DOScale(showScale, showScaleDuration).SetDelay(delayScale).SetEase(easeScale).OnStart( ()=>{} ).OnComplete(() =>{
    //                 if (showScaleCompleteAction != null) showPositionCompleteAction.Invoke();
    //                 if (action != null) action.Invoke();
    //             });
    //         }
    //         if (useTransparency)
    //         {
    //             Ease easeTrans = (ease != Ease.Unset) ? ease : showTransparencyEase;
    //             float delayTrans = (delay != -1f) ? delay : showTransparencyDelay;
    //             GetComponent<SpriteRenderer>().DOKill();
    //             GetComponent<SpriteRenderer>().DOFade(showFadeAmount, showTransparencyDuration).SetEase(easeTrans).SetDelay(delayTrans).OnComplete(() =>
    //             {
    //                 if (showTransparencyCompleteAction != null) showTransparencyCompleteAction.Invoke();
    //                 if (action != null) action.Invoke();
    //             });
    //         }
    //     }


    //     public void ShowImmediately()
    //     {
    //     }


    //     public void Hide(UnityAction action = null, float delay = -1f, Ease ease = Ease.Unset)
    //     {
    //         if( debugOutput ) Debug.Log("Hide "+gameObject.name+" usePosition:"+usePosition);
    //         if (usePosition)
    //         {
    //             Ease easePos = (ease != Ease.Unset) ? ease : hidePositionEase;
    //             float delayPos = (delay != -1f) ? delay : hidePositionDelay;
    //             bool hasRectTransform = (GetComponent<RectTransform>() != null);
    //             transform.DOKill();
    //             if(hasRectTransform)
    //             {
    //                 ((RectTransform)transform).DOAnchorPos(hidePosition2D, hidePositionDuration).SetDelay(delayPos).SetEase(easePos).OnComplete(() =>
    //                 {
    //                     if (hidePositionCompleteAction != null) hidePositionCompleteAction.Invoke();
    //                     if (action != null) action.Invoke();
    //                     if (disableWhenHiding) gameObject.SetActive(false);
    //                 });
    //             } else {
    //                 transform.DOLocalMove(hidePosition, hidePositionDuration).SetDelay(delayPos).SetEase(easePos).OnComplete(() =>
    //                 {
    //                     if (hidePositionCompleteAction != null) hidePositionCompleteAction.Invoke();
    //                     if (action != null) action.Invoke();
    //                     if (disableWhenHiding) gameObject.SetActive(false);
    //                 });
    //             }
    //         }
    //         if (usePositionUI) { Debug.Log("[Visualizer.Hide() for UI] Not implemented yet!"); }
    //         if (useScale)
    //         {
    //             Ease easeScale = (ease != Ease.Unset) ? ease : hideScaleEase;
    //             float delayScale = (delay != -1f) ? delay : hideScaleDelay;
    //             transform.DOKill();
    //             transform.DOScale(hideScale, hideScaleDuration).SetDelay(delayScale).SetEase(easeScale).OnStart( ()=>{} ).OnComplete(() =>{
    //                 if (hideScaleCompleteAction != null) hidePositionCompleteAction.Invoke();
    //                 if (action != null) action.Invoke();
    //             });
    //         }
    //         if (useTransparency)
    //         {
    //             Ease easeTrans = (ease != Ease.Unset) ? ease : hideTransparencyEase;
    //             float delayTrans = (delay != -1f) ? delay : hideTransparencyDelay;
    //             GetComponent<SpriteRenderer>().DOKill();
    //             GetComponent<SpriteRenderer>().DOFade(hideFadeAmount, hideTransparencyDuration).SetEase(easeTrans).SetDelay(delayTrans).OnComplete(() =>
    //             {
    //                 if (hideTransparencyCompleteAction != null) hideTransparencyCompleteAction.Invoke();
    //                 if (disableWhenHiding) gameObject.SetActive(false);
    //             });
    //         }
    //     }


    //     public void HideImmediately()
    //     {
    //         if (usePosition)
    //             transform.localPosition = hidePosition;
    //         if (usePositionUI)
    //         {

    //         }
    //         if (useScale)
    //             transform.localScale = hideScale;
    //         if (useTransparency)
    //         {
    //             Color c = GetComponent<SpriteRenderer>().color;
    //             c.a = hideFadeAmount;
    //             GetComponent<SpriteRenderer>().color = c;
    //         }
    //         // if (disableWhenHiding) gameObject.SetActive(false);
    //     }
    }
}