using DG.Tweening;
using Krk.Common.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace Krk.Common.Elements
{
    public class ShowableElement : MonoBehaviour
    {
        public UnityAction OnShowFinished;
        public UnityAction OnHideFinished;

        public SequenceData showSequenceData;

        Sequence showSequence;
        bool shown;

        public void Init(bool show, bool forceNewSequence = false)
        {
            shown = show;

            if (showSequence == null)
            {
                InitSequence();
            }
            else if (forceNewSequence || Application.isEditor)
            {
                showSequence.Kill();
                InitSequence();
            }

            if (shown)
                showSequence.Complete();
            else
            {
                showSequence.Rewind();
                showSequence.Flip();
            }
        }

        void InitSequence()
        {
            showSequence = DOTween.Sequence();
            showSequence.Init(showSequenceData);
            showSequence.OnComplete(ShowFinish);
            showSequence.OnRewind(HideFinish);
        }

        public void Show(bool force = false)
        {
            if (shown && !force) return;
            shown = true;

            showSequence.Flip();
            showSequence.Play();
        }

        public void Hide()
        {
            if (!shown) return;
            shown = false;

            showSequence.Flip();
            showSequence.Play();
        }

        void ShowFinish()
        {
            OnShowFinished?.Invoke();
        }

        void HideFinish()
        {
            OnHideFinished?.Invoke();
        }
    }
}