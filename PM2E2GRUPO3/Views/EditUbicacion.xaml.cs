using CommunityToolkit.Maui.Views;
using Plugin.AudioRecorder;
namespace PM2E2GRUPO3.Views;

public partial class EditUbicacion : ContentPage
{
    //cpntrol para video
    private byte[] videoBytes;
    private string videoBase64="";
    //control para audio
    private AudioRecorderService audioRecorderService;
    private string audioFilePath="";
    private bool isRecording=false;
    private byte[] audioBytes;
    private string base64Audio="";
    public EditUbicacion()
	{
		InitializeComponent();
        audioRecorderService = new AudioRecorderService();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await RequestLocationPermissionAsync();
        var location = await Geolocation.GetLastKnownLocationAsync();
        if (location != null)
        {
            LatitudeEntry.Text = location.Latitude.ToString();
            LongitudeEntry.Text = location.Longitude.ToString();
        }

    }


    private async void btnVideo_Clicked(object sender, EventArgs e)
    {
        var status = await Permissions.RequestAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            return;
        }

        var options = new MediaPickerOptions
        {
            Title = "Capturar Video"
        };

        try
        {
            var mediaFile = await MediaPicker.CaptureVideoAsync(options);

            if (mediaFile != null)
            {
                using (var stream = await mediaFile.OpenReadAsync())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        videoBytes = memoryStream.ToArray();
                    }
                }

                videoBase64 = Convert.ToBase64String(videoBytes);

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tempVideo.mp4");

                File.WriteAllBytes(filePath, videoBytes);

                VideoEntry.Source = MediaSource.FromFile(filePath);

            }
            else
            {
                Console.WriteLine("Video capture cancelled or failed.");
            }
        }
        catch (FeatureNotSupportedException)
        {
            Console.WriteLine("Video capturado no esta soportado en este dispositivo");
        }
        catch (PermissionException)
        {
            Console.WriteLine("Permisos de cámara denegados");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error capturando video: {ex.Message}");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(videoBase64))
        {
            await DisplayAlert("Alerta", "Por favor grabe un video de la ubicación", "OK");
            return;
        }
/*
        if (string.IsNullOrEmpty(base64Audio))
        {
            await DisplayAlert("Alerta", "Por favor grabe un audio con la descripción de la ubicaci�n (mantenga presionado el icono del micrófono para grabar)", "OK");
            return;
        }
*/
        if (string.IsNullOrEmpty(DescripcionEntry.Text))
        {
            await DisplayAlert("Alerta", "Por favor ingrese un título para el lugar", "OK");
            return;
        }


        var ubicacion = new Models.Ubicaciones
        {
            descripcion = DescripcionEntry.Text,
            latitude = double.Parse(LatitudeEntry.Text),
            longitude = double.Parse(LongitudeEntry.Text),
            video = videoBase64,
            audio = "audio"
        };
        int result = await Controllers.UbicacionesControllers.CreateUbicacion(ubicacion);
        if (result != -1)
        {
            await DisplayAlert("Aviso", "Ingresado con  éxito)", "Ok");
        }

    }

    private void OnUpdateClicked(object sender, EventArgs e)
    {

    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {

    }

    private void OnListarClicked(object sender, EventArgs e)
    {
        var page = new Views.listUbicacion();
        Navigation.PushAsync(page);

    }

    //Permisos de localizaci n
    private async Task RequestLocationPermissionAsync()
    {
        var status2 = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status2 != PermissionStatus.Granted)
        {
            status2 = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }

        if (status2 != PermissionStatus.Granted)
        {
            await DisplayAlert("Permiso", "No se pudo obtener el permiso de localización", "OK");
        }
    }


    private async Task StartRecordingAsync()
    {
        try
        {
            // Solicita permiso para usar el micrófono
            var status2 = await Permissions.RequestAsync<Permissions.Microphone>();

            // Verifica si el permiso fue otorgado
            if (status2 != PermissionStatus.Granted)
            {
                Console.WriteLine("Permisos no concedidos.");
                return;
            }

            // Inicia la grabación de audio
            var audioRecordTask = await audioRecorderService.StartRecording();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error iniciando grabación..: {ex.Message}");
        }
    }
    
    private async Task StopRecordingAsync()
    {
        try
        {
            await audioRecorderService.StopRecording();
            string audioFilePath = audioRecorderService.GetAudioFilePath();
            audioBytes = File.ReadAllBytes(audioFilePath);
            base64Audio = Convert.ToBase64String(audioBytes);
            string tempFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tmpAudio.mp3");
            File.WriteAllBytes(tempFilePath, audioBytes);
            mediaElementAudio.Source = MediaSource.FromFile(tempFilePath);
        }
        catch (Exception ex)
        {
            // Maneja cualquier excepción que ocurra
            await DisplayAlert("Error", $"Error al detener la grabación: {ex.Message}", "OK");
        }
    }

    private async void btnGrabarAudio_Pressed(object sender, EventArgs e)
    {
        if (!isRecording)
        {
            await StartRecordingAsync();
            isRecording = true;
        }
        else
        {
            await StopRecordingAsync();
            isRecording = false;

        }
    }

}