using System;
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
    public async Task ObtenerImagenes()
    {
        await _n8nService.ObtenerImagenes(Guid.Parse("2f8ba41e-de1e-4f30-84b8-fd9e3d8b5d6e"));
    }
    
    
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
        var uidproducto = Guid.Parse("2f8ba41e-de1e-4f30-84b8-fd9e3d8b5d6e");
        _n8nService.EnviarImagen(imagenPicker,uidproducto);


    }

    [RelayCommand]
    public async Task CrearProducto()
    {
        
    }
}