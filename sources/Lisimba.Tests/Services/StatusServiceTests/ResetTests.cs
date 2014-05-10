// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using DustInTheWind.Lisimba.Services;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Services.StatusServiceTests
{
    [TestFixture]
    public class ResetTests
    {
        private StatusService statusService;

        [SetUp]
        public void SetUp()
        {
            statusService = new StatusService();
        }

        [TearDown]
        public void TearDown()
        {
            statusService.Dispose();
        }

        [Test]
        public void sets_the_StatusText_value_equal_to_DefaultStatusText()
        {
            const string defaultStatusText = "DefaultStatusText";
            statusService.DefaultStatusText = defaultStatusText;
            statusService.SetPermanentStatusText("StatusText");

            statusService.Reset();

            Assert.That(statusService.StatusText, Is.EqualTo(defaultStatusText));
        }

        [Test]
        public void raises_StatusTextChanged_event()
        {
            bool eventWasRaised = false;
            statusService.DefaultStatusText = "DefaultStatusText";
            statusService.SetPermanentStatusText("StatusText");
            statusService.StatusTextChanged += (sender, args) => eventWasRaised = true;

            statusService.Reset();

            Assert.That(eventWasRaised, Is.True);
        }

        [Test]
        public void does_not_raise_StatusTextChanged_event_if_StatusText_is_already_equal_to_DefaultStatusText()
        {
            bool eventWasRaised = false;
            const string statusText = "some status";
            statusService.DefaultStatusText = statusText;
            statusService.SetPermanentStatusText(statusText);
            statusService.StatusTextChanged += (sender, args) => eventWasRaised = true;

            statusService.Reset();

            Assert.That(eventWasRaised, Is.False);
        }
    }
}
