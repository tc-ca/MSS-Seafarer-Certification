namespace CSF.SRDashboard.Client.Components.Icons.Utilities
{
    using CSF.SRDashboard.Client.Components.Icons.Constants;

    public class FontAwesomeSpinAnimationTypeStringifier
    {
        public static string GetCssClassFromAnimationType(FontAwesomeSpinAnimationType fontAwesomeSpinAnimationType)
        {
            switch(fontAwesomeSpinAnimationType)
            {
                case FontAwesomeSpinAnimationType.PULSE:
                    return "fa-pulse";
                case FontAwesomeSpinAnimationType.SPIN:
                default:
                    return "fa-spin";
            }
        }
    }
}
