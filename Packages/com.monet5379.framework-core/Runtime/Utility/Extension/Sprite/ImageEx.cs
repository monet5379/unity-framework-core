using System;
using UnityEngine;
using UnityEngine.UI;

namespace Monet5379
{
    public static class ImageEx
    {
        public static Vector3 GetAnchoredPosition3D(this Image image)
        {
            if (image != null)
            {
                return image.GetComponent<RectTransform>().anchoredPosition3D;
            }

            return Vector3.zero;
        }

        public static void SetAnchoredPosition3D(this Image image, Vector3 anchoredPosition3D)
        {
            if (image != null)
            {
                image.GetComponent<RectTransform>().anchoredPosition3D = anchoredPosition3D;
            }
        }

        public static void SetSprite(this Image image, Sprite sprite, bool useNativeSize = false)
        {
            if (sprite != null && image != null)
            {
                image.sprite = sprite;
                if (useNativeSize)
                {
                    image.SetNativeSize();
                }
            }
        }

        public static void ResetSprite(this Image image)
        {
            if (image != null)
            {
                image.sprite = null;
            }
        }

        public static void SetColor(this Image image, Color color)
        {
            if (image != null)
            {
                image.color = color;
            }
        }

        public static void SetAlpha(this Image image, float alpha)
        {
            if (image != null)
            {
                Color origin = image.color;

                image.color = new Color(origin.r, origin.g, origin.b, alpha);
            }
        }

        public static void SetFillAmount(this Image image, float fillAmount)
        {
            if (image != null)
            {
                image.fillAmount = Mathf.Clamp01(fillAmount);
            }
        }

        //

        public static void SetHitEffect(this Image image, bool enabled)
        {
            if (image != null)
            {
                if (enabled)
                {
                    image.material.EnableKeyword("HITEFFECT_ON");
                }
                else
                {
                    image.material.DisableKeyword("HITEFFECT_ON");
                }
            }
        }

        public static void SetHitEffectBlend(this Image image, float blend)
        {
            if (image != null)
            {
                image.material.SetFloat("_HitEffectBlend", blend);
            }
        }

        public static void SetHitEffectColor(this Image image, Color color, float blend)
        {
            if (image != null)
            {
                image.material.SetColor("_HitEffectColor", color);
                image.material.SetFloat("_HitEffectBlend", blend);
            }
        }

        //

        public static void SetGreyScale(this Image image, bool enabled)
        {
            if (image != null)
            {
                if (enabled)
                {
                    image.material.EnableKeyword("GREYSCALE_ON");
                }
                else
                {
                    image.material.DisableKeyword("GREYSCALE_ON");
                }
            }
        }
    }
}