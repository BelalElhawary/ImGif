using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImGif
{
    public class GifUtility
    {
        public static SpriteArray Load(GifData gif)
        {
            //var stop = Stopwatch.StartNew();
            //stop.Start();
            SpriteArray sprites;

            byte[] data = gif.Bytes;

            using (var decoder = new MG.GIF.Decoder(data))
            {
                List<Sprite> spriteList = new List<Sprite>();

                var img = decoder.NextImage();
                while (img != null)
                {
                    Texture2D tex = img.CreateTexture();
                    Debug.Log($"{tex.width} : {tex.height}");
                    Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                    spriteList.Add(sp);
                    img = decoder.NextImage();
                }

                sprites = new SpriteArray(spriteList);
            }
            return sprites;
            //stop.Stop();
            //Debug.Log($"Time to generate frames {stop.ElapsedMilliseconds}ms");
        }

        public static GifPreview GetPreview(byte[] data)
        {
            GifPreview preview = new GifPreview();

            using (var decoder = new MG.GIF.Decoder(data))
            {
                var img = decoder.NextImage();
                preview.texture = img.CreateTexture();
                preview.frames = 1;
                
                while (img != null)
                {
                    preview.frames++;
                    img = decoder.NextImage();
                }
            }

            return preview;
        }
    }

    public enum GifRenderType
    {
        Image,
        RawImage,
        SpriteRenderer,
    }

    public struct SpriteArray
    {
        private Sprite[] sprites;
        public int Length { get { return sprites.Length; } }

        public SpriteArray(Sprite[] sprites)
        {
            this.sprites = sprites;
        }

        public SpriteArray(List<Sprite> sprites)
        {
            this.sprites = sprites.ToArray();
        }

        public Sprite get(int index)
        {
            return sprites[index];
        }
    }

    public class GifPreview
    {
        public Texture2D texture;
        public int frames;
    }
}
