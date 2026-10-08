using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
namespace ControlAlmacen.Services;
public class FilePickerService
{
    public async Task<IStorageFile> SeleccionaImagen()
    {
        var ventana = App.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes
            .IClassicDesktopStyleApplicationLifetime;
        var window = ventana?.MainWindow;
        if (window == null)
        {
            return null;
        }
        var archivos = await window.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
            {
                Title = "Seleccionar Imagen",
                AllowMultiple =  false,
                FileTypeFilter = [ new FilePickerFileType("Imágenes")
                {
                    Patterns = ["*.jpg", "*.jpeg", "*.png", "*.webp"]
                } ]
            });
        return archivos.FirstOrDefault();
    }
}