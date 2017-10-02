using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using lindexi.uwp.Framework.ViewModel;

namespace VarietyHiggstGushed.ViewModel
{
    public class TvrwgrnNnuModel : ViewModelMessage
    {
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

        private bool _dxpoihQprdqbip = false;

        public async void DxpoihQprdqbip()
        {
            //��ȡ��Ϸ
            if (!_dxpoihQprdqbip)
            {
                await new MessageDialog("û���ҵ��浵", "û�д浵").ShowAsync();
                return;
            }
            Send(new NavigateCombinationComposite(this, typeof(StorageModel)));
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
                    Send(new NavigateCombinationComposite(this, typeof(StorageModel)));
                };
                await pzsqSgxdj.ShowAsync();
                return;
            }
            Send(new NavigateCombinationComposite(this, typeof(StorageModel)));
        }

        public override void OnNavigatedFrom(object sender, object obj)
        {

        }

        public override void OnNavigatedTo(object sender, object obj)
        {
            Read();
        }
    }

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