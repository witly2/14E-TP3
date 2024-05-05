﻿using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using CineQuebec.Windows.ViewModels;

namespace CineQuebec.Windows.View;

public partial class FilmControl : UserControl
{
    private int imgSlider=0;
    private DispatcherTimer timer;
    public FilmControl()
    {
        InitializeComponent();
        AccueilViewModel filmViewModel = new AccueilViewModel();
        this.DataContext = filmViewModel;
        
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(4); 
        timer.Tick += Timer_Tick; 
        timer.Start(); 
    }
    
    private void LoadNextImage()
    {
        if (imgSlider==3)
            imgSlider=0;
        string imagePath = $"../../../Ressources/images/{imgSlider}.png";

        BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

        ImgBrushSlider.ImageSource = bitmapImage;

        imgSlider++;
    }
    
    private void Timer_Tick(object sender, EventArgs e)
    {
        LoadNextImage();
    }
}