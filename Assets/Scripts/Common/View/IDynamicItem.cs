namespace Krk.Common.View
{
    public interface IDynamicItem<in TData>
    {
        void Init(TData data);
    }
}