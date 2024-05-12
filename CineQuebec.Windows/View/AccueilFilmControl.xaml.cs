using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.ViewModels;

namespace CineQuebec.Windows.View;

public partial class AccueilFilmControl : UserControl
{
    private MainWindow mainWindow;
    public event EventHandler BorderClicked;
    public AccueilFilmControl()
    {
        InitializeComponent();
        var filmViewModel = new AccueilFilmViewModel();
         mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;

        DataContext = filmViewModel;

        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(4);
        timer.Tick += Timer_Tick;
        timer.Start();
    }
    
    private int imgSlider;
    private readonly DispatcherTimer timer;



    private void LoadNextImage()
    {
        if (imgSlider == 3)
            imgSlider = 0;
        var imagePath = $"../../../Ressources/images/{imgSlider}.png";

        var bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

        ImgBrushSlider.ImageSource = bitmapImage;

        imgSlider++;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        LoadNextImage();
    }

    private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        var main = (MainWindow)System.Windows.Application.Current.MainWindow;
        
        Border border = sender as Border;
        if (border != null)
        {
            var filmSelected = border.DataContext as Film;
            if (filmSelected is not null)
            {
                main.DetailFilmControl(filmSelected);

            }
        }

        
    }
}