using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Models;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest
{
    public partial class RequestCommentComponent
    {
        [Parameter]
        public List<RequestCommentInfo> WorkComments
        {
            get => workComments;
            set
            {
                if (workComments == value) return;
                this.workComments = value;
                WorkCommentsChanged.InvokeAsync(value);
            }
        }
        [Parameter]
        public bool ReadOnly { get; set; }
        [Parameter]
        public EventCallback<string> SubmitComment { get; set; }
        [Parameter]
        public EventCallback<List<RequestCommentInfo>> WorkCommentsChanged { get; set; }
        [Inject]
        public IHttpContextAccessor httpContextAccessor { get; set; }
        private SortOption SortOption { get; set; } = new SortOption();
        public EditContext EditContext { get; set; }
        private List<RequestCommentInfo> workComments { get; set; }
        public RequestComment RequestComment { get; set; } = new RequestComment();

        public static List<SelectListItem> SortOptions = new List<SelectListItem>() {
            new SelectListItem { Id = "1", Text = "Newest" },
            new SelectListItem { Id = "2", Text = "Oldest" }
           };

        protected override void OnInitialized()
        {

            string language;

            httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(CookieRequestCultureProvider.DefaultCookieName, out language);

            if (string.IsNullOrWhiteSpace(language))
            {
                language = "en";
            }

            language = language.Substring(2, 2);

            foreach (var item in WorkComments)
            {
                item.CreatedDateFormatted = DateFormat(item.CreatedDateUTC.Value.DateTime, language);
            }

            this.EditContext = new EditContext(this.RequestComment);
            base.OnInitialized();
        }
        public async void SaveComment()
        {
            if (this.EditContext.Validate())
            {
                await SubmitComment.InvokeAsync(this.RequestComment.Comment);
            }
        }

        public void ChangeSortOrder()
        {
            if (string.Equals(this.SortOption.Text, "2"))
            {
                var sortedNew = WorkComments.OrderByDescending(o => o.CreatedDateUTC).ToList();
                this.WorkComments = sortedNew;
            }
            else
            {
                var sortedOld = WorkComments.OrderBy(o => o.CreatedDateUTC).ToList();
                this.WorkComments = sortedOld;
            }
            StateHasChanged();
        }

        private bool IsCommentListEmpty() => this.WorkComments == null || !this.WorkComments.Any() ? false : true;
        private string DateFormat(DateTime dateToFormat, string language, DateTime? currentTime = null)
        {
            var isFrench = false;
            DateTime now = DateTime.UtcNow;

            if (currentTime.HasValue)
            {
                now = currentTime.Value;
            }

            CultureInfo culture = new CultureInfo("en-CA");

            if (language.Equals("Fr", StringComparison.OrdinalIgnoreCase))
            {
                culture = new CultureInfo("fr-CA");
                isFrench = true;
            }

            Console.WriteLine("Comment = " + dateToFormat);

            var mins = Convert.ToInt32((now - dateToFormat).TotalMinutes); // Less than or = to 59 | 1m - 59m
            var hour = Convert.ToInt32((now - dateToFormat).TotalHours);
            var days = Convert.ToInt32((now - dateToFormat).TotalDays);

            string date;
            if (mins <= 59)
            {
                date = string.Format("{0}m", mins);
            }
            else if (hour <= 23)
            {
                date = string.Format("{0}h", hour);
            }
            else if (days <= 6)
            {
                var dayString = "d";

                if (isFrench)
                {
                    dayString = "j";
                }

                date = string.Format("{0}{1}", days, dayString);
            }
            else
            {
                date = dateToFormat.ToString("dd MMM, yyyy", culture);
                var charsToRemove = new string[] { "." };
                foreach (var c in charsToRemove)
                {
                    date = date.Replace(c, string.Empty);
                }
            }

            return date;
        }
    }

    public partial class SortOption
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
