<div style="width: 100%; margin: 20px; text-align: center;">
	<img alogn="center" src="https://github.com/jluchiji/Animat-Studio/blob/master/Animat.Studio/Resources/logo-banner-dark.png?raw=true" />
</div>


##Introduction
The **AmiMat** project is a collaboration by [R1cebank](http://github.com/R1cebank) and [Wyvernzora](https://github.com/jluchiji) to create a lightweight animation framework. The animation framework is mainly intended for developing desktop sprites, but it would also be perfect for other 2D animations, including games.

The AniMat Studio will support multiple project types, including **AniMat Resource Files**, **BarloX Animation** and even **GIF**.

##Project Types
###AmiMat Resource File
AmiMat Resource File is a great way to unify all your image frames and action sequences into one package. The AmiMat is specifically designed for game developers, who need a high level of flexibility and control over the animation. However, if your animation mostly runs on its own, you may consider using BarloX Animation.

###BarloX Animation
BarloX Animation is a plug-and-play type of animation file, with a self-guiding animation system. It can independently decide what animation sequence to play next, and supports both random decisions and decisions based on external events. However, if you need greater control over your animation that simple sequence jumps, please consider using AniMat Resource File.

###GIF File
Good ol' GIF is supported everywhere, so it is your best bet if you wanna use your animation all over the place. You can render you existing project of any type to a GIF image, but please note that interactive features are not supported there.

##Contributing
Just fork this project! However, there is a catch. Please add the following NuGet feed to make sure you get all the dependencies: `http://www.myget.org/F/wyvernzora/`

If you want to know what library is there, you can check out the [libWyvernzora Project](http://github.com/jluchiji/libWyvernzora).

##License
###The MIT License (MIT)

Copyright (c) 2014 Jieni Luchijinzhou a.k.a Aragorn Wyvernzora

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

 - The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.