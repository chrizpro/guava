using System;
using Guava;
using NUnit.Framework;

namespace GuavaTest;

public class StringsTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PadStartTest()
    {
        String name = null;
    
        Strings.PadStart(null, 10, ' ');
    }
}