using System;
using lindexi.uwp.Framework.ViewModel;

namespace lindexi.MVVM.Framework.ViewModel
{
    /// <summary>
    /// ���Դ�ָ���� ViewModel ��ȡֵ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class GetValueCombinationComposite<T> : CombinationComposite
    {
        /// <summary>
        /// ���Դ�ָ���� ViewModel ��ȡֵ
        /// </summary>
        /// <param name="source"></param>
        /// <param name="continueWith">�ڻ�ȡֵ֮������</param>
        public GetValueCombinationComposite(ViewModelBase source, Action<T> continueWith) : base(source)
        {
            ContinueWith = continueWith;
        }

        private Action<T> ContinueWith { get; set; }

        public override void Run(IViewModel source, IMessage message)
        {
            if (source is IViewModelValue<T> viewmodel)
            {
                ContinueWith(viewmodel.Value);
            }
        }
    }
}