using Avalonia.Controls;
using Avalonia.Input;

namespace PflanzenPi.UI.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MoistureImages_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        MoistureBehaviourPopup.IsOpen = !MoistureBehaviourPopup.IsOpen;
    }
}