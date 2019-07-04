using System.Collections.Generic;
using UnityEngine;

namespace Krk.Common.View
{
    public class DynamicContent
    {
        public void Init<TItem>(Transform parent, TItem template, IList<TItem> items, int count)
            where TItem : Component
        {
            var size = Mathf.Min(items.Count, count);

            for (int i = size; i < items.Count; i++)
            {
                DisableItem(items[i]);
            }

            for (int i = size; i < count; i++)
            {
                items.Add(CreateItem(parent, template));
            }
        }

        public void Init<TItem, TData>(Transform parent, TItem template, IList<TItem> items, IList<TData> entries)
            where TItem : Component, IDynamicItem<TData>
        {
            var size = Mathf.Min(items.Count, entries.Count);

            for (int i = 0; i < size; i++)
            {
                UpdateItem(items[i], entries[i]);
            }

            for (int i = size; i < items.Count; i++)
            {
                DisableItem(items[i]);
            }

            for (int i = size; i < entries.Count; i++)
            {
                items.Add(CreateItem(parent, template, entries[i]));
            }
        }

        void UpdateItem<TItem, TData>(TItem item, TData data) where TItem : Component, IDynamicItem<TData>
        {
            item.gameObject.SetActive(true);
            item.Init(data);
        }

        void DisableItem<TItem>(TItem item) where TItem : Component
        {
            item.gameObject.SetActive(false);
        }

        TItem CreateItem<TItem, TData>(Transform parent, TItem template, TData data)
            where TItem : Component, IDynamicItem<TData>
        {
            var item = Object.Instantiate(template, parent);
            item.gameObject.SetActive(true);
            item.Init(data);

            return item;
        }

        TItem CreateItem<TItem>(Transform parent, TItem template)
            where TItem : Component
        {
            var item = Object.Instantiate(template, parent);
            item.gameObject.SetActive(true);

            return item;
        }
    }
}