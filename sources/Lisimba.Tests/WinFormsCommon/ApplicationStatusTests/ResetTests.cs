// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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

using DustInTheWind.WinFormsCommon;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.WinFormsCommon.ApplicationStatusTests
{
    [TestFixture]
    public class ResetTests
    {
        private ApplicationStatus applicationStatus;

        [SetUp]
        public void SetUp()
        {
            applicationStatus = new ApplicationStatus();
        }

        [TearDown]
        public void TearDown()
        {
            applicationStatus.Dispose();
        }

        [Test]
        public void sets_the_StatusText_value_equal_to_DefaultStatusText()
        {
            const string defaultStatusText = "DefaultStatusText";
            applicationStatus.DefaultStatusText = defaultStatusText;
            applicationStatus.SetPermanentStatusText("StatusText");

            applicationStatus.Reset();

            Assert.That(applicationStatus.StatusText, Is.EqualTo(defaultStatusText));
        }

        [Test]
        public void raises_StatusTextChanged_event()
        {
            bool eventWasRaised = false;
            applicationStatus.DefaultStatusText = "DefaultStatusText";
            applicationStatus.SetPermanentStatusText("StatusText");
            applicationStatus.StatusTextChanged += (sender, args) => eventWasRaised = true;

            applicationStatus.Reset();

            Assert.That(eventWasRaised, Is.True);
        }

        [Test]
        public void does_not_raise_StatusTextChanged_event_if_StatusText_is_already_equal_to_DefaultStatusText()
        {
            bool eventWasRaised = false;
            const string statusText = "some status";
            applicationStatus.DefaultStatusText = statusText;
            applicationStatus.SetPermanentStatusText(statusText);
            applicationStatus.StatusTextChanged += (sender, args) => eventWasRaised = true;

            applicationStatus.Reset();

            Assert.That(eventWasRaised, Is.False);
        }
    }
}