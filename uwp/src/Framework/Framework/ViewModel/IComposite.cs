using lindexi.MVVM.Framework.ViewModel;

namespace lindexi.uwp.Framework.ViewModel
{
    /// <summary>
    /// ��ʾ������Ϣ
    /// </summary>
    public interface IComposite
    {
        /// <summary>
        /// ���д�����
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        void Run(IViewModel source, IMessage message);
    }
}