using System;

namespace lindexi.uwp.Framework.ViewModel
{
    /// <summary>
    /// �ṩ��ת��Ϣ���Զ��ҵ�<see cref="NavigateViewModel"/>������ת
    /// </summary>
    public class NavigateCombinationComposite : CombinationComposite<NavigateViewModel>
    {
        /// <summary>
        /// �ṩ��ת��Ϣ���Զ��ҵ�<see cref="NavigateViewModel"/>������ת
        /// </summary>
        public NavigateCombinationComposite(ViewModelBase source, Type view, object paramter = null) : base(source)
        {
            _run = viewModel => { viewModel.Navigate(view, paramter); };
        }

        /// <summary>
        /// �ṩ��ת��Ϣ���Զ��ҵ�<see cref="NavigateViewModel"/>������ת
        /// </summary>
        public NavigateCombinationComposite(ViewModelBase source, string key, object paramter = null) : base(source)
        {
            _run = viewModel => { viewModel.Navigate(key, paramter); };
        }
    }

    /// <summary>
    /// ��ת����Ϣ
    /// </summary>
    public class NavigateMessage : Message
    {
        /// <inheritdoc />
        public NavigateMessage(ViewModelBase source, string key, object parameter = null) : base(source)
        {
            Parameter = parameter;
            Goal = new PredicateInheritViewModel(typeof(IKeyNavigato));
            Key = key;
        }

        /// <summary>
        /// ��ת����
        /// </summary>
        public object Parameter { get; }
    }

    /// <summary>
    /// ��ת��Ϣ
    /// </summary>
    public class NavigateComposite : Composite
    {
        /// <inheritdoc />
        public NavigateComposite() : base(typeof(NavigateMessage))
        {
        }

        /// <inheritdoc />
        public override void Run(IViewModel source, IMessage e)
        {
            if (source is IKeyNavigato naviagateViewModel
                && e is NavigateMessage message)
            {
                // ֻ�п�����ת�� ViewModel �ſ���ʹ��
                naviagateViewModel.Navigate(message.Key, message.Parameter);
            }
        }
    }
}