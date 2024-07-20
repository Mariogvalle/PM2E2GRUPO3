using Microsoft.Maui.Devices.Sensors;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Maui.Controls;

namespace PM2E2GRUPO3.Views;

public partial class listUbicacion : ContentPage
{
    public ObservableCollection<Models.Ubicaciones> Ubicaciones { get; set; }
    public listUbicacion()
    {

		InitializeComponent();

    }
    private async void btn_clicked(object sender, EventArgs e)
    {
        List<Models.Ubicaciones> ubicacionlist = new List<Models.Ubicaciones>();
        ubicacionlist = await Controllers.UbicacionesControllers.GetUbicaciones();
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        List<Models.Ubicaciones> ubicacionlist = new List<Models.Ubicaciones>();
        ubicacionlist = await Controllers.UbicacionesControllers.GetUbicaciones();
        UbicacionesCollectionView.ItemsSource = ubicacionlist;
    }

    
    private void btn_agregar(object sender, EventArgs e)
    {
        var page = new Views.EditUbicacion();
        Navigation.PushAsync(page);
    }

    private void OnRecordAudioClicked(object sender, EventArgs e)
    {

    }

    private void OnRecordVideoClicked(object sender, EventArgs e)
    {

    }

    private void OnSaveUbicacionClicked(object sender, EventArgs e)
    {

    }

    private async void OnDeleteUbicacion(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var ubicacion = swipeItem?.CommandParameter as Models.Ubicaciones;
        if (ubicacion != null)
        {
            var Id = ubicacion.id;
            int result = await Controllers.UbicacionesControllers.DeleteUbicacion(Id);
            if (result != -1)
            {
                await DisplayAlert("Aviso", "Eliminado con  éxito)", "Ok");
            }
        }
        else
        {
            DisplayAlert("Aviso", "No se encuentra ID", "Aceptar");
        }


    }

    private void OnPlayAudioClicked(object sender, EventArgs e)
    {

    }

    private void OnPlayVideoClicked(object sender, EventArgs e)
    {

    }

    private void OnMenuClicked(object sender, EventArgs e)
    {

    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        var page = new Views.EditUbicacion();
        Navigation.PushAsync(page);
    }

    private void OnLoginClicked(object sender, EventArgs e)
    {

    }

    private void OnEditUbicacion(object sender, EventArgs e)
    {

    }

    private void OnMapUbicacion(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var ubicacion = swipeItem?.CommandParameter as Models.Ubicaciones;

        if (ubicacion != null)
        {
            var latitude = ubicacion.latitude;
            var longitude = ubicacion.longitude;
            var page = new Views.MapPage1(latitude, longitude);
            Navigation.PushAsync(page);
        }
        else
        {
            DisplayAlert("Aviso", "No se encuentran coordenadas", "Aceptar");
        }
    }
}