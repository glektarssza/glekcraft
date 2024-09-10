namespace Glekcraft.Graphics.GLFW.Tests;

[TestClass]
public class LibGLFWTests {
    public static Mock<INativeAPIProvider> MockProvider {
        get;
    } = new();

    [TestCleanup]
    public void AfterEach() {
        LibGLFW.Instance?.Terminate();
        LibGLFW.ClearLastError();
    }

    [TestMethod]
    [Description("`IsInitialized` should return `false` when the native library is not initialized.")]
    [TestCategory("Core")]
    public void IsInitialized_ReturnsFalse_WhenNotInitialized() {
        //-- Given

        //-- When
        var r = LibGLFW.IsInitialized;

        //-- Then
        r.Should().BeFalse();
    }

    [TestMethod]
    [Description("`IsInitialized` should return `true` when the native library is initialized.")]
    [TestCategory("Core")]
    public void IsInitialized_ReturnsTrue_WhenInitialized() {
        //-- Given
        MockProvider.Setup(x => x.Init()).Returns(true);
        using var lib = LibGLFW.Initialize(MockProvider.Object);

        //-- When
        var r = LibGLFW.IsInitialized;

        //-- Then
        r.Should().BeTrue();
    }
}
