using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.View.AbonneView;
using CineQuebec.Windows.ViewModels;

namespace CineQuebec.Windows.View.UsersControls;

public partial class AccueilFilmControl : UserControl
{
    private MainWindow mainWindow;
    public event EventHandler BorderClicked;
    private Abonne _abonne;
    private Window _currentWindows;
    public AccueilFilmControl(Abonne abonne = null)
    {
        InitializeComponent();
        var filmViewModel = new AccueilFilmViewModel();
        _abonne = abonne;
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
      
       
        
        Border border = sender as Border;
        if (border != null)
        {
            var filmSelected = border.DataContext as Film;
            if (filmSelected is not null)
            {
                if (_abonne is not null)
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is NavWindowsAbonneView navWindowsAbonne)
                        {
                            navWindowsAbonne.DetailFilmControl(filmSelected, _abonne);
                            break;
                        }
                    }

                }
                else
                {
                    var main = (MainWindow) Application.Current.MainWindow;
                    main.DetailFilmControl(filmSelected);
                }
        
                
                

            }
        }

        
    }
}