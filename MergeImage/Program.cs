using System;
using System.IO;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace MergeImage
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("开始合成图片");
            ImageAndTextSynthetic();
            Console.WriteLine("合成结束");
            Console.ReadKey();
        }

        private static void ImageAndTextSynthetic()
        {
            using (FileStream stream = File.OpenRead(@"F:\TestProject\图片处理\MergeImage\MergeImage\Images\mem3.png"))
            {
                var install_family = new FontCollection().Install(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Font", "STXINGKA.TTF"));
                var font = new Font(install_family, 38);
                //如需要换行，除设置WrapTextWidth外，text本身要使用空格来区分换行的内容
                string text = "李 二 狗";
                Image img = Image.Load(stream);
                TextGraphicsOptions textGraphics = new TextGraphicsOptions(true)
                {
                    WrapTextWidth = 38
                };
                img.Mutate(x => x.DrawText(textGraphics, text, font, Color.Black, new PointF(50, 50)));
                Stream output = new FileStream(@"F:\TestProject\图片处理\MergeImage\MergeImage\Images\result.png", FileMode.Create);
                img.SaveAsPng(output);
                stream.Dispose();
            }
        }

        private static void ManyImageSynthetic()
        {
            using (FileStream streamTemple = File.OpenRead(@"F:\TestProject\图片处理\MergeImage\MergeImage\Images\back.png"))
            using (FileStream stream1 = File.OpenRead(@"F:\TestProject\图片处理\MergeImage\MergeImage\Images\人头.png"))
            using (FileStream stream2 = File.OpenRead(@"F:\TestProject\图片处理\MergeImage\MergeImage\Images\相框.png"))
            using (FileStream stream3 = File.OpenRead(@"F:\TestProject\图片处理\MergeImage\MergeImage\Images\技术称号.png"))
            using (FileStream stream4 = File.OpenRead(@"F:\TestProject\图片处理\MergeImage\MergeImage\Images\麒麟.png"))
            {
                var imageTemple = Image.Load(streamTemple);
                var image1 = Image.Load(stream1);
                var image2 = Image.Load(stream2);
                var image3 = Image.Load(stream3);
                var image4 = Image.Load(stream4);
                image3.Mutate(a =>
                {
                    //改变图片大小
                    a.Resize(32, 36);
                });
                image4.Mutate(a =>
                {
                    a.Resize(image4.Width / 2, image4.Height / 2);
                });
                imageTemple.Mutate(a =>
                {
                    //按顺序合成，排在前面的在底层
                    a.DrawImage(image1, new Point(27, 38), 1);
                    a.DrawImage(image2, new Point(0, 0), 1);
                    a.DrawImage(image3, new Point(28, 42), 1);
                    //走兽
                    a.DrawImage(image4, new Point(110, 110), 1);
                    //飞禽
                    //a.DrawImage(image4, new Point(110, 20), 1);
                });
                Stream stream = new FileStream(@"F:\TestProject\图片处理\MergeImage\MergeImage\Images\result.png", FileMode.Create);
                imageTemple.SaveAsPng(stream);
                stream.Dispose();
            }
        }
    }
}