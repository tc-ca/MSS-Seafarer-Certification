using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Utilities;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class Index: ComponentBase
    {
        RadzenGrid<DashboardRow> newRequestsGrid;
        RadzenGrid<DashboardRow> inProgressGrid;
        RadzenGrid<DashboardRow> onHoldGrid;
        RadzenGrid<DashboardRow> completedGrid;
        RadzenGrid<DashboardRow> notSubmittedGrid;

        List<DashboardRow> requestsInProgress;
        List<DashboardRow> requestsOnHold;
        List<DashboardRow> requestsCompleted;
        List<DashboardRow> requestsNotSubmitted;
        List<DashboardRow> newRequests;

        int numberOfRequestsInProgress;
        int numberOfRequestsOnHold;
        int numberOfRequestsCompleted;
        int numberOfRequestNotSubmitted;
        int numberOfNewRequests;


        int inProgressPageSize
        {
            get
            {
                return _inProgressPageSize;
            }
            set
            {
                _inProgressPageSize = value;
                StateHasChanged();
                if (inProgressGrid != null)
                {
                    inProgressGrid.Reload();
                }
            }
        }
        int _inProgressPageSize ;

        int onHoldPageSize
        {
            get
            {
                return _onHoldPageSize;
            }
            set
            {
                _onHoldPageSize = value;
                StateHasChanged();
                if (onHoldGrid != null)
                {
                    onHoldGrid.Reload();
                }
            }
        }
        int _onHoldPageSize ;

        int completedPageSize
        {
            get
            {
                return _completedPageSize;
            }
            set
            {
                _completedPageSize = value;
                StateHasChanged();
                if (completedGrid != null)
                {
                    completedGrid.Reload();
                }
            }
        }
        int _completedPageSize ;

        int notSubmittedPageSize
        {
            get
            {
                return _notSubmittedPageSize;
            }
            set
            {
                _notSubmittedPageSize = value;
                StateHasChanged();
                if (notSubmittedGrid != null)
                {
                    notSubmittedGrid.Reload();
                }
            }
        }
        int _notSubmittedPageSize ;

        int newRequestsPageSize
        {
            get
            {
                return _newRequestsPageSize;
            }
            set
            {
                _newRequestsPageSize = value;
                StateHasChanged();
                if (newRequestsGrid != null)
                {
                    newRequestsGrid.Reload();
                }
            }
        }
        int _newRequestsPageSize ;


        int totalNewRequest;
        DashboardRow oneRow;

        [Inject]
        protected IMtoaArtifactService artifactService { get; set; }

        [Inject]
        protected DialogService dialogService { get; set; }

        [Inject]
        protected NavigationManager navigationManger { get; set; }

        protected override void OnInitialized()
        {
            Utility helper = new Utility(artifactService);

            //use the following method for filling in some mock data when not getting data from MTOA
            //totalNewRequest = helper.FillMockDataForGrids(ref newRequests,
            //                                              ref requestsInProgress,
            //                                              ref requestsOnHold,
            //                                              ref requestsCompleted,
            //                                              ref requestsNotSubmitted);

            //use the following method for fetching data froom MTOA
            totalNewRequest = helper.FillDataForGrids(ref newRequests,
                                                      ref requestsInProgress,
                                                      ref requestsOnHold,
                                                      ref requestsCompleted,
                                                      ref requestsNotSubmitted);


            numberOfRequestsInProgress = requestsInProgress.Count;
            numberOfRequestsOnHold = requestsOnHold.Count;
            numberOfRequestsCompleted = requestsCompleted.Count;
            numberOfRequestNotSubmitted = requestsNotSubmitted.Count;
            numberOfNewRequests = newRequests.Count;

            oneRow = requestsInProgress.FirstOrDefault();

            if (numberOfRequestsInProgress > Constants.DefaultPageSize)
            {
                inProgressPageSize = Constants.DefaultPageSize;
            }
            else
            {
                inProgressPageSize = numberOfRequestsInProgress;
            }

            if (numberOfRequestsOnHold > Constants.DefaultPageSize)
            {
                onHoldPageSize = Constants.DefaultPageSize;
            }
            else
            {
                onHoldPageSize = numberOfRequestsOnHold;
            }

            if (numberOfRequestsCompleted > Constants.DefaultPageSize)
            {
                completedPageSize = Constants.DefaultPageSize;
            }
            else
            {
                completedPageSize = numberOfRequestsCompleted;
            }

            if (numberOfRequestNotSubmitted > Constants.DefaultPageSize)
            {
                notSubmittedPageSize = Constants.DefaultPageSize;
            }
            else
            {
                notSubmittedPageSize = numberOfRequestNotSubmitted;
            }

            if (numberOfNewRequests > Constants.DefaultPageSize)
            {
                newRequestsPageSize = Constants.DefaultPageSize;
            }
            else
            {
                newRequestsPageSize = numberOfNewRequests;
            }


            base.OnInitialized();

        }
        void OnRowDoubleClick(DashboardRow row)
        {
            var requestNumber = row.ServiceRequestNumber;

            navigationManger.NavigateTo($"{navigationManger.BaseUri}/requestdetails/" + row.ServiceRequestNumber);
        }


        void OnPageSizeChange(int value, string name)
        {
            inProgressPageSize = value;
            StateHasChanged();

            var somePageSize = inProgressGrid.PageSize;
            inProgressGrid.Reload();
        }

        void SearchButtonClicked()
        {
            dialogService.Confirm("The Global Search functionality will be\n\n " +
                                  "implemented soon. Stay tuned :) ", "The future Search Functionality",
                                  new ConfirmOptions() { OkButtonText = "OK", CancelButtonText = "Cancel" });

        }

        void CreateNewRequestButtonClicked()
        {
            dialogService.Confirm("Create New Request functionality will be\n\n " +
                                  "implemented soon. Stay tuned :)\n\n Ask Colin for updates ", "Create New Request",
                                  new ConfirmOptions() { OkButtonText = "OK", CancelButtonText = "Cancel" });

        }

        public Dictionary<string, object> SearchButtonAttribute { get; set; } =
            new Dictionary<string, object>()
            {
                { "id", "gloablSearchButton" }
            };

        public Dictionary<string, object> CreateNewRequestButtonAttribute { get; set; } =
            new Dictionary<string, object>()
            {
                { "id", "createNewRequestButton" }
            };
    }
}
