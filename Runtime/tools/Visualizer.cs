/* Code by Ypmits */
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;


/**
 * Visualizer takes care of the transformations of a Transformm.
 * It's a generic helper-class that is used to 'Show/Hide' a transform.
 * It automatically takes care of the visibility of the the GameObject,
 * so when you use 'Show' it will automatically do 'gameObject.SetVisible(true)' at the start.
 * When you use 'Hide' it will automatically do 'gameObject.setVisible(false)' when it's done hiding.
 *
 * Visualizer's only dependency is DOTween's 'DG.Tweening' package
 */
public class  Visualizer : MonoBehaviour
{
	// MOVEMENT:
	public bool useMovement, use3D = true;
	public Vector3 showPosition2D, hidePosition2D, showPosition, hidePosition = new Vector3(0f, 0f, 0f);
	public float showPositionDuration = .25f;
	public float showPositionDelay, hidePositionDelay = 0f;
	public Ease showPositionEase = Ease.OutCubic;
	public UnityEvent showPositionCompleteAction, hidePositionCompleteAction = null;
	public float hidePositionDuration = .15f;
	public Ease hidePositionEase = Ease.InOutCubic;

	// ROTATION:
	public bool useRotation = false;
	public Vector3 showRotation, hideRotation = new Vector3(0f, 0f, 0f);
	public float showRotationDuration = .25f;
	public float showRotationDelay, hideRotationDelay = 0f;
	public Ease showRotationEase = Ease.OutCubic;
	public UnityEvent showRotationCompleteAction, hideRotationCompleteAction = null;
	public float hideRotationDuration = .15f;
	public Ease hideRotationEase = Ease.InOutCubic;

	// SCALE:
	public bool useScale = false;
	public Vector3 showScale = new Vector3(1f, 1f, 1f);
	public Vector3 hideScale = new Vector3(0f, 0f, 0f);
	public float showScaleDuration = 1f;
	public float showScaleDelay, hideScaleDelay = 0f;
	public Ease showScaleEase = Ease.OutCubic;
	public UnityEvent showScaleCompleteAction, hideScaleCompleteAction = null;
	public float hideScaleDuration = .35f;
	public Ease hideScaleEase = Ease.InOutCubic;

	// TRANSPARENCY:
	public bool useTransparency = false;
	public float showFadeAmount = 1f;
	public float hideFadeAmount = 0f;
	public float showTransparencyDuration = .25f;
	public float showTransparencyDelay = 0f;
	public Ease showTransparencyEase = Ease.OutCubic;
	public UnityEvent showTransparencyCompleteAction = null;
	public float hideTransparencyDuration = .15f;
	public float hideTransparencyDelay = 0f;
	public Ease hideTransparencyEase = Ease.InOutCubic;
	public UnityEvent hideTransparencyCompleteAction = null;

	public bool useCurrentAsShowAmount, autoHide, disableWhenHiding = true;
	public bool debugOutput, autoShow = false;
	public RectTransform rectTransform => transform as RectTransform;


	// [HideInInspector] public int selectedPositionType = 1;

	private bool _isHidden;
	[SerializeField] private Image _image;
	[SerializeField] private SpriteRenderer _spriteRenderer;


	#region Privates
	void Awake()
	{
		Check();
		if (useCurrentAsShowAmount) showPosition = transform.localPosition;
		if (autoHide) HideImmediately();
		_isHidden = autoHide;
		if (disableWhenHiding) gameObject.SetActive(false);
	}

	void Check()
	{
		_image = gameObject.GetComponent<Image>();
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		if (!_spriteRenderer && !_image)
		{
			Debug.LogWarning($"Please add a component of type 'SpriteRenderer' or 'Image' ({gameObject.name})");
			return;
		}
		if (use3D && _image)
		{
			use3D = false;
			Debug.LogWarning("You've set 'use3D' but there is only an 'Image' and no 'SpriteRenderer'. I'm setting it to 2D and use the 'Image-component'!");
		}
		else if (!use3D && _spriteRenderer)
		{
			use3D = true;
			Debug.LogWarning("You've set to not use 'use3D' but there is only an 'SpriteRenderer' and no 'Image'. I'm setting it to 3D and use the 'SpriteRenderer-component'!");
		}
	}

	void Start()
	{
		if (autoShow) Show();
	}
	#endregion


	void OnDrawGizmos()
	{
		// Draw Position:
		if (useMovement)
		{
			Vector3 hp = transform.TransformPoint(use3D ? hidePosition : hidePosition2D);
			Vector3 sp = transform.TransformPoint(use3D ? showPosition : showPosition2D);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(hp, sp);
		}
	}

	private UnityAction _showAction, _hideAction = null;
	public void Show(UnityAction _action = null, float delay = -1f, Ease ease = Ease.Unset)
	{
		_showAction = _action;
		_isHidden = false;
		if (debugOutput) Debug.Log("Show " + gameObject.name + " use3D:" + use3D);
		gameObject.SetActive(true);
		Ease easePos = (ease != Ease.Unset) ? ease : showPositionEase;
		float delayPos = (delay != -1f) ? delay : showPositionDelay;

		if (useMovement)
		{
			transform.DOKill();
			if (use3D)
			{
				transform.DOLocalMove(showPosition, showPositionDuration).SetDelay(delayPos).SetEase(easePos).OnStart(() => { }).OnComplete(() =>
				 {
					 if (showPositionCompleteAction != null) showPositionCompleteAction.Invoke();
					 if (_showAction != null) _showAction.Invoke();
				 });
			}
			else
			{
				rectTransform.DOAnchorPos(showPosition2D, showPositionDuration).SetDelay(delayPos).SetEase(easePos).OnStart(() => { }).OnComplete(() =>
				   {
					   if (showPositionCompleteAction != null) showPositionCompleteAction.Invoke();
					   if (_showAction != null) _showAction.Invoke();
				   });
			}
		}
		if (useScale)
		{
			Ease easeScale = (ease != Ease.Unset) ? ease : showScaleEase;
			float delayScale = (delay != -1f) ? delay : showScaleDelay;
			transform.DOKill();
			transform.DOScale(showScale, showScaleDuration).SetDelay(delayScale).SetEase(easeScale).OnStart(() => { }).OnComplete(() =>
			{
				if (showScaleCompleteAction != null) showPositionCompleteAction.Invoke();
				if (_showAction != null) _showAction.Invoke();
			});
		}
		if (useTransparency)
		{
			Ease easeTrans = (ease != Ease.Unset) ? ease : showTransparencyEase;
			float delayTrans = (delay != -1f) ? delay : showTransparencyDelay;
			if (use3D)
			{
				_spriteRenderer.DOKill();
				_spriteRenderer.DOFade(showFadeAmount, showTransparencyDuration).SetEase(easeTrans).SetDelay(delayTrans).OnComplete(() =>
				{
					if (showTransparencyCompleteAction != null) showTransparencyCompleteAction.Invoke();
					if (_showAction != null) _showAction.Invoke();
				});
			}
			else
			{
				_image.DOKill();
				_image.DOFade(showFadeAmount, showTransparencyDuration).SetEase(easeTrans).SetDelay(delayTrans).OnComplete(() =>
				{
					if (showTransparencyCompleteAction != null) showTransparencyCompleteAction.Invoke();
					if (_showAction != null) _showAction.Invoke();
				});
			}
		}
	}

	void CompleteShowActions()
	{

	}


	public void ShowImmediately()
	{
		_isHidden = false;
	}


	public void Hide(UnityAction _action = null, float delay = -1f, Ease ease = Ease.Unset)
	{
		_hideAction = _action;
		if (_isHidden) return;
		_isHidden = true;
		if (debugOutput) Debug.Log("Hide " + gameObject.name + " use3D:" + use3D);
		Ease easePos = (ease != Ease.Unset) ? ease : hidePositionEase;
		float delayPos = (delay != -1f) ? delay : hidePositionDelay;

		if (useMovement)
		{
			if (use3D)
			{
				bool hasRectTransform = (GetComponent<RectTransform>() != null);
				transform.DOKill();
				if (hasRectTransform)
				{
					rectTransform.DOAnchorPos(hidePosition2D, hidePositionDuration).SetDelay(delayPos).SetEase(easePos).OnComplete(() =>
					{
						if (hidePositionCompleteAction != null) hidePositionCompleteAction.Invoke();
						if (_hideAction != null) _hideAction.Invoke();
						if (disableWhenHiding) gameObject.SetActive(false);
					});
				}
				else
				{
					transform.DOLocalMove(hidePosition, hidePositionDuration).SetDelay(delayPos).SetEase(easePos).OnComplete(() =>
					{
						if (hidePositionCompleteAction != null) hidePositionCompleteAction.Invoke();
						if (_hideAction != null) _hideAction.Invoke();
						if (disableWhenHiding) gameObject.SetActive(false);
					});
				}
			}
			else
			{
				rectTransform.DOAnchorPos(hidePosition2D, hidePositionDuration).SetDelay(delayPos).SetEase(easePos).OnComplete(() =>
				  {
					  if (hidePositionCompleteAction != null) hidePositionCompleteAction.Invoke();
					  if (_hideAction != null) _hideAction.Invoke();
					  if (disableWhenHiding) gameObject.SetActive(false);
				  });
			}
		}
		if (useScale)
		{
			Ease easeScale = (ease != Ease.Unset) ? ease : hideScaleEase;
			float delayScale = (delay != -1f) ? delay : hideScaleDelay;
			transform.DOKill();
			transform.DOScale(hideScale, hideScaleDuration).SetDelay(delayScale).SetEase(easeScale).OnStart(() => { }).OnComplete(() =>
			{
				if (hideScaleCompleteAction != null) hidePositionCompleteAction.Invoke();
				if (_hideAction != null) _hideAction.Invoke();
				if (disableWhenHiding) gameObject.SetActive(false);
			});
		}
		if (useTransparency)
		{
			Ease easeTrans = (ease != Ease.Unset) ? ease : hideTransparencyEase;
			float delayTrans = (delay != -1f) ? delay : hideTransparencyDelay;
			if (use3D)
			{
				GetComponent<SpriteRenderer>().DOKill();
				GetComponent<SpriteRenderer>().DOFade(hideFadeAmount, hideTransparencyDuration).SetEase(easeTrans).SetDelay(delayTrans).OnComplete(() =>
				{
					if (hideTransparencyCompleteAction != null) hideTransparencyCompleteAction.Invoke();
					if (disableWhenHiding) gameObject.SetActive(false);
				});
			}
			else
			{
				GetComponent<Image>().DOKill();
				GetComponent<Image>().DOFade(hideFadeAmount, hideTransparencyDuration).SetEase(easeTrans).SetDelay(delayTrans).OnComplete(() =>
				{
					if (hideTransparencyCompleteAction != null) hideTransparencyCompleteAction.Invoke();
					if (disableWhenHiding) gameObject.SetActive(false);
				});
			}
		}
	}


	public void HideImmediately()
	{
		_isHidden = true;
		if (useMovement)
		{
			if (use3D)
				transform.localPosition = hidePosition;
			else
				rectTransform.anchoredPosition = hidePosition2D;
		}
		if (useScale)
			transform.localScale = hideScale;
		if (useTransparency)
		{
			if (use3D)
			{
				Color c = _spriteRenderer.color;
				c.a = hideFadeAmount;
				_spriteRenderer.color = c;
			}
			else
			{
				Color c = _image.color;
				c.a = hideFadeAmount;
				_image.color = c;
			}
		}
	}
}