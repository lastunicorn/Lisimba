// Lisimba
// Copyright (C) 2014 Dust in the Wind
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

using System;
using System.Threading;
using DustInTheWind.Lisimba.Services;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Services.StatusServiceTests
{
    [TestFixture]
    public class PropertiesTests
    {
        private StatusService statusService;

        [SetUp]
        public void SetUp()
        {
            statusService = new StatusService();
        }

        [Test]
        public void StatusText_raises_StatusTextChanged_event_when_value_is_changed()
        {
            bool eventWasRaised = false;
            statusService.StatusTextChanged += (sender, e) =>
            {
                eventWasRaised = true;
            };

            statusService.StatusText = "test status";

            Assert.That(eventWasRaised, Is.True);
        }

        [Test]
        public void StatusText_does_not_raise_event_if_it_is_set_to_same_value()
        {
            bool eventWasRaised = false;
            const string statusText = "some text";
            statusService.StatusText = statusText;
            statusService.StatusTextChanged += (sender, e) =>
            {
                eventWasRaised = true;
            };

            statusService.StatusText = statusText;

            Assert.That(eventWasRaised, Is.False);
        }

        [Test]
        public void when_DefaultStatusText_is_set_StatusText_is_set_to_same_value()
        {
            const string statusText = "some value";
            statusService.DefaultStatusText = statusText;

            Assert.That(statusService.StatusText, Is.EqualTo(statusText));
        }

        [Test]
        public void StatusText_reverts_to_DefaultStatusText_value_after_ResetTimeout_time()
        {
            const string defaultStatusText = "default status text";
            const string statusText = "some status";
            statusService.ResetTimeout = TimeSpan.FromMilliseconds(100);
            statusService.DefaultStatusText = defaultStatusText;

            statusService.StatusText = statusText;

            Thread.Sleep(1000 + TestConstants.AcceptedTimeError);

            Assert.That(statusService.StatusText, Is.EqualTo(defaultStatusText));
        }
    }
}
