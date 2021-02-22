using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.Components.Layouts
{
    public partial class Card
    {

        /// <summary>
        /// Gets or sets the Header render fragment of the Card.
        /// </summary>
        [Parameter]
        public RenderFragment Header { get; set; }

        /// <summary>
        /// Gets or sets the Body render fragment of the Card.
        /// </summary>
        [Parameter]
        public RenderFragment Body { get; set; }

        [Parameter]
        public string HeaderClasses { get; set; }

        [Parameter]
        public string HeaderStyle { get; set; }

        [Parameter]
        public string BodyClasses { get; set; }

        [Parameter]
        public string BodyStyle { get; set; }
    }
}
