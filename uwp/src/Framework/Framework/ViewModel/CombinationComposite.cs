using System;
using lindexi.MVVM.Framework.Annotations;
using lindexi.uwp.Framework.ViewModel;

namespace lindexi.MVVM.Framework.ViewModel
{
    /// <summary>
    ///     ��� Composite �� Message
    /// </summary>
    public class CombinationComposite : Composite, IMessage, ICombinationComposite
    {
        /// <summary>
        /// ������ϵ�Composite����������Ϣ
        /// </summary>
        /// <param name="source">������Ϣ����</param>
        public CombinationComposite([NotNull] ViewModelBase source)
        {
            if (ReferenceEquals(source, null)) throw new ArgumentNullException(nameof(source));
            ((IMessage) this).Source = source;
        }

        /// <summary>
        /// ������ϵ�Composite�����Ҹ�����δ���
        /// </summary>
        /// <param name="run">��δ���</param>
        /// <param name="source">������Ϣ����</param>
        public CombinationComposite([NotNull] Action<ViewModelBase, object> run, [NotNull] ViewModelBase source)
        {
            if (ReferenceEquals(run, null)) throw new ArgumentNullException(nameof(run));
            if (ReferenceEquals(source, null)) throw new ArgumentNullException(nameof(source));

            _run = run;
            ((IMessage) this).Source = source;
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public override void Run(ViewModelBase source, IMessage message)
        {
            _run(source, message);
        }

        /// <inheritdoc />
        IViewModel IMessage.Source { get; set; }

        /// <inheritdoc />
        public IPredicateViewModel Goal { set; get; }

        /// <inheritdoc />
        public bool Predicate(IViewModel viewModel)
        {
            if (Goal == null)
            {
                return true;
            }

            return Goal.Predicate(viewModel);
        }

        protected Action<ViewModelBase, object> _run;
    }

    /// <summary>
    /// ��� Composite �� Message �����ڷ���ʱ������δ���
    /// </summary>
    /// <typeparam name="T">���͵���Ӧ���͵� ViewModel ����������� ViewModel ����</typeparam>
    public class CombinationComposite<T> : Composite, IMessage, ICombinationComposite
        where T : IViewModel
    {
        /// <summary>
        /// ������� Composite �� Message �����ڷ���ʱ������δ���
        /// </summary>
        /// <param name="source"></param>
        public CombinationComposite([NotNull] ViewModelBase source)
        {
            if (ReferenceEquals(source, null)) throw new ArgumentNullException(nameof(source));
            ((IMessage) this).Source = source;
            Goal = new PredicateInheritViewModel(typeof(T));
        }

        /// <summary>
        ///  ������� Composite �� Message �����ڷ���ʱ������δ���
        /// </summary>
        /// <param name="run">���͵���Ӧ�� ViewModel ��Ҫ��δ���</param>
        /// <param name="source"></param>
        public CombinationComposite([NotNull] Action<T> run, [NotNull] ViewModelBase source) : this(source)
        {
            if (ReferenceEquals(run, null)) throw new ArgumentNullException(nameof(run));
            if (ReferenceEquals(source, null)) throw new ArgumentNullException(nameof(source));
            _run = run;
        }

        /// <inheritdoc />
        public override void Run(IViewModel source, IMessage message)
        {
            if (source is T t)
            {
                _run.Invoke(t);
            }
        }

        /// <inheritdoc />
        IViewModel IMessage.Source { get; set; }

        /// <inheritdoc />
        public IPredicateViewModel Goal { set; get; }

        /// <inheritdoc />
        public bool Predicate(IViewModel viewModel)
        {
            if (Goal == null)
            {
                return viewModel is T;
            }

            return Goal.Predicate(viewModel);
        }

        protected Action<T> _run;
    }

    /// <summary>
    /// ��� Composite �� Message
    /// </summary>
    /// <typeparam name="T">���͵���ViewModel���ĸ�</typeparam>
    /// <typeparam name="U">���͵���Ϣ���ĸ�</typeparam>
    public class CombinationComposite<T, U> : Composite, IMessage, ICombinationComposite
        where U : IMessage where T : IViewModel
    {
        /// <summary>
        /// ������� Composite �� Message �ڷ��͵�ʱ�������δ���
        /// </summary>
        /// <param name="source"></param>
        public CombinationComposite([NotNull] ViewModelBase source)
        {
            if (ReferenceEquals(source, null)) throw new ArgumentNullException(nameof(source));
            ((IMessage) this).Source = source;
            Goal = new PredicateInheritViewModel(typeof(T));
        }

        /// <summary>
        /// ������� Composite �� Message �ڷ��͵�ʱ�������δ���
        /// </summary>
        /// <param name="run"></param>
        /// <param name="source"></param>
        public CombinationComposite([NotNull] Action<T, U> run, ViewModelBase source) : this(source)
        {
            if (ReferenceEquals(run, null)) throw new ArgumentNullException(nameof(run));
            _run = run;
        }

        /// <inheritdoc />
        public override void Run(IViewModel source, IMessage message)
        {
            if (source is T && message is U)
            {
                _run.Invoke((T) source, (U) message);
            }
        }

        /// <inheritdoc />
        IViewModel IMessage.Source { get; set; }

        /// <inheritdoc />
        public IPredicateViewModel Goal { set; get; }

        /// <inheritdoc />
        public bool Predicate(IViewModel viewModel)
        {
            if (Goal == null)
            {
                return viewModel is T;
            }

            return Goal.Predicate(viewModel);
        }

        protected Action<T, U> _run;
    }
}