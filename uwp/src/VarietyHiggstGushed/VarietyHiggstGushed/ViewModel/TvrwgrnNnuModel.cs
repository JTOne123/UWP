using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using lindexi.MVVM.Framework.ViewModel;

namespace VarietyHiggstGushed.ViewModel
{
    public class TvrwgrnNnuModel : ViewModelMessage
    {
        public async void DxpoihQprdqbip()
        {
            //��ȡ��Ϸ
            if (!_dxpoihQprdqbip)
            {
                await new MessageDialog("û���ҵ��浵", "û�д浵").ShowAsync();
                return;
            }

            Send(new NavigateCombinationComposite(this, typeof(KdgderhlMzhpModel)));
        }


        public async void AdraqbqhUgtwg()
        {
            if (_dxpoihQprdqbip)
            {
                var pzsqSgxdj = new ContentDialog
                {
                    Title = "�Ѿ����ڴ浵���Ƿ񸲸�",
                    PrimaryButtonText = "ȷ��",
                    SecondaryButtonText = "ȡ��"
                };

                pzsqSgxdj.PrimaryButtonClick += async (sender, args) =>
                {
                    await AccountGoverment.JwAccountGoverment.Read();
                    Send(new NavigateCombinationComposite(this, typeof(KdgderhlMzhpModel)));
                };
                await pzsqSgxdj.ShowAsync();
                return;
            }

            Send(new NavigateCombinationComposite(this, typeof(KdgderhlMzhpModel)));
        }

        public override void OnNavigatedFrom(object sender, object obj)
        {
        }

        public override void OnNavigatedTo(object sender, object obj)
        {
            Read();
        }

        private bool _dxpoihQprdqbip;

        private async void Read()
        {
            if (await AccountGoverment.JwAccountGoverment.ReadJwStorage())
            {
                //���ǵ�һ��ʹ��
                _dxpoihQprdqbip = true;
            }
            else
            {
                await AccountGoverment.JwAccountGoverment.Read();
            }
        }
    }
}