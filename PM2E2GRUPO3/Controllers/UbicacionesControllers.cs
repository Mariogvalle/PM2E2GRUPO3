using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PM2E2GRUPO3.Controllers
{
    public static class UbicacionesControllers
    {
        //crud
        //create
        public async static Task<int> CreateUbicacion(Models.Ubicaciones _ubicacion)
        {
            try
            {
                String jsonObject = JsonConvert.SerializeObject(_ubicacion);
                System.Net.Http.StringContent contenido = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = null;
                    response = await client.PostAsync(Config.Config.EndPointCreate, contenido);

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                            //Console.writeline($"Ha ourrido un error": {response.ReasonPhrase});
                        }
                        else
                        {
                            Console.WriteLine($"Ha ocurrido un error: {response.ReasonPhrase}");
                            return -1;
                        }
                    }
                }
                return 1;
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Ha ourrido un error: {Ex.ToString()}");
                return -1;
            }
            // console.writelin($"Ha ourrido un error: {ex.tostring()}");
        }

        //Read
        public async static Task<List<Models.Ubicaciones>> GetUbicaciones()
        {
            List<Models.Ubicaciones> ubicacioneslist = new List<Models.Ubicaciones>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = null;
                    response = await client.GetAsync(Config.Config.EndPointList);

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                            try
                            {
                                ubicacioneslist = JsonConvert.DeserializeObject<List<Models.Ubicaciones>>(result);
                            }
                            catch (JsonException jex)
                            {
                            }
                        }
                    }
                    return ubicacioneslist;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async static Task<int> UpdateUbicacion(Models.Ubicaciones _ubicacion)
        {
            try
            {
                string jsonObject = JsonConvert.SerializeObject(_ubicacion);
                StringContent contenido = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync($"{Config.Config.EndPointUpdate}/{_ubicacion.id}", contenido);

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            // Puedes hacer algo con el resultado si es necesario
                            return 1;
                        }
                        else
                        {
                            Console.WriteLine($"Ha ocurrido un error: {response.ReasonPhrase}");
                            return -1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("La respuesta es nula.");
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
                return -1;
            }
        }


        public async static Task<int> DeleteUbicacion(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{Config.Config.EndPointDelete}/{id}");

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return 1;
                        }
                        else
                        {
                            Console.WriteLine($"Ha ocurrido un error: {response.ReasonPhrase}");
                            return -1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("La respuesta es nula.");
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
                return -1;
            }
        }

    }
}
