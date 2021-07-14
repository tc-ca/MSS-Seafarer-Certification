using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Models;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest
{
    public partial class RequestCommentComponent
    {
        [Parameter]
        public List<RequestCommentInfo> WorkComments { get; set; }
        [Parameter]
        public bool ReadOnly { get; set; }
        [Parameter]
        public EventCallback<string> SubmitComment { get; set; }
        private SortOption SortOption { get; set; } = new SortOption();
        public EditContext EditContext { get; set; }

        public RequestComment RequestComment { get; set; } = new RequestComment();

        public static List<SelectListItem> SortOptions = new List<SelectListItem>() {
            new SelectListItem { Id = "1", Text = "Newest" },
            new SelectListItem { Id = "2", Text = "Oldest" }
           };

        protected override void OnInitialized()
        {
           

            this.EditContext = new EditContext(this.RequestComment);
            base.OnInitialized();
        }
        public async Task SaveComment()
        {
            if (this.EditContext.Validate())
            {
                await SubmitComment.InvokeAsync(this.RequestComment.Comment);
            }
            StateHasChanged();
        }
        
        public void ChangeSortOrder()
        {
            if(string.Equals(this.SortOption.Value, "Newest")){
                WorkComments.OrderByDescending(o => o.CreatedDateUTC);
            }
            else
            {
                WorkComments.OrderBy(o => o.CreatedDateUTC);
            }
            StateHasChanged();
        }

        private bool isCommentListEmpty() => this.WorkComments == null || !this.WorkComments.Any() ? false : true;

    }
    public partial class SortOption
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
}
