﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DemoLibrary;
using System.IO;

namespace DemoLibrary.Tests
{
    public class ExamplesTests
    {
        [Fact]
        public void ExampleLoadTextFile_ValidNameShouldWork()
        {
            string actual = Examples.ExampleLoadTextFile("This is a valid file name.");

            Assert.True(actual.Length > 0);
        }

        [Fact]
        public void ExampleLoadTextFile_InvalidNameShouldFail()
        {
            Assert.Throws<ArgumentException>("file1", () => Examples.ExampleLoadTextFile(""));

            // File not found exception does not work with a parameter name passed in (as in "file" above) - but arguemnt exception does
            // Assert.Throws<FileNotFoundException>(() => Examples.ExampleLoadTextFile(""));
        }
    }
}
