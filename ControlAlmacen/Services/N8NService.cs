using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace ControlAlmacen.Services;

public class N8NService
{
    //private HttpClient cliente = new ();
    private string url = "http://192.168.29.12:11000/webhook";

    public async Task EnviarImagen(IStorageFile imagen)
    {
        using HttpClient cliente = new();
        using var contenido = new MultipartFormDataContent();
        await using var stream = await imagen.OpenReadAsync();
        var archivo  = new StreamContent(stream);
        archivo.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        contenido.Add(archivo, "imagen", imagen.Name);
        await cliente.PostAsync(url+"/imagen", contenido);
    }
}