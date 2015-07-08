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

using System;
using DustInTheWind.Lisimba.Services;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Services.StatusServiceTests
{
    [TestFixture]
    public class ConstructorTests
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
        public void StatusText_is_initially_empty_string()
        {
            Assert.That(applicationStatus.StatusText, Is.EqualTo(string.Empty));
        }

        [Test]
        public void DefaultStatusText_is_initially_empty_string()
        {
            Assert.That(applicationStatus.DefaultStatusText, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ResetTimeout_is_initially_1_second()
        {
            Assert.That(applicationStatus.ResetTimeout, Is.EqualTo(TimeSpan.FromSeconds(1)));
        }
    }
}
