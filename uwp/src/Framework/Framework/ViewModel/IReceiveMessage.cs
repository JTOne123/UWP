using System.Collections.Generic;

namespace lindexi.uwp.Framework.ViewModel
{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public interface IReceiveMessage : IViewModel
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        void ReceiveMessage(object sender, IMessage message);
    }
}