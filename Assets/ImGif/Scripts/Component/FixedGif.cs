using UnityEngine;
using UnityEngine.UI;

namespace ImGif
{
    public class FixedGif : MonoBehaviour
    {
        public GifRenderType renderTarget;
        private SpriteRenderer sprite_renderer;
        private RawImage raw_image;
        private Image image;
        private SpriteArray sprites;

        public GifData data;


        [Range(1, 15)]
        public int frameDelay = 1;

        public bool playOnAwake;

        [Range(0, 25)]
        public int loopTimes = 0;

        private bool isPlaying;

        private void Awake()
        {
            sprites = GifUtility.Load(data);

            GetRendererComponent();

            if (playOnAwake)
                isPlaying = true;
        }

        private void UpdateTexture(Sprite sp)
        {
            switch(renderTarget)
            {
                case GifRenderType.Image:
                    image.sprite = sp;
                    break;
                case GifRenderType.RawImage:
                    raw_image.texture = sp.texture;
                    break;
                case GifRenderType.SpriteRenderer:
                    sprite_renderer.sprite = sp;
                    break;
            }
        }

        private void GetRendererComponent()
        {
            switch(renderTarget)
            {
                case GifRenderType.Image:
                    image = GetComponent<Image>();
                    break;
                case GifRenderType.RawImage:
                    raw_image = GetComponent<RawImage>();
                    break;
                case GifRenderType.SpriteRenderer:
                    sprite_renderer = GetComponent<SpriteRenderer>();
                    break;
            }
        }

        public void Play() => isPlaying = true;
        public void Stop() => isPlaying = false;


        public void PlayOnce() { 
            loopTimes = 1;
            isPlaying = true;
        }

        private int loops = 1;
        private void LoopRound()
        {
            if(loops == loopTimes)
            {
                isPlaying = false;
                loops = 1;
            }
            else
            {
                loops++;
            }
        }

        private int i = -1;
        private int ignore = 0;
        public void FixedUpdate()
        {
            if (!isPlaying)
                return;

            if(ignore == 0)
            {
                i++;

                UpdateTexture(sprites.get(i));

                if (i == sprites.Length - 1)
                {
                    i = -1;

                    if (loops > 0)
                        LoopRound();
                }

                ignore++;
            }else if(ignore >= frameDelay)
            {
                ignore = 0;
            }
            else
            {
                ignore++;
            }
        }
    }
}