using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ImGif
{
    public class Gif : MonoBehaviour
    {
        public GifRenderType render_on;
        private SpriteRenderer sprite_renderer;
        private Image image;
        private SpriteArray sprites;

        public string gif_name;
        public Texture2D texture;

        [Range(0.01f, 1f)]
        public float frame_delay = 1f;

        [Range(0.25f, 2f)]
        public float play_speed = 1f;

        public bool play_awake;

        public bool auto_frame_rate;

        [Range(0, 25)]
        public int loop_times = 0;

        private void Awake()
        {
            sprites = GifUtility.Load(gif_name);

            TryGetComponent(out Image image);
            TryGetComponent(out SpriteRenderer sprite_renderer);

            this.image = image;
            this.sprite_renderer = sprite_renderer;

            if (play_awake)
                Run();
        }

        public void Run()
        {
            switch (render_on)
            {
                case GifRenderType.Image:
                    StartCoroutine(image_loop(FrameRate));
                    break;
                case GifRenderType.RawImage:
                    if (image)
                        StartCoroutine(image_loop(FrameRate));
                    else
                        StartCoroutine(sprite_loop(FrameRate));
                    break;
                case GifRenderType.SpriteRenderer:
                    StartCoroutine(sprite_loop(FrameRate));
                    break;
            }
        }

        private void LoadTextureGif()
        {
            byte[] data = texture.GetRawTextureData();
            TextAsset asset = new TextAsset(System.Text.Encoding.Default.GetString(data));
            Debug.Log($"raw data = {System.Text.Encoding.Default.GetString(asset.bytes)}");
            data = Resources.Load<TextAsset>($"gif/{gif_name}").bytes;
            Debug.Log($"txt data = {System.Text.Encoding.Default.GetString(data)}");


            using (var decoder = new MG.GIF.Decoder(data))
            {
                List<Sprite> spriteList = new List<Sprite>();

                var img = decoder.NextImage();
                while (img != null)
                {
                    Texture2D tex = img.CreateTexture();
                    Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                    spriteList.Add(sp);
                    img = decoder.NextImage();
                }

                sprites = new SpriteArray(spriteList);
            }
        }

        private float FrameRate
        {
            get
            {
                if (auto_frame_rate)
                {
                    return (1f / sprites.Length) * (2.25f - play_speed);
                }
                else
                {
                    return frame_delay;
                }
            }
        }

        IEnumerator image_loop(float delay)
        {
            int loops = 1;
            for (int i = 0; i < sprites.Length; i++)
            {
                image.sprite = sprites.get(i);

                yield return new WaitForSeconds(delay);

                if (i == sprites.Length - 1)
                {
                    if (loop_times > 0)
                    {
                        if (loops == loop_times)
                            break;
                        else
                            loops++;
                    }

                    i = -1;
                }
            }
        }

        IEnumerator sprite_loop(float delay)
        {
            int loops = 1;
            for (int i = 0; i < sprites.Length; i++)
            {
                sprite_renderer.sprite = sprites.get(i);

                yield return new WaitForSeconds(delay);

                if (i == sprites.Length - 1)
                {
                    if (loop_times > 0)
                    {
                        if (loops == loop_times)
                            break;
                        else
                            loops++;
                    }

                    i = -1;
                }
            }
        }
    }

    
}
