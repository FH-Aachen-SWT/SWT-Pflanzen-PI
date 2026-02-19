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

    private void BrightnessImages_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BrightnessBehavioursPopup.IsOpen = !BrightnessBehavioursPopup.IsOpen;
    }

    private void PlantImage_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        PersonalitiesPopup.IsOpen = !PersonalitiesPopup.IsOpen;
    }

    private void RootGrid_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        RootGrid.Focus();
    }

    private void NameTextBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape)
        {
            RootGrid.Focus();
        }
    }
}