// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// Animat-Studio/IComponentFactory.cs
// --------------------------------------------------------------------------------
// Copyright (c) 2014, Jieni Luchijinzhou a.k.a Aragorn Wyvernzora
// 
// This file is a part of Animat-Studio.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy 
// of this software and associated documentation files (the "Software"), to deal 
// in the Software without restriction, including without limitation the rights 
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
// of the Software, and to permit persons to whom the Software is furnished to do 
// so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all 
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

using System;

namespace Animat.Project.Moduality
{
    /// <summary>
    ///     Defines a generalized type-specific factory class that is able to create instances of another class.
    /// </summary>
    public interface IComponentFactory
    {
        /// <summary>
        ///     Creates an instance of the class associated with the factory.
        /// </summary>
        /// <param name="arguments">Argument for creating an instance.</param>
        /// <returns></returns>
        Object Create(params Object[] arguments);

        /// <summary>
        /// Specifies error that occured while loading the component factory.
        /// If there was no error, the value of this property is null.
        /// </summary>
        Exception LoadingError { get; set; }
    }

    /// <summary>
    ///     Defines a generic type-specific factory class that is able to create instances of another class.
    /// </summary>
    /// <typeparam name="T">Type of instances created by this factory.</typeparam>
    public interface IComponentFactory<out T> : IComponentFactory
    {
        /// <summary>
        ///     Creates an instance of the class associated with the factory.
        /// </summary>
        /// <param name="arguments">Argument for creating an instance.</param>
        /// <returns></returns>
        new T Create(params Object[] arguments);
    }
}