using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Threadly.UI.DTOs;
using Threadly.UI.Models.ViewModels.Community;
using Threadly.UI.Services.Abstracts;

namespace Threadly.UI.Controllers
{
    public class CommunitiesController : Controller
    {

        private readonly IApiService _client;

        public CommunitiesController(IApiService client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _client.GetAsync<List<CommunityVM>, List<CommunityVM>>("Communities");

            return View(result);
        }

        //public async Task<IActionResult> Details(string id)
        //{
        //    var result = await _client.GetAsync<CommunityVM>($"Communities/{id}");

        //    return View(result);
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommunityCreateVM communityCreateVM)
        {


            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(communityCreateVM.Name), "Name");
            formData.Add(new StringContent(communityCreateVM.Description), "Description");
            formData.Add(new StringContent("null"), "ImageUrl");
            formData.Add(new StringContent("null"), "OwnerId");

            if(communityCreateVM.File != null)
            {
                foreach (var file in communityCreateVM.File)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    formData.Add(fileContent, "File", file.FileName);

                }
            }



            var result = await _client.PostStreamAsync<NoDataDto> ("Communities", formData);




            return RedirectToAction("Index");
        }
    }
}
