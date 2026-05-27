using System.Runtime.InteropServices;

namespace SIGEFA.Librerias.Librerias.Util;

internal class WinAPI
{
	private const int SWP_NOSIZE = 1;

	private const int SWP_NOMOVE = 2;

	private const int SWP_NOACTIVATE = 16;

	private const int wFlags = 19;

	private const int HWND_TOPMOST = -1;

	private const int HWND_NOTOPMOST = -2;

	[DllImport("user32.DLL")]
	private static extern void SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int wFlags);

	public static void SiempreEncima(int handle)
	{
		SetWindowPos(handle, -1, 0, 0, 0, 0, 19);
	}

	public static void NoSiempreEncima(int handle)
	{
		SetWindowPos(handle, -2, 0, 0, 0, 0, 19);
	}
}
