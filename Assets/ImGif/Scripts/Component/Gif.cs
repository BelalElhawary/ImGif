using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ImGif
{
    public class Gif : MonoBehaviour
    {
        public GifRenderType renderTarget;
        private SpriteRenderer _spriteRenderer;
        private RawImage _rawImage;
        private Image _image;
        private SpriteArray _sprites;

        public GifData data;

        public bool playOnAwake = true;

        [Range(0.25f, 2.0f)]
        public float playSpeed = 1f;

        [Range(0, 25)]
        public int loopTimes;

        private bool _isPlaying;

        private void Awake()
        {
            _sprites = GifUtility.Load(data);

            GetRendererComponent();

            _isPlaying = playOnAwake;
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

        public void Play(){
            _loops = 0;
            _isPlaying = true;
        }

        public void Stop() => _isPlaying = false;

        public void PlayOnce() { 
            loopTimes = 1;
            _isPlaying = true;
        }

        private float _lastTime;
        private float _delay;
        private Sprite _sprite;
        private int _loops = 1;
        private int _i;
        public void Update()
        {
            if (!_isPlaying) return;

            _lastTime += Time.deltaTime;

            if(_lastTime>=_delay)
            {
                UpdateTexture(_sprite);

                (_delay, _sprite) = _sprites.get(_i);
                _delay = _delay / playSpeed;

                if (_i == _sprites.Length - 1)
                {
                    if (loopTimes > 0)
                    {
                        if (_loops == loopTimes)
                            _isPlaying = false;
                        else
                            _loops++;
                    }

                    _i = -1;
                }

                _lastTime = 0;
                _i++;
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
