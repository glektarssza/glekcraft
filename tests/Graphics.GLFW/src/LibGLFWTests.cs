namespace Glekcraft.Graphics.GLFW.Tests;

[TestClass]
public class LibGLFWTests {
    public static INativeAPIProvider MockAPIProvider {
        get;
    }

    static LibGLFWTests() =>
        MockAPIProvider = Mock.Of<INativeAPIProvider>(MockBehavior.Strict);

    [TestCleanup]
    public void AfterEachTest() {
        Mock.Get(MockAPIProvider).Setup((obj) => obj.Terminate());
        LibGLFW.Instance?.Dispose();
        Mock.Get(MockAPIProvider).Reset();
    }

    [TestMethod]
    [Description("Test whether the `Error` event triggers when an error occurs.")]
    [TestCategory("Core")]
    public void Test_Error_EventTriggersWhenErrorOccurs() {
        //-- Given
        ErrorCode? eventErrorCode = null;
        string? eventErrorDescription = null;
        void errorEventHandler(ErrorCode code, string? description) {
            eventErrorCode = code;
            eventErrorDescription = description;
            LibGLFW.Error -= errorEventHandler;
        }
        static void errorCallback(INativeAPIProvider.ErrorCallback? cb) =>
            cb?.Invoke(ErrorCode.PlatformError, "Mock Error");
        Mock.Get(MockAPIProvider).Setup((obj) => obj.Init()).Returns(false);
#pragma warning disable CS8625
        Mock.Get(MockAPIProvider).Setup((obj) => obj.SetErrorCallback(It.IsAny<INativeAPIProvider.ErrorCallback?>())).Callback(errorCallback).Returns(null);
#pragma warning restore CS8625
        LibGLFW.Error += errorEventHandler;

        //-- When
        var r = FluentActions.Invoking(() => LibGLFW.Initialize(MockAPIProvider));

        //-- Then
        r.Should().Throw<GLFWException>();
        eventErrorCode.Should().Be(ErrorCode.PlatformError);
        eventErrorDescription.Should().Be("Mock Error");
    }

    [TestMethod]
    [Description("Test whether the `IsInitialized` property returns `false` if the library is not initialized.")]
    [TestCategory("Core")]
    public void Test_IsInitialized_ReturnsFalseIfNotInitialized() {
        //-- Given

        //-- When
        var r = LibGLFW.IsInitialized;

        //-- Then
        r.Should().BeFalse();
    }

    [TestMethod]
    [Description("Test whether the `IsInitialized` property returns `true` if the library is initialized.")]
    [TestCategory("Core")]
    public void Test_IsInitialized_ReturnsTrueIfInitialized() {
        //-- Given
        Mock.Get(MockAPIProvider).Setup((obj) => obj.Init()).Returns(true);
#pragma warning disable CS8625
        Mock.Get(MockAPIProvider).Setup((obj) => obj.SetErrorCallback(It.IsAny<INativeAPIProvider.ErrorCallback?>())).Returns(null);
#pragma warning restore CS8625
        var instance = LibGLFW.Initialize(MockAPIProvider);

        //-- When
        var r = LibGLFW.IsInitialized;

        //-- Then
        r.Should().BeTrue();
    }
}
