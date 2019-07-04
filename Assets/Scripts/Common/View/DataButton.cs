using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Common.View
{
    public class DataButton<TData> : MonoBehaviour, IDynamicItem<TData>
    {
        public UnityAction<TData> OnClicked;

        [SerializeField] Button button;

        bool locked;
        TData data;

        public TData Data => data;

        public virtual void Init(TData data)
        {
            this.data = data;
        }

        public bool Locked
        {
            get => locked;
            set
            {
                locked = value;
                button.interactable = !locked;
            }
        }

        void OnEnable()
        {
            button.onClick.AddListener(HandleButtonClicked);
        }

        void OnDisable()
        {
            button.onClick.RemoveListener(HandleButtonClicked);
        }

        void HandleButtonClicked()
        {
            OnClicked?.Invoke(data);
        }
    }
}