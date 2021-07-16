namespace CSF.SRDashboard.Client.Graph
{
    public static class GraphConstants
    {
        // Defines the permission scopes used by the app
        public readonly static string[] Scopes =
        {
            "User.Read",
        };

        public readonly static string[] WorkloadManagementAPIScopes =
        {
            "https://034gc.onmicrosoft.com/ncd-wms-dev/WorkItem.Read.All",
            "https://034gc.onmicrosoft.com/ncd-wms-dev/WorkItem.CreateUpdate.All"
        };
    }
}
