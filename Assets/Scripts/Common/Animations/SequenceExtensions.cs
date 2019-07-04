using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Common.Animations
{
    public static class SequenceExtensions
    {
        public static void Init(this Sequence sequence, SequenceData data)
        {
            if (data.config != null)
                Init(sequence, data.config);

            foreach (var step in data.steps)
            {
                sequence.Join(step);
            }
        }

        public static void Init(this Sequence sequence, SequenceConfig config)
        {
            sequence.SetLoops(config.loops);
        }

        public static void Join(this Sequence sequence, SequenceStepData data)
        {
            foreach (var stepConfig in data.config.steps)
            {
                var step = GetStep(data.item, stepConfig);
                sequence.Insert(0f, step);
            }
        }

        public static Tweener GetStep(GameObject item, SequenceStepConfig config)
        {
            var step = CreateStep(item, config);
            if (config.from) step.From();
            step.SetDelay(config.delay);
            step.SetRelative(config.relative);
            step.SetEase(config.ease);

            return step;
        }

        static Tweener CreateStep(GameObject item, SequenceStepConfig config)
        {
            switch (config.type)
            {
                case SequenceStepType.Scale:
                    return item.transform.DOScale(config.value, config.duration);
                case SequenceStepType.Move:
                    return CreateMoveStep(item, config);
                case SequenceStepType.Rotate:
                    return item.transform.DORotate(config.value, config.duration);
                case SequenceStepType.Fade:
                    return CreateFadeStep(item, config);
                case SequenceStepType.FlexibleSize:
                    return CreateFlexibleSizeStep(item, config);
                case SequenceStepType.PreferredSize:
                    return CreatePreferredSizeStep(item, config);
                case SequenceStepType.Color:
                    return CreateColorStep(item, config);
                default:
                    Debug.LogError($"Invalid sequence step type: {config.type}");
                    return null;
            }
        }

        static Tweener CreateMoveStep(GameObject item, SequenceStepConfig config)
        {
            var rectTransform = item.GetComponent<RectTransform>();
            if (rectTransform != null)
                return rectTransform.DOAnchorPos(config.value, config.duration);

            return item.transform.DOLocalMove(config.value, config.duration);
        }

        static Tweener CreateFadeStep(GameObject item, SequenceStepConfig config)
        {
            var canvas = item.GetComponent<CanvasGroup>();
            if (canvas != null)
                return canvas.DOFade(config.value.x, config.duration);

            var sprite = item.GetComponent<SpriteRenderer>();
            if (sprite != null)
                return sprite.DOFade(config.value.x, config.duration);

            var childSprite = item.GetComponentInChildren<SpriteRenderer>();
            if (childSprite != null)
                return childSprite.DOFade(config.value.x, config.duration);

            var image = item.GetComponent<Image>();
            if (image != null)
                return image.DOFade(config.value.x, config.duration);

            var text = item.GetComponent<TextMeshProUGUI>();
            if (text != null)
                return text.DOFade(config.value.x, config.duration);

            Debug.LogError($"Invalid animation target: {item}.");
            return null;
        }

        static Tweener CreateColorStep(GameObject item, SequenceStepConfig config)
        {
            var sprite = item.GetComponent<SpriteRenderer>();
            if (sprite != null)
                return sprite.DOColor(config.color, config.duration);

            var childSprite = item.GetComponentInChildren<SpriteRenderer>();
            if (childSprite != null)
                return childSprite.DOColor(config.color, config.duration);

            var image = item.GetComponent<Image>();
            if (image != null)
                return image.DOColor(config.color, config.duration);

            var text = item.GetComponent<TextMeshProUGUI>();
            if (text != null)
                return text.DOColor(config.color, config.duration);

            var textMesh = item.GetComponent<TextMeshPro>();
            if (textMesh != null)
                return textMesh.DOColor(config.color, config.duration);

            Debug.LogError($"Invalid animation target: {item}.");
            return null;
        }

        static Tweener CreateFlexibleSizeStep(GameObject item, SequenceStepConfig config)
        {
            var layoutElement = item.GetComponent<LayoutElement>();
            if (layoutElement != null)
                return layoutElement.DOFlexibleSize(config.value, config.duration);

            Debug.LogError($"Invalid animation target: {item}.");
            return null;
        }

        static Tweener CreatePreferredSizeStep(GameObject item, SequenceStepConfig config)
        {
            var layoutElement = item.GetComponent<LayoutElement>();
            if (layoutElement != null)
                return layoutElement.DOPreferredSize(config.value, config.duration);

            Debug.LogError($"Invalid animation target: {item}.");
            return null;
        }
    }
}