using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlAlmacen.Services;
using SukiUI.Toasts;

namespace ControlAlmacen.ViewModels;

public partial class AltaComponenteViewModel : ViewModelBase
{
    private FilePickerService _filePickerService = new();
    private N8NService _n8nService = new();
    
    [ObservableProperty] private bool _mostrarImagen;
    [ObservableProperty] private Bitmap? _imagen;

    [RelayCommand]
    public async Task SeleccionarImagen()
    {
        var imagenPicker = await _filePickerService.SeleccionaImagen();
        if (imagenPicker == null)
        {
            return;
        }

        MostrarImagen = true;
        await using var stream = await imagenPicker.OpenReadAsync();
        Imagen = new Bitmap(stream);

        _n8nService.EnviarImagen(imagenPicker);


    }

    [RelayCommand]
    public async Task CrearProducto()
    {
        
    }
}