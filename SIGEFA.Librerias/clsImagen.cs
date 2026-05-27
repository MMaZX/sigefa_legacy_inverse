using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SIGEFA.Librerias;

public class clsImagen
{
	public static byte[] ImagenAbyte(Image Imagen)
	{
		MemoryStream memory = new MemoryStream();
		Imagen?.Save(memory, ImageFormat.Jpeg);
		return memory.ToArray();
	}

	public static Image ByteArrayToImage(byte[] byteArrayIn)
	{
		MemoryStream ms = new MemoryStream(byteArrayIn);
		return Image.FromStream(ms);
	}

	public static byte[] ImageToByteArray(Image imageIn)
	{
		MemoryStream ms = new MemoryStream();
		imageIn.Save(ms, ImageFormat.Jpeg);
		return ms.ToArray();
	}
}
