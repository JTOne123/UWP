using System;

namespace lindexi.uwp.Framework.ViewModel
{
    /// <summary>
    /// �ṩ��ת��Ϣ���Զ��ҵ�<see cref="NavigateViewModel"/>������ת
    /// </summary>
    public class NavigateCombinationComposite : CombinationComposite<NavigateViewModel>
    {
        public NavigateCombinationComposite(ViewModelBase source, Type view, object paramter = null) : base(source)
        {
            _run = viewModel =>
            {
                viewModel.Navigate(view, paramter);
            };
        }
    }
}