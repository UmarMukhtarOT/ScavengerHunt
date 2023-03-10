using UnityEngine;
using System.Collections.Generic;
using TinyK.Common;

namespace TinyK.Touch
{
	/// <summary>This component works like LeanFingerSwipe, but you must manually add fingers from components like LeanFingerDown, LeanFingerDownCanvas, etc.</summary>
	[HelpURL(LeanTouch.HelpUrlPrefix + "LeanManualSwipe")]
	[AddComponentMenu(LeanTouch.ComponentPathPrefix + "Manual Swipe")]
	public class LeanManualSwipe : LeanSwipeBase
	{
		/// <summary>Ignore fingers with OverGui?</summary>
		public bool IgnoreIsOverGui { set { ignoreIsOverGui = value; } get { return ignoreIsOverGui; } } [SerializeField] private bool ignoreIsOverGui;

		/// <summary>If the specified object is set and isn't selected, then this component will do nothing.</summary>
		public LeanSelectable RequiredSelectable { set { requiredSelectable = value; } get { return requiredSelectable; } } [SerializeField] private LeanSelectable requiredSelectable;

		[System.NonSerialized]
		private List<LeanFinger> fingers;

		/// <summary>This method allows you to manually add a finger.</summary>
		public void AddFinger(LeanFinger finger)
		{
			if (fingers == null)
			{
				fingers = new List<LeanFinger>();
			}

			for (var i = fingers.Count - 1; i >= 0; i--)
			{
				if (fingers[i] == finger)
				{
					return;
				}
			}

			fingers.Add(finger);
		}

		/// <summary>This method allows you to manually remove a finger.</summary>
		public void RemoveFinger(LeanFinger finger)
		{
			fingers.Remove(finger);
		}

#if UNITY_EDITOR
		protected virtual void Reset()
		{
			requiredSelectable = GetComponentInParent<LeanSelectable>();
		}
#endif
		protected virtual void Start()
		{
			if (requiredSelectable == null)
			{
				requiredSelectable = GetComponentInParent<LeanSelectable>();
			}
		}

		protected virtual void OnEnable()
		{
			LeanTouch.OnFingerSwipe += HandleFingerSwipe;
		}

		protected virtual void OnDisable()
		{
			LeanTouch.OnFingerSwipe -= HandleFingerSwipe;
		}

		private void HandleFingerSwipe(LeanFinger finger)
		{
			// Make sure this finger was manually added
			if (fingers != null && fingers.Remove(finger) == true)
			{
				if (ignoreIsOverGui == true && finger.IsOverGui == true)
				{
					return;
				}

				if (requiredSelectable != null && requiredSelectable.IsSelected == false)
				{
					return;
				}

				HandleFingerSwipe(finger, finger.StartScreenPosition, finger.ScreenPosition);
			}
		}
	}
}

#if UNITY_EDITOR
namespace TinyK.Touch.Editor
{
	using UnityEditor;
	using TARGET = LeanManualSwipe;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(TARGET))]
	public class LeanManualSwipe_Editor : LeanSwipeBase_Editor
	{
		protected override void OnInspector()
		{
			TARGET tgt; TARGET[] tgts; GetTargets(out tgt, out tgts);

			Draw("ignoreIsOverGui", "Ignore fingers with OverGui?");
			Draw("requiredSelectable", "If the specified object is set and isn't selected, then this component will do nothing.");

			base.OnInspector();
		}
	}
}
#endif