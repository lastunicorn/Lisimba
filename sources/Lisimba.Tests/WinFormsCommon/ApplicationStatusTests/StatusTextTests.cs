﻿// Lisimba
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

using System;
using System.Threading;
using DustInTheWind.WinFormsCommon;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.WinFormsCommon.ApplicationStatusTests
{
    [TestFixture]
    public class StatusTextTests
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
        public void raises_StatusTextChanged_event_when_value_is_changed()
        {
            bool eventWasRaised = false;
            applicationStatus.StatusTextChanged += (sender, e) => { eventWasRaised = true; };

            applicationStatus.StatusText = "test status";

            Assert.That(eventWasRaised, Is.True);
        }

        [Test]
        public void does_not_raise_event_if_it_is_set_to_same_value()
        {
            bool eventWasRaised = false;
            const string statusText = "some text";
            applicationStatus.StatusText = statusText;
            applicationStatus.StatusTextChanged += (sender, e) => { eventWasRaised = true; };

            applicationStatus.StatusText = statusText;

            Assert.That(eventWasRaised, Is.False);
        }

        [Test]
        public void reverts_to_DefaultStatusText_value_after_ResetTimeout_time()
        {
            const string defaultStatusText = "default status text";
            const string statusText = "some status";
            applicationStatus.ResetTimeout = TimeSpan.FromMilliseconds(100);
            applicationStatus.DefaultStatusText = defaultStatusText;

            applicationStatus.StatusText = statusText;

            Thread.Sleep(1000 + TestConstants.AcceptedTimeError);

            Assert.That(applicationStatus.StatusText, Is.EqualTo(defaultStatusText));
        }
    }
}