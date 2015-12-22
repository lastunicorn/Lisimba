// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
    public class DefaultStatusTextTests
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
        public void when_value_is_set_StatusText_is_set_to_same_value()
        {
            const string statusText = "some value";
            applicationStatus.DefaultStatusText = statusText;

            Assert.That(applicationStatus.StatusText, Is.EqualTo(statusText));
        }
    }
}