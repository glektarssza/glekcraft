namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

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

    #region Public Properties

    /// <summary>
    /// The function to use to allocate memory on the heap.
    /// </summary>
    public AllocateFunction Allocate {
        readonly get;
        set;
    }

    /// <summary>
    /// The function to use to reallocate memory on the heap.
    /// </summary>
    public ReallocateFunction Reallocate {
        readonly get;
        set;
    }

    /// <summary>
    /// The function to use to deallocate memory on the heap.
    /// </summary>
    public DeallocateFunction Deallocate {
        readonly get;
        set;
    }

    /// <summary>
    /// The pointer to the custom data to pass to each custom function.
    /// </summary>
    public IntPtr UserData {
        readonly get;
        set;
    }

    #endregion
}
