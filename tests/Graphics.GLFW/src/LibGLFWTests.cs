namespace Glekcraft.Graphics.GLFW.Tests;

[TestClass]
public class LibGLFWTests {
    #region Private Fields

    /// <summary>
    /// The mock native API provider.
    /// </summary>
    private readonly Mock<INativeAPIProvider> MoqNativeAPIProvider = new(MockBehavior.Default);

    #endregion

    [TestCleanup]
    public void AfterEach() =>
        LibGLFW.Instance?.Dispose();

    [TestMethod]
    [TestCategory("Core")]
    [Description("Test whether the 'Initialize' static method returns a new instance when the library is not initialized.")]
    public void Test_Initialize_ReturnsNewInstanceWhenNotInitialized() {
        //-- Given
        this.MoqNativeAPIProvider.Setup((it) => it.Init()).Returns(true);

        //-- When
        var result = FluentActions.Invoking(() =>
            LibGLFW.Initialize(this.MoqNativeAPIProvider.Object));

        //-- Then
        result.Should().NotThrow();
    }

    [TestMethod]
    [TestCategory("Core")]
    [Description("Test whether the 'Initialize' static method returns the same instance when the library is already initialized.")]
    public void Test_Initialize_ReturnsSameInstanceWhenAlreadyInitialized() {
        //-- Given
        this.MoqNativeAPIProvider.Setup((it) => it.Init()).Returns(true);
        using var instance = LibGLFW.Initialize(this.MoqNativeAPIProvider.Object);

        //-- When
        var result = FluentActions.Invoking(() =>
            LibGLFW.Initialize(this.MoqNativeAPIProvider.Object));

        //-- Then
        using var instance2 = result.Should().NotThrow().Subject;
        instance2.Should().BeSameAs(instance);
    }

    [TestMethod]
    [TestCategory("Core")]
    [Description("Test whether the 'Initialize' static method sets the error callback before initializing the native library.")]
    public void Test_Initialize_SetsErrorCallbackBeforeInit() {
        //-- Given
        this.MoqNativeAPIProvider.Setup((it) => it.Init()).Returns(true);
        this.MoqNativeAPIProvider.Setup((it) => it.SetErrorCallback(It.IsAny<INativeAPIProvider.ErrorCallback?>()));

        //-- When
        var result = FluentActions.Invoking(() =>
                    LibGLFW.Initialize(this.MoqNativeAPIProvider.Object));

        //-- Then
        result.Should().NotThrow();
        this.MoqNativeAPIProvider.Verify((it) => it.SetErrorCallback(It.IsAny<INativeAPIProvider.ErrorCallback?>()), Times.Once);
    }

    [TestMethod]
    [TestCategory("Core")]
    [Description("Test whether the 'Initialize' static method resets the error callback on an error.")]
    public void Test_Initialize_ResetsErrorCallbackOnError() {
        //-- Given
        this.MoqNativeAPIProvider.Setup((it) => it.Init()).Returns(false);
        this.MoqNativeAPIProvider.Setup((it) => it.SetErrorCallback(It.IsAny<INativeAPIProvider.ErrorCallback?>()));

        //-- When
        var result = FluentActions.Invoking(() =>
                    LibGLFW.Initialize(this.MoqNativeAPIProvider.Object));

        //-- Then
        result.Should().Throw<GLFWException>();
        this.MoqNativeAPIProvider.Verify((it) => it.SetErrorCallback(null), Times.Once());
    }

    [TestMethod]
    [TestCategory("Core")]
    [Description("Test whether the 'Initialize' static method throws a 'GLFWException' with the right property values on an error.")]
    public void Test_Initialize_ThrowsAppropriately() {
        //-- Given
        this.MoqNativeAPIProvider.Setup((it) => it.Init()).Returns(false);
        this.MoqNativeAPIProvider.Setup((it) => it.SetErrorCallback(It.IsNotNull<INativeAPIProvider.ErrorCallback>())).Callback((INativeAPIProvider.ErrorCallback callback) => {
            callback.Invoke(ErrorCode.OutOfMemory, "Failed to allocate memory");
        });

        //-- When
        var result = FluentActions.Invoking(() =>
                    LibGLFW.Initialize(this.MoqNativeAPIProvider.Object));

        //-- Then
        result.Should().Throw<GLFWException>().Which.Should().Satisfy((GLFWException ex) => {
            ex.ErrorCode.Should().Be(ErrorCode.OutOfMemory);
            ex.ErrorDescription.Should().Be("Failed to allocate memory");
        });
    }

    [TestMethod]
    [TestCategory("Core")]
    [Description("Test whether the 'IsInitialized' static property is 'false' when the library is not initialized.")]
    public void Test_IsInitialized_IsFalseWhenLibraryIsNotInitialized() {
        //-- Given
        this.MoqNativeAPIProvider.Setup((it) => it.Init()).Returns(true);

        //-- When
        var result = LibGLFW.IsInitialized;

        //-- Then
        result.Should().BeFalse();
    }

    [TestMethod]
    [TestCategory("Core")]
    [Description("Test whether the 'IsInitialized' static property is 'true' when the library is initialized.")]
    public void Test_IsInitialized_IsTrueWhenLibraryIsInitialized() {
        //-- Given
        this.MoqNativeAPIProvider.Setup((it) => it.Init()).Returns(true);
        using var instance = LibGLFW.Initialize(this.MoqNativeAPIProvider.Object);

        //-- When
        var result = LibGLFW.IsInitialized;

        //-- Then
        result.Should().BeTrue();
    }

    [TestMethod]
    [TestCategory("Core")]
    [Description("Test whether the 'IsCurrentInstance' property is 'false' when the instance is not the current instance.")]
    public void Test_IsCurrentInstance_IsFalseWhenInstanceIsNotCurrent() {
        //-- Given
        this.MoqNativeAPIProvider.Setup((it) => it.Init()).Returns(true);
        using var instance = LibGLFW.Initialize(this.MoqNativeAPIProvider.Object);

        //-- When
        instance.Dispose();
        var result = instance.IsCurrentInstance;

        //-- Then
        result.Should().BeFalse();
    }

    [TestMethod]
    [TestCategory("Core")]
    [Description("Test whether the 'IsCurrentInstance' property is 'true' when the instance is the current instance.")]
    public void Test_IsCurrentInstance_IsTrueWhenInstanceIsCurrent() {
        //-- Given
        this.MoqNativeAPIProvider.Setup((it) => it.Init()).Returns(true);
        using var instance = LibGLFW.Initialize(this.MoqNativeAPIProvider.Object);

        //-- When
        var result = instance.IsCurrentInstance;

        //-- Then
        result.Should().BeTrue();
    }
}
