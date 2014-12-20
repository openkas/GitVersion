﻿using GitVersion;
using NUnit.Framework;

[TestFixture]
public class JenkinsTests
{
    [Test]
    public void Develop_branch()
    {
        var authentication = new Authentication();
        var versionBuilder = new Jenkins(authentication);
        var tcVersion = versionBuilder.GenerateSetVersionMessage("0.0.0-Unstable4");
        Assert.AreEqual("##teamcity[buildNumber '0.0.0-Unstable4']", tcVersion);
    }

    [Test]
    public void EscapeValues()
    {
        var authentication = new Authentication();
        var versionBuilder = new Jenkins(authentication);
        var tcVersion = versionBuilder.GenerateSetParameterMessage("Foo", "0.8.0-unstable568 Branch:'develop' Sha:'ee69bff1087ebc95c6b43aa2124bd58f5722e0cb'");
        Assert.AreEqual("##teamcity[setParameter name='GitVersion.Foo' value='0.8.0-unstable568 Branch:|'develop|' Sha:|'ee69bff1087ebc95c6b43aa2124bd58f5722e0cb|'']", tcVersion[0]);
        Assert.AreEqual("##teamcity[setParameter name='system.GitVersion.Foo' value='0.8.0-unstable568 Branch:|'develop|' Sha:|'ee69bff1087ebc95c6b43aa2124bd58f5722e0cb|'']", tcVersion[1]);
    }

    [Test]
    public void ShouldAlwayRun()
    {
        var authentication = new Authentication();
        var versionBuilder = new Jenkins(authentication);
        Assert.True(versionBuilder.CanApplyToCurrentContext());
    }

}