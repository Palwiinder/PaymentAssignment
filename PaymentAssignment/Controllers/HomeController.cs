using Newtonsoft.Json;
using PaymentAssignment.Models;
using PaymentAssignment.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PaymentAssignment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var httpClient = new HttpClient();
            string url = "http://localhost:55383/api/payment/get-brands";
            var response = httpClient
               .GetAsync(url)
               .Result;

            var data = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<BrandViewModel>>(data);
            var editViewModel = new CreatePayementViewModel();
            editViewModel.Brand = new SelectList(result, nameof(BrandViewModel.Name), nameof(BrandViewModel.Name));
            return View(editViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreatePayementViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            var parameters = new List<KeyValuePair<string, string>>();

            parameters.Add(new KeyValuePair<string, string>("Name", model.FirstName));
            parameters.Add(new KeyValuePair<string, string>("Description", model.LastName));
            parameters.Add(new KeyValuePair<string, string>("Description", model.CreditCardNumber));
            parameters.Add(new KeyValuePair<string, string>("Description", model.SecurityCode));
            parameters.Add(new KeyValuePair<string, string>("Description", model.Amount.ToString()));
            // Brand Dont have any id so i was not able to fill that as a  singe brand parameter without id
            parameters.Add(new KeyValuePair<string, string>("Description", model.Brand);

            var encodedParameters = new FormUrlEncodedContent(parameters);
            var httpClient = new HttpClient();

            var response = httpClient
                .PostAsync("http://localhost:55383/api/payment/create",
                    encodedParameters)
                .Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Payement");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {

                var data = response.Content.ReadAsStringAsync().Result;

                var errors = JsonConvert.DeserializeObject<Error>(data);

                foreach (var key in errors.ModelState)
                {
                    foreach (var error in key.Value)
                    {
                        ModelState.AddModelError(key.Key, error);
                    }
                }

                return View(model);
               
            }
            else
            {
                //Create a log for the error message
                ModelState.AddModelError("", "Sorry. An unexpected error has occured. Please try again later");
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Payment(PayementResultViewModel model)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            var encodedParameters = new FormUrlEncodedContent(parameters);
            var httpClient = new HttpClient();

            parameters.Add(new KeyValuePair<string, string>("Name", model.PaymentReferenceNumber));
            var response = httpClient
                 .PostAsync("http://localhost:55383/api/payment/create",
                     encodedParameters)
                 .Result;

            return View();
        }


    }
}