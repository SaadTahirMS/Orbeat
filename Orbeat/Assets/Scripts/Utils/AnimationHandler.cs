using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationHandler {

	private static Sequence warningAnimSeq;
	private static Sequence selectAnimSeq;
	private static Sequence fadeSeq;
	private static Sequence popSeq;
	private static Sequence slideDownSeq;
	private static Sequence slideLeftSeq;
	private static Sequence slideRightSeq;
	private static Sequence toggleSeq;
	private static Sequence shakeSeq;
	private static Sequence punchSeq;
	private static Sequence yMoveSeq;
	private static Sequence rotationSeq;

	public static void Initialize()
	{
		warningAnimSeq = DOTween.Sequence ();
		selectAnimSeq = DOTween.Sequence ();
		fadeSeq = DOTween.Sequence ();
		popSeq = DOTween.Sequence ();
		slideDownSeq = DOTween.Sequence ();
		slideLeftSeq = DOTween.Sequence ();
		slideRightSeq = DOTween.Sequence ();
		toggleSeq = DOTween.Sequence ();
		shakeSeq = DOTween.Sequence ();
		punchSeq = DOTween.Sequence ();
		yMoveSeq = DOTween.Sequence ();
		rotationSeq = DOTween.Sequence ();
	}

	public static void PlayRotationAnim(Transform obj,Vector3 value,float duration = 0.1f)
	{
		rotationSeq.Kill ();
		rotationSeq = DOTween.Sequence ();
		rotationSeq.Append (obj.DORotate (value, duration)).SetEase (Ease.Linear).Play ();
	}

	public static void PlayRotationOutBackAnim(Transform obj,Vector3 value,float duration = 0.1f)
	{
		rotationSeq.Kill ();
		rotationSeq = DOTween.Sequence ();
		rotationSeq.Append (obj.DORotate (value, duration).SetEase (Ease.Linear).SetLoops(1,LoopType.Yoyo))
			.Append(obj.DORotate(Vector3.zero,duration)).Play ();
	}

	public static void PlayYMovementAnim(Transform obj,float value,float duration = 0.5f)
	{
		yMoveSeq.Kill ();
		yMoveSeq = DOTween.Sequence ();
		yMoveSeq.Append (obj.DOMoveY (value, duration)).SetEase (Ease.Linear).Play ();
	}

	public static void PlayRotationPunchAnim(Transform obj,Vector3 value,float duration = 0.5f)
	{
		punchSeq.Kill ();
		punchSeq = DOTween.Sequence ();
		punchSeq.Append (obj.DORotate (value, duration)).SetEase (Ease.InBack)
			.Append (obj.DORotate (Vector3.zero, 0.1f)).SetEase(Ease.InBack).Play ();
	}

	public static void PlayScalePunchAnim(Transform obj,float duration = 0.5f)
	{
		punchSeq.Kill ();
		punchSeq = DOTween.Sequence ();
		punchSeq.Append (obj.DOScale (Vector3.one, 0.1f))
			.Append (obj.DOPunchScale (Vector3.one * 0.1f, duration,3)).SetEase (Ease.Linear)
			.AppendInterval (0.5f).SetLoops (-1, LoopType.Yoyo).Play ();
	}

	public static void PlayShakeAnim(Transform obj,float duration = 0.7f)
	{
		shakeSeq.Kill ();
		shakeSeq = DOTween.Sequence ();
		shakeSeq
			.Append (obj.DORotate (Vector3.zero, 0.0001f))
			.Join (obj.DOScale (Vector3.one, 0.0001f))
			.Append (obj.DOPunchRotation (Vector3.one * 5, duration, 6)).SetEase (Ease.Linear)
			.Join (obj.DOPunchScale (Vector3.one * 0.1f, duration, 0)).SetEase (Ease.Linear)
			.Append (obj.DORotate (Vector3.zero, 0.0001f))
			.Join (obj.DOScale (Vector3.one, duration))
			.AppendInterval (1f)
			.SetLoops (-1).Play ();
	}

	public static void PlayXToggleAnimation(Transform obj, float moveValue,float duration = 0.1f,bool initialScreen = false)
	{
		if (toggleSeq.IsPlaying ()) {
			toggleSeq.Complete ();
			toggleSeq.Kill ();
		}
		toggleSeq = DOTween.Sequence ();
		if (initialScreen)
			duration = 0;
		toggleSeq.Append (obj.DOLocalMoveX (moveValue, duration)).SetEase (Ease.Linear).Play ();
	}

	public static void PlayWarningAnimation(Text obj, float movePosition = 200, float fadeDuration = 0.25f, float moveDuration = 0.75f)
	{
//		if (warningAnimSeq.IsPlaying ()) 
		{
			warningAnimSeq.Kill ();


		}
		warningAnimSeq = DOTween.Sequence ();

		obj.gameObject.SetActive (true);
		
		warningAnimSeq
			.Append(obj.DOFade (0, 0))
			.Join(obj.transform.DOLocalMove (Vector3.zero, 0.1f))
			.Append (obj.transform.DOLocalMoveY (movePosition, moveDuration).SetEase (Ease.OutBack))
			.Join (obj.DOFade (1, fadeDuration).SetEase (Ease.Linear))
			.AppendInterval(1)
//			.Append(obj.DOFade(0,0.1f))
			.Append(obj.DOFade(0,0.1f).OnComplete(delegate {
				obj.gameObject.SetActive (false);
			}))
			.Play ();
	}

	public static void PlaySelectAnimation(Transform obj, float sizeMultiplier = 0.1f, float duration = 0.3f)
	{
		if (selectAnimSeq.IsPlaying ()) {
			selectAnimSeq.Complete ();
			selectAnimSeq.Kill ();
		}
		obj.DOScale (1, 0);
		selectAnimSeq.Append (obj.DOPunchScale (Vector3.one * sizeMultiplier, duration,0,1).SetEase(Ease.Linear))
			.Play ();
	}

	public static void FadeIn(Image obj, TweenCallback callback, float duration = 0.3f)
	{
		if (fadeSeq.IsPlaying ()) {
			fadeSeq.Complete ();
			fadeSeq.Kill ();
		}

		obj.DOFade (0, 0);
		obj.gameObject.SetActive (true);

		fadeSeq.Append (obj.DOFade (1, duration).SetEase (Ease.Linear).OnComplete (callback))
			.Play ();
	}

	public static void FadeOut(Image obj, float duration = 0.3f)
	{
		if (fadeSeq.IsPlaying ()) {
			fadeSeq.Complete ();
			fadeSeq.Kill ();
		}

		obj.DOFade (1, 0);

		fadeSeq.Append (obj.DOFade (0, duration).SetEase (Ease.Linear).OnComplete (delegate {
			obj.gameObject.SetActive (false);
		}))
			.Play ();
	}

	public static void PopIn(Transform obj, float duration = 0.5f)
	{
		if (popSeq.IsPlaying ()) {
			popSeq.Complete ();
			popSeq.Kill ();
		}

		obj.DOScale (0, 0);

		popSeq.Append (obj.DOScale (1, duration).SetEase (Ease.OutBack))
			.Play ();
	}

	public static void PopOut(Transform obj, TweenCallback callback, float duration = 0.15f)
	{
		if (popSeq.IsPlaying ()) {
			popSeq.Complete ();
			popSeq.Kill ();
		}

		obj.DOScale (1, 0);

		popSeq.Append (obj.DOScale (0, duration).SetEase (Ease.Linear).OnComplete (callback))
			.Play ();
	}

	public static void SlideDown(Transform obj, float duration = 0.15f)
	{
		if (slideDownSeq.IsPlaying ()) {
			slideDownSeq.Complete ();
			slideDownSeq.Kill ();
		}

		obj.DOLocalMoveY (1000, 0);

		slideDownSeq.Append (obj.DOLocalMoveY (0, duration).SetEase (Ease.OutBack))
			.Play ();
	}

	public static void SlideBackUp(Transform obj, TweenCallback callback, float duration = 0.15f)
	{
		if (slideDownSeq.IsPlaying ()) {
			slideDownSeq.Complete ();
			slideDownSeq.Kill ();
		}

		obj.DOLocalMoveY (0, 0);

		slideDownSeq.Append (obj.DOLocalMoveY (1000, duration).SetEase (Ease.Linear).OnComplete (callback))
			.Play ();

	}

	public static void SlideInFromLeft(Transform obj, float duration = 0.15f)
	{
		if (slideLeftSeq.IsPlaying ()) {
			slideLeftSeq.Complete ();
			slideLeftSeq.Kill ();
		}

		obj.DOLocalMoveX (1000, 0);

		slideLeftSeq.Append (obj.DOLocalMoveX (0, duration).SetEase (Ease.Linear))
			.Play ();
	}

	public static void SlideOutToLeft(Transform obj, TweenCallback callback, float duration = 0.15f)
	{
		if (slideLeftSeq.IsPlaying ()) {
			slideLeftSeq.Complete ();
			slideLeftSeq.Kill ();
		}

		obj.DOLocalMoveX (0, 0);

		slideLeftSeq.Append (obj.DOLocalMoveX (1000, duration).SetEase (Ease.Linear).OnComplete (callback))
			.Play ();
	}

	public static void SlideInFromRight(Transform obj, float duration = 0.15f)
	{
		if (slideRightSeq.IsPlaying ()) {
			slideRightSeq.Complete ();
			slideRightSeq.Kill ();
		}

		obj.DOLocalMoveX (-1000, 0);

		slideRightSeq.Append (obj.DOLocalMoveX (0, duration).SetEase (Ease.Linear))
			.Play ();
	}

	public static void SlideOutToRight(Transform obj, TweenCallback callback, float duration = 0.15f)
	{
		if (slideRightSeq.IsPlaying ()) {
			slideRightSeq.Complete ();
			slideRightSeq.Kill ();
		}

		obj.DOLocalMoveX (0, 0);

		slideRightSeq.Append (obj.DOLocalMoveX (-1000, duration).SetEase (Ease.Linear).OnComplete (callback))
			.Play ();
	}
}
