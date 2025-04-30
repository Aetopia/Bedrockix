using System;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Unmanaged;

[StructLayout(LayoutKind.Sequential)]
readonly ref struct FILE_STANDARD_INFO
{
    internal readonly long AllocationSize, EndOfFile;

    internal readonly uint NumberOfLinks;

    internal readonly bool DeletePending, Directory;
}

[StructLayout(default(LayoutKind), Size = sizeof(char) * Length)]
readonly struct ApplicationUserModelId
{
    internal const int Length = 130;

    internal ApplicationUserModelId(string @this) => Unsafe.lstrcpy(this, @this);
}

[StructLayout(default(LayoutKind), Size = sizeof(char) * Length)]
readonly struct PackageFamilyName { internal const int Length = 65; }

readonly struct Handle(nint @this) : IDisposable
{
    readonly nint _ = @this;

    public void Dispose() => Unsafe.CloseHandle(_);

    public static implicit operator nint(in Handle @this) => @this._;
}

readonly struct Process : IDisposable
{
    public void Dispose() => _.Dispose();

    readonly Handle _; internal readonly int Id;

    internal bool this[bool value] => Safe.WaitForSingleObject(_, value);

    internal Process(int value) { Id = value; _ = new(Safe.OpenProcess(value)); }

    internal Process(nint value) { Id = Safe.GetWindowThreadProcessId(value); _ = new(Safe.OpenProcess(Id)); }

    public static implicit operator nint(in Process @this) => @this._;
}

readonly ref struct Address(nint @this, nint @params, int @object) : IDisposable
{
    readonly nint _ = @params;

    public void Write(string value) => Unsafe.WriteProcessMemory(@this, _, value, @object, default);

    public void Dispose() => Unsafe.VirtualFreeEx(@this, default, default, MEM_RELEASE);

    public static implicit operator nint(in Address @this) => @this._;
}

readonly ref struct Thread(nint @this, nint @params, nint @object) : IDisposable
{
    readonly Handle Handle = new(Unsafe.CreateRemoteThread(@this, default, default, @params, @object, default, default));

    public void Dispose() => Handle.Dispose();

    public void Wait() => Safe.WaitForSingleObject(Handle);
}