namespace Glekcraft.Graphics.GLFW.Tests;

[TestClass]
public class LibGLFWTests {
    private static Mock<INativeApiProvider> MockApiProvider {
        get;
    }

    static LibGLFWTests() =>
        MockApiProvider = new(MockBehavior.Default);

    [TestCleanup]
    public void AfterEach() {
        LibGLFW.Instance?.Dispose();
        MockApiProvider.Reset();
    }

    [TestMethod]
    [Description("Test that the `Init` static method returns a new instance if the library is not initialized.")]
    [TestCategory("Core")]
    public void Test_Init_ShouldReturnNewInstance() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);

        //-- When
        var result = FluentActions.Invoking(() =>
            LibGLFW.Init(MockApiProvider.Object));

        //-- Then
        result.Should().NotThrow().Subject
            .Should().BeSameAs(LibGLFW.Instance);
    }

    [TestMethod]
    [Description("Test that the `Init` static method returns an existing instance if the library is already initialized.")]
    [TestCategory("Core")]
    public void Test_Init_ShouldReturnExistingInstance() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        using var instance = LibGLFW.Init(MockApiProvider.Object);

        //-- When
        var result = FluentActions.Invoking(() =>
            LibGLFW.Init(MockApiProvider.Object));

        //-- Then
        result.Should().NotThrow().Subject
            .Should().BeSameAs(instance);
        MockApiProvider.Verify(x => x.Init(), Times.Once);
    }

    [TestMethod]
    [Description("Test that the `Init` static method throws an exception if the library fails to initialize.")]
    [TestCategory("Core")]
    public void Test_Init_ThrowsIfLibraryFailsToInitialize() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(false);

        //-- When
        var result = FluentActions.Invoking(() =>
            LibGLFW.Init(MockApiProvider.Object));

        //-- Then
        result.Should().Throw<InvalidOperationException>()
            .WithMessage("Failed to initialize the native GLFW library");
    }

    [TestMethod]
    [Description("Test that the `IsInitialized` property returns `true` if the library is initialized.")]
    [TestCategory("Core")]
    public void Test_IsInitialized_ShouldReturnTrueIfLibraryIsInitialized() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);

        //-- When
        var result = FluentActions.Invoking(() =>
            LibGLFW.Init(MockApiProvider.Object));

        //-- Then
        result.Should().NotThrow();
        LibGLFW.IsInitialized.Should().BeTrue();
    }

    [TestMethod]
    [Description("Test that the `IsInitialized` property returns `false` if the library is not initialized.")]
    [TestCategory("Core")]
    public void Test_IsInitialized_ShouldReturnFalseIfLibraryIsNotInitialized() {
        //-- Given

        //-- When
        //-- Do nothing

        //-- Then
        LibGLFW.IsInitialized.Should().BeFalse();
    }

    [TestMethod]
    [Description("Test that the `IsCurrentInstance` property returns `true` if the instance is the current instance.")]
    [TestCategory("Core")]
    public void Test_IsCurrentInstance_ShouldReturnTrueIfIsTheCurrentInstance() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        using var instance = LibGLFW.Init(MockApiProvider.Object);

        //-- When
        //-- Do nothing

        //-- Then
        instance.IsCurrentInstance.Should().BeTrue();
    }

    [TestMethod]
    [Description("Test that the `IsCurrentInstance` property returns `false` if the instance is not the current instance.")]
    [TestCategory("Core")]
    public void Test_IsCurrentInstance_ShouldReturnFalseIfIsNotTheCurrentInstance() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        using var instance = LibGLFW.Init(MockApiProvider.Object);
        instance.Terminate();

        //-- When
        //-- Do nothing

        //-- Then
        instance.IsCurrentInstance.Should().BeFalse();
    }

    [TestMethod]
    [Description("Test that the `IsDisposed` property returns `true` if the instance is disposed.")]
    [TestCategory("Core")]
    public void Test_IsDisposed_ShouldReturnTrueIfIsDisposed() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        using var instance = LibGLFW.Init(MockApiProvider.Object);

        //-- When
        instance.Dispose();

        //-- Then
        instance.IsDisposed.Should().BeTrue();
    }

    [TestMethod]
    [Description("Test that the `IsDisposed` property returns `false` if the instance is not disposed.")]
    [TestCategory("Core")]
    public void Test_IsDisposed_ShouldReturnFalseIfIsNotDisposed() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        using var instance = LibGLFW.Init(MockApiProvider.Object);

        //-- When
        //-- Do nothing

        //-- Then
        instance.IsDisposed.Should().BeFalse();
    }

    [TestMethod]
    [Description("Test that the `Dispose` method disposes of the instance.")]
    [TestCategory("Core")]
    public void Test_Dispose_ShouldDisposeOfInstance() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        using var instance = LibGLFW.Init(MockApiProvider.Object);

        //-- When
        instance.Dispose();

        //-- Then
        instance.IsDisposed.Should().BeTrue();
        instance.IsCurrentInstance.Should().BeFalse();
        LibGLFW.Instance.Should().BeNull();
        LibGLFW.IsInitialized.Should().BeFalse();
        MockApiProvider.Verify(x => x.SetErrorCallback(null), Times.Once);
        MockApiProvider.Verify(x => x.Terminate(), Times.Once);
    }

    [TestMethod]
    [Description("Test that the `Version` property returns a version.")]
    [TestCategory("Core")]
    public void Test_Version_ShouldReturnVersion() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        MockApiProvider.Setup(x => x.GetVersion()).Returns(new Version(3, 3, 3));
        using var instance = LibGLFW.Init(MockApiProvider.Object);

        //-- When
        var result = instance.NativeVersion;

        //-- Then
        result.Should().NotBeNull().And.BeEquivalentTo<Version>(new(3, 3, 3));
    }

    [TestMethod]
    [Description("Test that the `VersionString` property returns a version string.")]
    [TestCategory("Core")]
    public void Test_VersionString_ShouldReturnVersionString() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        MockApiProvider.Setup(x => x.GetVersionString()).Returns("3.3.3 (mock)");
        using var instance = LibGLFW.Init(MockApiProvider.Object);

        //-- When
        var result = instance.NativeVersionString;

        //-- Then
        result.Should().NotBeNull().And.Be("3.3.3 (mock)");
    }

    [TestMethod]
    [Description("Test that the `LastErrorCode` property returns the last error code.")]
    [TestCategory("Core")]
    public void Test_LastErrorCode_ReturnsLastErrorCode() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        MockApiProvider.Setup(x => x.SetErrorCallback(It.IsAny<INativeApiProvider.ErrorCallback?>()))
            .Callback<INativeApiProvider.ErrorCallback?>(callback => callback?.Invoke(ErrorCode.PlatformError, "Platform error"));
        using var instance = LibGLFW.Init(MockApiProvider.Object);

        //-- When
        var result = instance.LastErrorCode;

        //-- Then
        result.Should().Be(ErrorCode.PlatformError);
    }

    [TestMethod]
    [Description("Test that the `LastErrorDescription` property returns the last error description.")]
    [TestCategory("Core")]
    public void Test_LastErrorDescription_ReturnsErrorDescription() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        MockApiProvider.Setup(x => x.SetErrorCallback(It.IsAny<INativeApiProvider.ErrorCallback?>()))
            .Callback<INativeApiProvider.ErrorCallback?>(callback => callback?.Invoke(ErrorCode.PlatformError, "Platform error"));
        using var instance = LibGLFW.Init(MockApiProvider.Object);

        //-- When
        var result = instance.LastErrorDescription;

        //-- Then
        result.Should().Be("Platform error");
    }

    [TestMethod]
    [Description("Test that the `ClearLastErrorCode` method clears the last error description.")]
    [TestCategory("Core")]
    public void Test_ClearLastErrorCode_ClearsLastErrorCode() {
        //-- Given
        MockApiProvider.Setup(x => x.Init()).Returns(true);
        MockApiProvider.Setup(x => x.SetErrorCallback(It.IsAny<INativeApiProvider.ErrorCallback?>()))
            .Callback<INativeApiProvider.ErrorCallback?>(callback => callback?.Invoke(ErrorCode.PlatformError, "Platform error"));
        using var instance = LibGLFW.Init(MockApiProvider.Object);

        //-- When
        var result = instance.LastErrorDescription;

        //-- Then
        result.Should().Be("Platform error");
    }
}
