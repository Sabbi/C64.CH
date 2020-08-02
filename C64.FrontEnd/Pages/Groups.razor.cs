using C64.Data.Entities;
using C64.FrontEnd.Extensions;
using C64.FrontEnd.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C64.FrontEnd.Pages
{
    public partial class Groups : IDisposable
    {
        [Parameter]
        public int Id { get; set; }

        private Group group;
        private IEnumerable<Production> productions = new List<Production>();

        [Parameter]
        public string GroupName { get; set; }

        // Pagination
        private readonly int pageSize = 12;

        private int currentPage = 1;
        private int totalNumberOfPages = 0;
        private int totalNumberOfRecords = 0;

        // Sorting
        private readonly SorterData sorterData = new SorterData();

        protected override async Task OnInitializedAsync()
        {
            navigationManager.LocationChanged += OnLocationChanged;

            // Init Sorter
            sorterData.CurrentSortColumn = "Name";
            sorterData.IsSortedAscending = true;
            sorterData.SorterItems.Add(new SorterItem("Name", "Name of the Demo", true));
            sorterData.SorterItems.Add(new SorterItem("Downloads", "Downlöds", false));
            sorterData.SorterItems.Add(new SorterItem("AverageRating", "Average Ratingof the Demo", false));
            sorterData.SorterItems.Add(new SorterItem("Added", "Sort By Added", false));
            sorterData.SorterItems.Add(new SorterItem("ReleaseDate", "Sort By Released", false));

            group = await unitOfWork.Groups.GetWithProductions(Id);

            LoadQueryParamters();
            await LoadProductions();
        }

        private void OnSorted()
        {
            NavigateTo();
        }

        private void OnSelectPage(int page)
        {
            currentPage = page;
            NavigateTo();
        }

        private async void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            // Group only needs to be reloaded if it changes...
            if (group.GroupId != Id)
            {
                group = null;
                StateHasChanged();
                group = await unitOfWork.Groups.GetWithProductions(Id);
            }
            LoadQueryParamters();
            await LoadProductions();
            StateHasChanged();
        }

        private void LoadQueryParamters()
        {
            currentPage = navigationManager.GetFromQueryString<int>("currentPage");
            if (currentPage < 1) currentPage = 1;

            var sortCol = navigationManager.GetFromQueryString<string>("sortCol");
            if (sortCol != null)
                sorterData.CurrentSortColumn = sortCol;

            var sortDir = navigationManager.GetFromQueryString<bool?>("sortDir");
            if (sortDir.HasValue)
                sorterData.IsSortedAscending = sortDir.Value;
        }

        private void NavigateTo()
        {
            navigationManager.NavigateTo($"/groups/{Id}/{GroupName}?currentPage={currentPage}&sortCol={sorterData.CurrentSortColumn}&sortDir={sorterData.IsSortedAscending}");
        }

        private async Task LoadProductions()
        {
            var productionIds = group.ProductionsGroups.Select(p => p.Production.ProductionId).ToList();

            Log.Warning("Load Production for {@productionIds}", productionIds);

            var paginatedResult = await unitOfWork.Productions.GetPaginatedWithGroups(p => productionIds.Contains(p.ProductionId), sorterData.CurrentSortColumn, sorterData.IsSortedAscending, currentPage, pageSize);
            productions = paginatedResult.Data;
            currentPage = paginatedResult.CurrentPage;
            totalNumberOfPages = paginatedResult.NumberOfPages;
            totalNumberOfRecords = paginatedResult.TotalNumberOfRecords;
        }

        public void Dispose()
        {
            navigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}