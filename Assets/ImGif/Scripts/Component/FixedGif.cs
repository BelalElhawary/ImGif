using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ImGif
{
    public class FixedGif : MonoBehaviour
    {
        public GifRenderType renderTarget;
        private SpriteRenderer _spriteRenderer;
        private RawImage _rawImage;
        private Image _image;
        private SpriteArray _sprites;

        public GifData data;


        [Range(1, 15)]
        public int frameDelay = 1;

        public bool playOnAwake = true;

        [Range(0, 25)]
        public int loopTimes;

        private bool _isPlaying;

        private void Awake()
        {
            _sprites = GifUtility.Load(data);

            GetRendererComponent();

            if (playOnAwake)
                _isPlaying = true;
        }

        private void UpdateTexture(Sprite sp)
        {
            switch(renderTarget)
            {
                case GifRenderType.Image:
                    _image.sprite = sp;
                    break;
                case GifRenderType.RawImage:
                    _rawImage.texture = sp.texture;
                    break;
                case GifRenderType.SpriteRenderer:
                    _spriteRenderer.sprite = sp;
                    break;
            }
        }

        private void GetRendererComponent()
        {
            switch(renderTarget)
            {
                case GifRenderType.Image:
                    _image = GetComponent<Image>();
                    break;
                case GifRenderType.RawImage:
                    _rawImage = GetComponent<RawImage>();
                    break;
                case GifRenderType.SpriteRenderer:
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                    break;
            }
        }

        public void Play() => _isPlaying = true;
        public void Stop() => _isPlaying = false;


        public void PlayOnce() { 
            loopTimes = 1;
            _isPlaying = true;
        }

        private int _loops = 1;
        private void LoopRound()
        {
            if(_loops == loopTimes)
            {
                _isPlaying = false;
                _loops = 1;
            }
            else
            {
                _loops++;
            }
        }

        private int _i = -1;
        private int _ignore;

        public void FixedUpdate()
        {
            if (!_isPlaying)
                return;

            if(_ignore == 0)
            {
                _i++;

                UpdateTexture(_sprites.get(_i).Item2);

                if (_i == _sprites.Length - 1)
                {
                    _i = -1;

                    if (_loops > 0)
                        LoopRound();
                }

                _ignore++;
            }else if(_ignore >= frameDelay)
            {
                _ignore = 0;
            }
            else
            {
                _ignore++;
            }
        }


#if UNITY_EDITOR
        [ContextMenu("Switch To Gif")]
        private void SwitchToGif()
        {
            var gif = gameObject.AddComponent<Gif>();
            gif.data = data;
            gif.renderTarget = renderTarget;
            gif.playOnAwake = playOnAwake;
            gif.loopTimes = loopTimes;
            DestroyImmediate(this);
        }
#endif
    }
}