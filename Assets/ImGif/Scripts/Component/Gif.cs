using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ImGif
{
    public class Gif : MonoBehaviour
    {
        public GifRenderType renderTarget;
        private SpriteRenderer sprite_renderer;
        private RawImage raw_image;
        private Image image;
        private SpriteArray sprites;

        public GifData data;

        public bool playOnAwake = true;

        public bool autoFrameRate = true;

        [Range(0.25f, 2f)]
        public float playSpeed = 1f;

        [Range(0.01f, 1f)]
        public float frameDelay = 1f;

        [Range(0, 25)]
        public int loopTimes = 0;

        private void Awake()
        {
            sprites = GifUtility.Load(data);

            GetRendererComponent();

            if (playOnAwake)
                StartCoroutine(loop());
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

        private float FrameDelay
        {
            get
            {
                if (autoFrameRate)
                {
                    return (1f / sprites.Length) * (2.25f - playSpeed);
                }
                else
                {
                    return frameDelay;
                }
            }
        }

        public void Play(){
            StopCoroutine(loop());
            StartCoroutine(loop());
        }
        public void Stop() => StopCoroutine(loop());

        public void PlayOnce() { 
            loopTimes = 1;
            Play();
        }

        IEnumerator loop()
        {
            float delay = FrameDelay;
            int loops = 1;
            for (int i = 0; i < sprites.Length; i++)
            {
                UpdateTexture(sprites.get(i));

                yield return new WaitForSeconds(delay);

                if (i == sprites.Length - 1)
                {
                    if (loopTimes > 0)
                    {
                        if (loops == loopTimes)
                            break;
                        else
                            loops++;
                    }

                    i = -1;
                }
            }
        }
#if UNITY_EDITOR
        [ContextMenu("Switch To FixedGif")]
        private void SwitchToGif()
        {
            var gif = gameObject.AddComponent<FixedGif>();
            gif.data = data;
            gif.renderTarget = renderTarget;
            gif.playOnAwake = playOnAwake;
            gif.loopTimes = loopTimes;
            DestroyImmediate(this);
        }
#endif
    }
}
