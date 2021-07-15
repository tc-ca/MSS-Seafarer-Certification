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
        public List<RequestCommentInfo> WorkComments {
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
            if(string.Equals(this.SortOption.Text, "2")){
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
        private TimeSpan GetElapsedTime(DateTime postedTime)
        {
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeDifference = currentTime - postedTime;

            return timeDifference;


        }
    }
   


    public partial class SortOption
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
