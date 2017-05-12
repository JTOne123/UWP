using System;

namespace lindexi.uwp.Framework.ViewModel
{
    /// <summary>
    /// ��� Composite �� Message
    /// </summary>
    public class CombinationComposite : Composite, IMessage, ICombinationComposite
    {
        public CombinationComposite(ViewModelBase source)
        {
            ((IMessage) this).Source = source;
        }

        public CombinationComposite(Action<ViewModelBase, object> run, ViewModelBase source)
        {
            _run = run;
            ((IMessage) this).Source = source;
        }

        public ViewModelMessage Aim { get; set; }

        ViewModelBase IMessage.Source { get; set; }

        public string Goal { set; get; }

        public override void Run(ViewModelBase source, IMessage message)
        {
            _run.Invoke(source, message);
        }

        protected Action<ViewModelBase, object> _run;

    }

    /// <summary>
    /// ��� Composite �� Message
    /// </summary>
    public class CombinationComposite<T, U>  : Composite, IMessage,ICombinationComposite
        where U : IMessage where T : IViewModel
    {
        public CombinationComposite(ViewModelBase source)
        {
            ((IMessage)this).Source = source;
        }

        public CombinationComposite(Action<T, U> run, ViewModelBase source)
        {
            _run = run;
            ((IMessage)this).Source = source;
        }

        public ViewModelMessage Aim
        {
            get; set;
        }

        ViewModelBase IMessage.Source
        {
            get; set;
        }

        public string Goal
        {
            set; get;
        }

        public override void Run(IViewModel source, IMessage message)
        {
            if (source is T && message is U)
            {
                _run.Invoke((T)source ,(U) message);
            }

        }

        protected Action<T, U> _run;
    }
}