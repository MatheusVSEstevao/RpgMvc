using RpgMvc.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Linq;



{
    
}

public class DisputasController : Controller
{

    public string uriBase = "http://lzsouza.somee.com/Disputas/";

    [HttpGet]
    public async Task<ActioResult> IndexAsync()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string Token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string uriBuscaPersonagens = "http://lzsouza.somee.com/RpgApi/Personagens/GetAll";
            HttpResponseMessage response = await httpClient.GetAsync(uriBuscaPersonagens);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                ViewBag.ListaAtacantes = listaPersonagens;
                ViewBag.ListaOponentes = listaPersonagens;
                return View();
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<ActionResult> IndexAsync(DisputaViewModel disputa)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string uriComplementar = "Arma";

            var content = new StringContent(JsonConvert.SerializeObject(disputa));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                disputa = await Task.Run(() => JsonConvert.DeserializeObject<DisputaViewModel>(serialized));
                TempData["Mensagem"] = disputa.Narracao;
                return RedirectToAction("Index", "Personagens");
            }
            else
                throw new System.Exception(serialized);

        }
        catch(System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<ActionResult> IndexHabilidadesAsync()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string Token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string uriBuscaPersonagens = "http://lzsouza.somee.com/RpgApi/Personagens/GetAll";
            HttpResponseMessage response = await httpClient.GetAsync(uriBuscaPersonagens);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                ViewBag.ListaAtacantes = listaPersonagens;
                ViewBag.ListaOponentes = listaPersonagens;
            }
            else
                throw new System.Exception(serialized);

                string uriBuscaHabilidades = "http://lzsouza.somee.com/RpgApi/PersonagemHabilidades/GetHabilidades";
                response = await httpClient.GetAsync(uriBuscaHabilidades);
                serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<HabilidadeViewModel> listaHabilidades = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<HabilidadeViewModel>>(serialized));
                    ViewBag.ListaHabilidades = listaHabilidades;
                }
                else
                    throw new System.Exception(serialized);

                return View("IndexHabilidades");
        }
        catch
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
    [HttpPost]
    public async Task<ActionResult> IndexHabilidadesAsync(DisputaViewModel disputa)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string uriComplementar = "Habilidade";
            var content = new StringContent(JsonConvert.SerializeObject(disputa));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                disputa = await task.Run(() =>
                JsonConvert.DeserializeObject<DisputaViewModel>(serialized));
                TempData["Mensagem"] = disputa.Narracao;
                return RedirectToAction("Index", "Personagens");
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

































}