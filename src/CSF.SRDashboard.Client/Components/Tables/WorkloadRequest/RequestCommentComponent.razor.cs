﻿using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Models;
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
        public EditContext EditContext { get; set; }

        public RequestComment RequestComment { get; set; } = new RequestComment();

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
        private bool isCommentListEmpty() => this.WorkComments == null || !this.WorkComments.Any() ? false : true;
    }
}
