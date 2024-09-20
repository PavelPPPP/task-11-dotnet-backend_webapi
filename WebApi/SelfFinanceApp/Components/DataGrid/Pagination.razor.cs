using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace SelfFinanceApp.Components.DataGrid
{
    public partial class Pagination
    {
        [Parameter]
        public int CurrentPage { get; set; } = 1;
        [Parameter]
        public int CountItems { get; set; } = 0;
        [Parameter]
        public int[] PageSizes { get; set; } = [5, 10, 15, 20];
        [Parameter]
        public int PagesCount { get; set; } = 2;
        [Parameter]
        public int PageSize { get; set; } = 5;
        [Parameter]
        public EventCallback<int> PageSizeChanged { get; set; }

        [Inject] NavigationManager Navigation { get; set; } = default!;

        int _totalPages = 0;
        int _copyCountItems;

        readonly string _CLASS_ACTIVE = "active";
        readonly string _CLASS_DISABLED = "disabled";

        protected override void OnInitialized()
        {
            CalculateTotalPages();
        }

        protected override void OnParametersSet()
        {
            if (CountItems != _copyCountItems)
            {
                CalculateTotalPages();
            }
        }

        void CalculateTotalPages()
        {
            if (CountItems == 0)
            {
                _totalPages = 0;
                return;
            }

            _totalPages = (int)Math.Ceiling((int)CountItems / (double)PageSize);

            if (CurrentPage > _totalPages)
            {
                Navigation.NavigateTo(GetUriPage(1));
            }
        }

        string GetDisableClass()
        {
            string result = "page_item ";
            if (CurrentPage > 1)
                return result + _CLASS_DISABLED;

            return result;
        }

        string GetUriPage(int numberPage)
        {
            string uri = Navigation.Uri;

            if (Regex.IsMatch(uri, @"/[1-9]+$"))
            {
                return Regex.Replace(uri, @"/[1-9]+$", $"/{numberPage}");
            }

            if (Regex.IsMatch(uri, @"/$"))
            {
                return $"{uri}{numberPage}";
            }

            return $"{uri}/{numberPage}";
        }

        string SetActiveClassItemPage(int i)
        {
            if (i == CurrentPage)
                return _CLASS_ACTIVE;

            return "";
        }

        string SetDisabledClassForPrevBtn()
        {
            if (CurrentPage == 1)
                return _CLASS_DISABLED;

            return "";
        }

        string SetDisabledClassForNextBtn()
        {
            if (CurrentPage == _totalPages)
                return _CLASS_DISABLED;

            return "";
        }

        async Task ChangePageSize(ChangeEventArgs e)
        {
            if (e.Value is not null)
                PageSize = Int32.Parse(e.Value.ToString()!);

            await PageSizeChanged.InvokeAsync(PageSize);
        }

        void Refresh(string uri)
        {
            Navigation.NavigateTo(uri);
        }

        int GetFromNumberIteration()
        {
            if (CurrentPage <= PagesCount)
                return 1;

            int remainderFromDiv;

            Math.DivRem(CurrentPage, PagesCount, out remainderFromDiv);

            if (remainderFromDiv == 0)
                return CurrentPage - PagesCount;

            return CurrentPage - remainderFromDiv;
        }

        int GetToNumberIteration()
        {
            if (CurrentPage <= PagesCount)
                return PagesCount + 1;

            int remainderFromDiv;
            Math.DivRem(CurrentPage, PagesCount, out remainderFromDiv);

            if (remainderFromDiv == 0)
                return CurrentPage + 1;

            return CurrentPage + PagesCount + remainderFromDiv - 1;
        }
    }
}
