using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImGif
{
    public class GifUtility
    {
        public static SpriteArray Load(string fileName)
        {
            //var stop = Stopwatch.StartNew();
            //stop.Start();
            SpriteArray sprites;

            byte[] data = Resources.Load<TextAsset>($"gif/{fileName}").bytes;

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
    }

    public enum GifRenderType
    {
        Image,
        SpriteRenderer,
        RawImage
    }

    [System.Serializable]
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
}
