using ObjectsFall.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ObjectsFall.View
{
    public partial class MainPage : ContentPage
    {
        #region Fields
        private MainPageViewModel viewModel;       
        #endregion
        #region Constructor
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            BindingContext = viewModel;
        }
        #endregion
        #region methods
        private async void StartGame(object sender, System.EventArgs e)
        {
            bool showAlertPopup = (string.IsNullOrEmpty(viewModel.PlayerName) || viewModel.RandomTimeBetween == 0);
            if (showAlertPopup)
            {
                string alertMessage = (viewModel.RandomTimeBetween == 0) ? "Please enter valed  rendom time between fall objects" : "Please enter player name";
                await App.Current.MainPage.DisplayAlert("Alert", alertMessage, "Ok");
            }
           
            else
            {
                RestImagesVisibility();

                viewModel.StartTimer();
                viewModel.IsTimeEnd = false;
                while (!viewModel.IsTimeEnd)
                {
                    await RandomMoveItems();
                };
            }

        }

        private void RestImagesVisibility()
        {
            image1.IsVisible = true;
            image2.IsVisible = true;
            image3.IsVisible = true;
            image4.IsVisible = true;
            image5.IsVisible = true;
            image6.IsVisible = true;
        }

        private async Task RandomMoveItems()
        {
            await Task.Delay(GetRandomDelayTime());
            await MoveItems();
        }

        private int GetRandomDelayTime()
        {
            Random randomSecond = new Random();
            return randomSecond.Next(0, viewModel.RandomTimeBetween); ;
        }

        private async Task MoveItems()
        {
            int randomNextItems = GetNextRandomFallItem();
            await RunMoves(randomNextItems);

            if (!viewModel.IsTimeEnd)
            {
                viewModel.Total += randomNextItems;
                viewModel.Score -= randomNextItems;
            }
        }

        private int GetNextRandomFallItem()
        {
            Random randomItems = new Random();
            return randomItems.Next(1, 7); ;
        }

        private async Task RunMoves(int totalNumberToMove)
        {
            var moveImag1 = MoveOneImage(GetPresent());
            var moveImag2 = MoveOneImage(GetPresent());
            var moveImag3 = MoveOneImage(GetPresent());
            var moveImag4 = MoveOneImage(GetPresent());
            var moveImag5 = MoveOneImage(GetPresent());
            var moveImag6 = MoveOneImage(GetPresent());

            switch (totalNumberToMove)
            {
                case 1:
                    await moveImag1;
                    break;
                case 2:
                    await moveImag1;
                    await moveImag2;
                    break;
                case 3:
                    await moveImag1;
                    await moveImag2;
                    await moveImag3;
                    break;
                case 4:
                    await moveImag1;
                    await moveImag2;
                    await moveImag3;
                    await moveImag4;
                    break;
                case 5:
                    await moveImag1;
                    await moveImag2;
                    await moveImag3;
                    await moveImag4;
                    await moveImag5;
                    break;
                case 6:
                    await moveImag1;
                    await moveImag2;
                    await moveImag3;
                    await moveImag4;
                    await moveImag5;
                    await moveImag6;
                    break;
                default:
                    break;
            }      
        }

        private async Task MoveOneImage(Image image)
        {
            var lastPostionY = uint.Parse(Math.Round(LastItem.Bounds.Y).ToString());
            var lastPostionX = uint.Parse(Math.Round(LastItem.Bounds.X).ToString());

            image.IsVisible = true;
            await Device.InvokeOnMainThreadAsync(() => image.TranslateTo(lastPostionX, lastPostionY, viewModel.MoveSpeed, Easing.SinInOut));
            image.TranslationX = image.AnchorX;
            image.TranslationY = image.AnchorY;
        }

        private Image GetPresent()
        {
            switch (GetRandomNumberImage())
            {
                case 1:
                    return image1;
                case 2:
                    return image2;
                case 3:
                    return image3;
                case 4:
                    return image4;
                case 5:
                    return image5;
                case 6:
                    return image6;
            }
            return null;
        }

        private  int GetRandomNumberImage()
        {
            Random rand = new Random();
            int i = rand.Next(1, 7);
            return i;
        }

        #endregion
    }
}
