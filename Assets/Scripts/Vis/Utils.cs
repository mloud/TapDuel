using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Vis
{
	public static class Utils
	{

		public static void Makefullscreen(Transform tr)
		{
			(tr as RectTransform).localPosition = Vector3.zero;
			(tr as RectTransform).localScale = Vector3.one;
			(tr as RectTransform).anchorMax = Vector3.one;
			(tr as RectTransform).anchorMin = Vector3.zero;
			(tr as RectTransform).offsetMin = Vector3.zero;
			(tr as RectTransform).offsetMax = Vector3.zero;
		}

		public static void SetAlpha(float alpha, GameObject go)
		{
			var sprRen = go.GetComponentsInChildren<SpriteRenderer>();
			var textsUi = go.GetComponentsInChildren<TextMeshProUGUI>();
			var texts = go.GetComponentsInChildren<TextMeshPro>();
			var imgs = go.GetComponentsInChildren<Image>();

			for (int i = 0; i < sprRen.Length; ++i) {
				var color = sprRen[i].color;
				color.a = alpha;
				sprRen[i].color = color;
			}

			for (int i = 0; i < textsUi.Length; ++i) {
				var color = textsUi[i].color;
				color.a = alpha;
				textsUi[i].color = color;
			}
		

			for (int i = 0; i < texts.Length; ++i) {
				var color = texts[i].color;
				color.a = alpha;
				texts[i].color = color;
			}

			for (int i = 0; i < imgs.Length; ++i) {
				var color = imgs[i].color;
				color.a = alpha;
				imgs[i].color = color;
			}
		}
	}
}

