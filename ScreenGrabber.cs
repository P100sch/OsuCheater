using System;

namespace OsuCheater
{
  class ScreenGrabber
  {
    private const int GWL_STYLE = -16;              //hex constant for style changing
    private const int WS_BORDER = 0x00800000;       //window with border
    private const int WS_CAPTION = 0x00C00000;      //window with a title bar
    private const int WS_SYSMENU = 0x00080000;      //window with no borders etc.
    private const int WS_MINIMIZEBOX = 0x00020000;  //window with minimizebox


    public static Bitmap printWindow(IntPtr hwnd)
    {
      RECT rc;
      GetWindowRect(hwnd, out rc);
      Bitmap bmp;

      //make window borderless
      SetWindowLong(hwnd, GWL_STYLE, WS_SYSMENU);
      SetWindowPos(hwnd, -2, rc.X, rc.Y, rc.Width, rc.Height, 0x0040);

      DrawMenuBar(hwnd);
      bmp = new Bitmap(800, 800, PixelFormat.Format32bppArgb);
      Graphics gfxBmp = Graphics.FromImage(bmp);
      IntPtr hdcBitmap = gfxBmp.GetHdc();
      PrintWindow(hwnd, hdcBitmap, 0);

      gfxBmp.ReleaseHdc(hdcBitmap);
      gfxBmp.Dispose();

      //restore window
      SetWindowLong(hwnd, GWL_STYLE, WS_CAPTION | WS_BORDER | WS_SYSMENU | WS_MINIMIZEBOX);
      DrawMenuBar(hwnd);
      ShowWindowAsync(hwnd, 1); //1 = Normal

      return bmp;
    }
  }
}
