namespace Glekcraft.Graphics.GLFW.Tests;

[TestClass]
public class LibGLFWTests {
    #region Private Fields

    /// <summary>
    /// The mock native API provider.
    /// </summary>
    private readonly Mock<INativeAPIProvider> MoqNativeAPIProvider = new(MockBehavior.Default);

    #endregion

    #region Private Properties

    /// <summary>
    /// The mock native API provider.
    /// </summary>
    private INativeAPIProvider MockNativeAPIProvider =>
        this.MoqNativeAPIProvider.Object;


    #endregion

    [TestMethod]
    [TestCategory("Core")]
    [Description("Test whether the 'Initialize' static method returns a new instance when the library is not initialized.")]
    public void Test_Initialize_ReturnsNewInstance() {
        //-- Given
        this.MoqNativeAPIProvider.Setup((it) => it.Init()).Returns(true);

        //-- When
        var result = FluentActions.Invoking(() => LibGLFW.Initialize(this.MockNativeAPIProvider));

        //-- Then
        using var instance = result.Should().NotThrow().Subject;
    }
}
