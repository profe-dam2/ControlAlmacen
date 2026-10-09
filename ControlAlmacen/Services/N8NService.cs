using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Collections;
using Avalonia.Platform.Storage;

namespace ControlAlmacen.Services;

public class N8NService
{
    //private HttpClient cliente = new ();
    private string url = "http://192.168.29.12:11000/webhook";

    
    public async Task EnviarImagen(IStorageFile imagen, Guid uidproducto)
    {
        using HttpClient cliente = new();
        using var contenido = new MultipartFormDataContent();
        await using var stream = await imagen.OpenReadAsync();
        var archivo  = new StreamContent(stream);
        archivo.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        contenido.Add(archivo, "imagen", imagen.Name);
        await cliente.PostAsync(url+"/imagenes/?uidproducto="+uidproducto, contenido);
    }
    
    public async Task EnviarImagenSola(IStorageFile imagen)
    {
        using HttpClient cliente = new();
        using var contenido = new MultipartFormDataContent();
        await using var stream = await imagen.OpenReadAsync();
        var archivo  = new StreamContent(stream);
        archivo.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        contenido.Add(archivo, "imagen", imagen.Name);
        await cliente.PostAsync(url+"/imagen", contenido);
    }

    public async Task<AvaloniaList<string>> ObtenerImagenes(Guid uidproducto)
    {
        using HttpClient cliente = new();
        var listJson = await cliente.GetFromJsonAsync<JsonElement>(url+"/lista/?uidproducto="+uidproducto);
        var elementos = listJson.EnumerateArray().ToArray();
        AvaloniaList<string> listaUrlImagenes = new();
        foreach (var elemento in elementos)
        {
            string urlImagen =url + "/imagenes?uidproducto="+uidproducto +"&nombre="+  elemento.GetProperty("name").GetString();
            listaUrlImagenes.Add(urlImagen);
        }
        return listaUrlImagenes;
    }
    
    
}