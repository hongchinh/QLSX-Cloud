using CRMApp.Data;
using CRMApp.Helpers;
using CRMApp.Services;
using CRMShared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CRMApp.Pages.CustomerPage
{
    public partial class ListCustomer : BlazorComponent
    {
        public  List<Customer> customerItems = new List<Customer>();
        public  List<Customer> filteredCustomerItems = new List<Customer>();
        bool isLoading = true;
        public  int index =1;
        //private Edit EditComponent { get; set; }
        //private Delete DeleteComponent { get; set; }
        //private Details DetailsComponent { get; set; }
        //private Create CreateComponent { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("Authors - OnAfterRenderAsync - firstRender = " + firstRender);

            if (firstRender)
            {
                index = 1;
                await LoadCustomer();

                isLoading = false;
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task LoadCustomer()
        {
            await Task.Delay(500);

            customerItems = filteredCustomerItems = await customersService.GetAllAsync("api/customers/list");

            if (customerItems == null)
            {
                RefreshRequest refreshRequest = new RefreshRequest();
                refreshRequest.AccessToken = await localStorageService.GetItemAsync<string>("accessToken");
                refreshRequest.RefreshToken = await localStorageService.GetItemAsync<string>("refreshToken");

                var user1 = await userService.RefreshTokenAsync(refreshRequest);
                await localStorageService.SetItemAsync("accessToken", user1.AccessToken);

                customerItems = filteredCustomerItems = await customersService.GetAllAsync("api/customers/list");
            }

            //if (authorList != null)
            //    authorList = authorList.OrderByDescending(auth => auth.AuthorId).ToList();

            StateHasChanged();
        }

        private async void DetailsClick(int id)
        {
            //await DetailsComponent.Open(id);
        }

        private async Task CreateClick()
        {
           // await CreateComponent.Open();
        }

        private async Task EditClick(int id)
        {
           // await EditComponent.Open(id);
        }

        private async Task DeleteClick(int id)
        {
           // await DeleteComponent.Open(id);
        }

        private async Task ReloadCatalogItems()
        {
            //catalogItems = await CatalogItemService.List();
            //StateHasChanged();
        }
    }
}
