# ImGif
**GIF** image player for Unity.

works with Image, RawImage and SpriteRenderer

tested on Windows x64, Android, WebGl

this library is using [mgGif](https://github.com/gwaredd/mgGif) 'Gif parser for unity' to parse gif file byte array to list of texture2d

# How to use
Download unity package and import it to your project

To add new Gif asset go to Assets/ImGif/Import

![alt text](https://raw.githubusercontent.com/BelalElhawary/ImGif/main/screenshots/screenshot-1.png)

pick your file.gif image

![alt text](https://raw.githubusercontent.com/BelalElhawary/ImGif/main/screenshots/screenshot-2.png)

this will create new asset file with the same name in your Assets folder

![alt text](https://raw.githubusercontent.com/BelalElhawary/ImGif/main/screenshots/screenshot-3.png)

now you can add Gif or FixedGif Component to your GameObject and drag the gif asset file to the data field in the inspector

![alt text](https://raw.githubusercontent.com/BelalElhawary/ImGif/main/screenshots/screenshot-4.png)

# Components

**Gif**

uses Update function to loop for every texture in the array, first it will pick the first texture from the array and apply it to the target renderer then it will wait for a specific amount of time given in seconds after that will advance to the next texture.

**Fixed Gif**

uses FixedUpdate to update the texture on the target renderer which means delays should be in int value between each frame, you can set this value in the frame delay field.

**Public methods**

`public void Play()`

`public void Stop()`

`public void PlayOnce()`

# Chose Renderer
Target your preferred Renderer from **Render Target** option in Gif component
![alt text](https://raw.githubusercontent.com/BelalElhawary/ImGif/main/screenshots/screenshot-4.png)

**⚠️ Make sure you added the targeted component before running**