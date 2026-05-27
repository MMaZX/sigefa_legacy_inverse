using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel;

[ComImport]
[CompilerGenerated]
[DefaultMember("_Default")]
[Guid("000208D7-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Sheets : IEnumerable
{
	void _VtblGap1_8();

	[DispId(170)]
	object Item
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(170)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		get;
	}
}
