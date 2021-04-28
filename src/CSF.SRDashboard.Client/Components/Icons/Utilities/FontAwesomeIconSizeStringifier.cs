namespace CSF.SRDashboard.Client.Components.Icons.Utilities
{
    using CSF.SRDashboard.Client.Components.Icons.Constants;

    public class FontAwesomeIconSizeStringifier
    {
        public static string GetCssClassFromSize(FontAwesomeIconSize fontAwesomeIconSize)
        {
            switch (fontAwesomeIconSize)
            {
                case FontAwesomeIconSize.XS:
                    return "fa-xs";
                case FontAwesomeIconSize.SM:
                    return "fa-sm";
                case FontAwesomeIconSize.LG:
                    return "fa-lg";
                case FontAwesomeIconSize.TWO:
                    return "fa-2x";
                case FontAwesomeIconSize.THREE:
                    return "fa-3x";
                case FontAwesomeIconSize.FOUR:
                    return "fa-4x";
                case FontAwesomeIconSize.FIVE:
                    return "fa-5x";
                case FontAwesomeIconSize.SIX:
                    return "fa-6x";
                case FontAwesomeIconSize.SEVEN:
                    return "fa-7x";
                case FontAwesomeIconSize.EIGHT:
                    return "fa-8x";
                case FontAwesomeIconSize.NINE:
                    return "fa-9x";
                case FontAwesomeIconSize.TEN:
                    return "fa-10x";
            }
            return "";
        }
    }
}
