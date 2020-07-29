using System;
using CDNApplication.TCComponents;
using Microsoft.AspNetCore.Components.Testing;
using Xunit;

namespace CDNApplication.Test
{
    public class TCInputRadioTest
    {
        private TestHost host;

        public TCInputRadioTest()
        {
            this.host = new TestHost();
        }

        [Fact]
        public void Test()
        {
            this.host.AddComponent<TCInputRadio<string>>();
        }
    }
}
