using System.Runtime.InteropServices;

namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// A structure which is used to control how the native library makes heap
/// allocations.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct NativeAllocator {
    #region Public Delegates

    /// <summary>
    /// Allocate heap memory.
    /// </summary>
    /// <param name="size">
    /// The minimum size, in bytes, of the block of memory to allocate.
    /// </param>
    /// <param name="userData">
    /// The user-defined pointer from the allocator.
    /// </param>
    /// <returns>
    /// The pointer to the newly allocated block of memory on success;
    /// <see cref="IntPtr.Zero" /> on failure.
    /// </returns>
    public delegate IntPtr AllocateFunction(nuint size, IntPtr userData);

    /// <summary>
    /// Deallocate heap memory.
    /// </summary>
    /// <param name="block">
    /// The pointer to the block of memory to be reallocated.
    /// </param>
    /// <param name="size">
    /// The minimum size, in bytes, of the block of memory to allocate.
    /// </param>
    /// <param name="userData">
    /// The user-defined pointer from the allocator.
    /// </param>
    /// <returns>
    /// The address to the newly allocated block of memory on success;
    /// <see cref="IntPtr.Zero" /> on failure.
    /// </returns>
    public delegate IntPtr ReallocateFunction(IntPtr block, nuint size, IntPtr userData);

    /// <summary>
    /// Deallocate heap memory.
    /// </summary>
    /// <param name="block">
    /// The pointer to the block of memory to be deallocated.
    /// </param>
    /// <param name="userData">
    /// The user-defined pointer from the allocator.
    /// </param>
    public delegate void DeallocateFunction(IntPtr block, IntPtr userData);

    #endregion

    #region Public Fields

    /// <summary>
    /// The function to use to allocate memory on the heap.
    /// </summary>
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public AllocateFunction Allocate;

    /// <summary>
    /// The function to use to reallocate memory on the heap.
    /// </summary>
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public ReallocateFunction Reallocate;

    /// <summary>
    /// The function to use to deallocate memory on the heap.
    /// </summary>
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public DeallocateFunction Deallocate;

    /// <summary>
    /// The pointer to the custom data to pass to each custom function.
    /// </summary>
    [MarshalAs(UnmanagedType.SysUInt)]
    public IntPtr UserData;

    #endregion
}
